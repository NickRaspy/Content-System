using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class PathSplitter
{
    public static string SplitPath(string path, int parentFolderAmount)
    {
        string splitPath = "";

        var splittedPaths = path.Split('\\');

        if(parentFolderAmount > splittedPaths.Length) parentFolderAmount = splittedPaths.Length - 1;

        List<string> reversedPaths = new(splittedPaths.Reverse());

        if (parentFolderAmount == 0) return reversedPaths[0];

        splitPath += reversedPaths[0];

        for(int i = 1; i <= parentFolderAmount; i++)
        {
            splitPath = Path.Combine(reversedPaths[i], splitPath);
        }

        return splitPath;
    }
}
