namespace HRMS.Presentation.Infrastructure.Files
{
	public interface IFileUpload
	{
		public Task<string> UploadFileAsync(IFormFile file, string innerDir = "");
		public Task DeleteFileAsync(string filePath);
		public string GetOriginalFileName(string filePath);
	}
}
