using System.IO;

namespace URIMP
{
    public static class DataExtractor
    {
        public static string Extract(string path)
        {
            return File.Exists(path) ? File.ReadAllText(path) : "";
        }

        public static string[] ExtractMultiple(params string[] paths)
        {
            string[] datas = new string[paths.Length];

            for(int i = 0; i < datas.Length; i++)
            {
                datas[i] = File.Exists(paths[i]) ? File.ReadAllText(paths[i]) : "";
            }

            return datas;
        }
    }
}
