using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ZCS_Common
{
    public static class PathHelper
    {
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string CreateDirectory(string path)
        {
            try
            {
                var info = new DirectoryInfo(path);
                if (!info.Exists)
                {
                    info.Create();
                }
            }
            catch
            {
            }
            return path;
        }

        /// <summary>
        /// 计算相对路径
        /// </summary>
        /// <param name="mainDirPath">主目录</param>
        /// <param name="absoluteFilePath">绝对路径</param>
        /// <returns></returns>
        public static string EvaluateRelativePath(string mainDirPath, string absoluteFilePath)
        {
            string[] firstPathParts = mainDirPath.Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);
            string[] secondPathParts = absoluteFilePath.Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);

            int sameCounter = 0;
            int partsCount = Math.Min(firstPathParts.Length, secondPathParts.Length);
            for (int i = 0; i < partsCount; i++)
            {
                if (String.Compare(firstPathParts[i], secondPathParts[i], true) != 0)
                {
                    break;
                }
                sameCounter++;
            }

            if (sameCounter == 0)
            {
                return absoluteFilePath;
            }

            string newPath = string.Empty;
            for (int i = sameCounter; i < firstPathParts.Length; i++)
            {
                if (i > sameCounter)
                {
                    newPath += Path.DirectorySeparatorChar;
                }
                newPath += "..";
            }
            if (newPath.Length == 0)
            {
                newPath = ".";
            }
            for (int i = sameCounter; i < secondPathParts.Length; i++)
            {
                newPath += Path.DirectorySeparatorChar;
                newPath += secondPathParts[i];
            }
            return newPath;
        }

        /// <summary>
        /// 获取指定路径下的文件信息
        /// </summary>
        /// <param name="drictPath">文件路径</param>
        /// <returns></returns>
        public static FileSystemInfo[] GetFilesInfo(string drictPath)
        {
            DirectoryInfo theFolder = new DirectoryInfo(drictPath);

            if (!theFolder.Exists)
            {
                return null;
            }

            DirectoryInfo dir = theFolder as DirectoryInfo;

            //如果不是目录
            if (dir == null)
            {
                return null;
            }

            FileSystemInfo[] files = dir.GetFileSystemInfos();
            return files;
        }

    }
}
