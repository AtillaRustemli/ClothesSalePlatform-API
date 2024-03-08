namespace ClothesSalePlatform.Helpers.Extensions
{
    public static class FileExtensions
    {

        public static bool CheckSize(this IFormFile file,int size)
        {
            return size*1024>file.Length;
        }
        public static bool CheckImage(this IFormFile file,string folder)
        {
            return file.ContentType.Contains(folder);
        }

        public static string SaveImage(this IFormFile file,string folder)
        { 
            string fileName=Guid.NewGuid()+file.FileName;
            var path=Path.Combine(Directory.GetCurrentDirectory(),folder,fileName);
            using FileStream fileStream = new(path, FileMode.Create);
            file.CopyTo(fileStream);


            return fileName;
        }

    }
}
