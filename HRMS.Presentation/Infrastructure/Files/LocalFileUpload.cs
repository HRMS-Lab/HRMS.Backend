namespace HRMS.Presentation.Infrastructure.Files
{
	public class LocalFileUpload : IFileUpload
	{
		private readonly IWebHostEnvironment _webHostEnv;
		private readonly string _baseUrl;
		public LocalFileUpload(IWebHostEnvironment webHostEnv, IConfiguration configuration)
		{
			_webHostEnv = webHostEnv;
			_baseUrl = "http://41.196.0.83/HRMS_Abdo";
		}

		public async Task<string> UploadFileAsync(IFormFile file, string innerDir = "")
		{
			if (file.Length <= 0)
			{
				throw new Exception("No file is provided.");
			}

			string uploadFolder = Path.Combine(_webHostEnv.WebRootPath, "Uploads", innerDir);
			Directory.CreateDirectory(uploadFolder);

			string uniqueFileName = Guid.NewGuid().ToString() + "-" + file.FileName;
			string filePath = Path.Combine(uploadFolder, uniqueFileName);

			try
			{
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					await file.CopyToAsync(fileStream);
				}

				string fileUrl = $"{_baseUrl}/Uploads/{innerDir}/{uniqueFileName}";
				return fileUrl;
			}
			catch (Exception ex)
			{
				throw new Exception("An error occurred while uploading the file.", ex);
			}
		}

		public async Task DeleteFileAsync(string fileUrl)
		{
			string fullFilePath = GetFilePhysicalPath(fileUrl);

			if (File.Exists(fullFilePath))
			{
				try
				{
					await Task.Yield();
					File.Delete(fullFilePath);
				}
				catch (Exception ex)
				{
					throw new Exception($"An error occurred while deleting the file: {fullFilePath}", ex);
				}
			}
			else
			{
				throw new FileNotFoundException($"File not found: {fullFilePath}");
			}
		}

		public string GetFilePhysicalPath(string fileUrl)
		{
			if (Uri.TryCreate(fileUrl, UriKind.Absolute, out Uri? fileUri))
			{
				string filePath = fileUri.AbsolutePath;
				string relativePath = filePath.Substring(filePath.IndexOf("/Uploads/", StringComparison.Ordinal));
				return Path.Combine(_webHostEnv.WebRootPath, relativePath.TrimStart('/'));
			}
			else
			{
				throw new ArgumentException("Invalid file URL format.", nameof(fileUrl));
			}
		}

		public string GetOriginalFileName(string filePath)
		{
			var fileNameWithGuid = Path.GetFileName(filePath);

			var fileNameParts = fileNameWithGuid.Split('-', 6);

			return fileNameParts.Length > 1 ? fileNameParts[5] : fileNameWithGuid;
		}
	}
}
