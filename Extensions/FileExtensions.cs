namespace BinaAz.Extensions
{
    public static class FileExtensions
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType == "image/jpg" ||
                file.ContentType == "image/jpeg" ||
                file.ContentType == "image/png" ||
                file.ContentType == "image/webp" ||
                file.ContentType == "image/gif";
        }

        public static bool IsCV(this IFormFile CVfile)
        {
            return CVfile.ContentType == "application/pdf" ||
             CVfile.ContentType == "application/doc" ||
             CVfile.ContentType == "application/docx" ||
             CVfile.ContentType == "application/pptx";

        }
        public async static Task<string> SaveAsync(this IFormFile file, string root)
        {
            string path = Path.Combine(@"wwwroot/images", root);

            string fileName = Path.Combine(Guid.NewGuid().ToString() + Path.GetFileName(file.FileName));

            string resultPath = Path.Combine(path, fileName);

            using (FileStream fileStream = new FileStream(resultPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileName;
        }
    }
}