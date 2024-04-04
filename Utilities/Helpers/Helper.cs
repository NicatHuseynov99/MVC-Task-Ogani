namespace Ogani.Utilities.Helpers
{
    public class Helper
    {
        public static string GetFilePath(string root, string folder, string fileName)
        {
            return Path.Combine(root, folder, fileName);
        }
        public static void DeleteFile(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
        public enum UserRoles
        {
            Member,
            Admin
        }
    }
}
