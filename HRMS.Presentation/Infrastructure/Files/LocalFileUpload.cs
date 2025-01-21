namespace HRMS.Presentation.Infrastructure.Files
{
	public class LocalFileUpload : IFileUpload
	{
		private readonly IWebHostEnvironment _webHostEnv;

		public LocalFileUpload(IWebHostEnvironment webHostEnv)
		{
			_webHostEnv = webHostEnv;
		}

		public async Task<string> UploadFileAsync(IFormFile file, string innerDir = "")
		{
			if (file.Length > 0)
			{
				string uploadFolder = Path.Combine(_webHostEnv.WebRootPath, "Uploads", innerDir);
				Directory.CreateDirectory(uploadFolder);

				string uniqueFileName = Guid.NewGuid().ToString() + "-" + file.FileName;
				string file_path = Path.Combine(uploadFolder, uniqueFileName);

				using (var fileStream = new FileStream(file_path, FileMode.Create))
				{
					await file.CopyToAsync(fileStream);
				}

				return file_path;
			}
			else
			{
				throw new Exception("No file is provided.");
			}
		}

		public async Task DeleteFileAsync(string filePath)
		{
			if (File.Exists(filePath))
			{
				await Task.Run(() => File.Delete(filePath));
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
