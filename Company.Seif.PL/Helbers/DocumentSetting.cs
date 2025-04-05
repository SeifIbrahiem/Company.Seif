namespace Company.Seif.PL.Helbers
{
    namespace Company.Seif.PL.Helbers
    {
        public static class DocumentSetting
        {
            //1.Upload
            public static string UploadFile(IFormFile file , string folderName)
            {

                //1.Get Folder Loation
               // string folderpath = "C:\\Users\\ADMIN\\source\\repos\\Company.Seif\\Company.Seif.PL\\wwwroot\\files\\images\\"+folderName;
                
               // var folderpath = Directory.GetCurrentDirectory()+ "\\wwwroot\\files\\"+ folderName;

                var folderpath=Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName);
               
                //2.Get File Name And Make It Unique

                var filename = $"{Guid.NewGuid()}{file.FileName}";
                
                //File path

                var filepath = Path.Combine(folderpath, filename);

               using var fileStream= new FileStream(filepath , FileMode.Create);

                file.CopyTo(fileStream);

                return filename ;
            }
            //2.Delete


            public static void DeleteFile(string filName, string folderName)
            {

              var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName, filName);

                if (File.Exists(filePath)) 
                    File.Delete(filePath);
            }


        }

    }
}
