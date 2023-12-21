using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;

namespace Thresh.Unity.Utility
{
    public static class FileUtil
    {
        public static string RemovePathPrefix(string path, string prefix)
        {
            int f = path.IndexOf(prefix);
            if (f != -1)
            {
                path = path.Remove(f, prefix.Length);
            }

            return path;
        }

        public static string RemovePathFileName(string path)
        {
            string f = Path.GetFileName(path);
            return path.Replace(f, "");
        }

        public static string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }

        public static string GetFileNameWithoutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        public static string GetExtension(string path)
        {
            int f = path.LastIndexOf(".");
            if (f != -1)
            {
                int l = path.Length - f;
                return path.Substring(f, l);
            }

            return "";
        }

        public static string RemoveExtension(string path)
        {
            int f = path.LastIndexOf(".");
            if (f != -1)
            {
                int l = path.Length - f;
                path = path.Remove(f, l);
            }

            return path;
        }

        public static string RemoveLastPathSep(string path)
        {
            string last = path.Substring(path.Length - 1);
            if (last == "/" || last == "\\")
            {
                path = path.Substring(0, path.Length - 1);
            }

            return path;
        }

        public static string GetLastDir(string path)
        {
            path = RemoveLastPathSep(path);
            int f = path.LastIndexOfAny(new char[] { '/', '\\' });
            if (f != -1)
            {
                int l = path.Length - f - 1;
                return path.Substring(f + 1, l);
            }

            return "";
        }

        public static void CopyFile(string src, string dest)
        {
            if (File.Exists(dest))
            {
                byte[] bytes = File.ReadAllBytes(src);
                File.WriteAllBytes(dest, bytes);
            }
            else
            {
                string destDir = Path.GetDirectoryName(dest);
                if (!Directory.Exists(destDir))
                {
                    CreateDirectory(destDir);
                }

                File.Copy(src, dest);
            }
        }

        public static void MoveFile(string src, string dest)
        {
            string destDir = Path.GetDirectoryName(dest);
            if (!Directory.Exists(destDir))
            {
                CreateDirectory(destDir);
            }

            File.Move(src, dest);
        }

        public static byte[] GetFileBytes(string src)
        {
            return File.ReadAllBytes(src);
        }

        public static void DeleteFile(string src)
        {
            File.Delete(src);
        }

        public static void DeleteFiles(string path, string ext)
        {
            if (!ext.Contains("."))
            {
                ext = "." + ext;
            }

            string[] files = Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++)
            {
                string f = files[i];
                if (f.Contains(ext))
                {
                    DeleteFile(f);
                }
            }
        }

        public static List<string> GetFilesInDirectory(string path, bool withSubDirs)
        {
            List<string> ret = new List<string>();

            GetFilesInDirectoryRecursively(path, withSubDirs, ref ret);

            return ret;
        }

        private static void GetFilesInDirectoryRecursively(string path, bool withSubDirs, ref List<string> ret)
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found:" + path);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            FileInfo[] files = dir.GetFiles();

            foreach (var file in files)
            {
                ret.Add(file.FullName);
            }

            if (withSubDirs)
            {
                foreach (var sub_dir in dirs)
                {
                    GetFilesInDirectoryRecursively(sub_dir.FullName, withSubDirs, ref ret);
                }
            }
        }

        public static void DirectoryCopy(string srcPath, string destPath, bool withSubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(srcPath);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: " + srcPath);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            if (!Directory.Exists(destPath))
            {
                Directory.CreateDirectory(destPath);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (var file in files)
            {
                string temp_path = Path.Combine(destPath, file.Name);
                file.CopyTo(temp_path, false);
            }

            if (withSubDirs)
            {
                foreach (var sub_dir in dirs)
                {
                    string temp_path = Path.Combine(destPath, sub_dir.Name);
                    DirectoryCopy(sub_dir.FullName, temp_path, withSubDirs);
                }
            }
        }

        public static bool Exists(string path)
        {
            return File.Exists(path);
        }

        public static bool Ignore(string path)
        {
            return path.Contains(".meta");
        }

        public static string ReadString(string path)
        {
            if (!Exists(path))
            {
                return "";
            }

            string text = "";
            StreamReader sr = File.OpenText(path);
            text = sr.ReadToEnd();
            sr.Close();

            return text;
        }

        public static byte[] ReadBytes(string path)
        {
            if (!Exists(path))
            {
                return null;
            }

            return File.ReadAllBytes(path);
        }

        private const string PATH_SPLIT_CHAR = "/";
        private const string FILTER_SPLIT_CHAR = "\\";

        public static List<string> GetFiles(string dir)
        {
            List<string> paths = new List<string>();
            foreach (var path in Directory.GetFiles(dir))
            {
                if (path.Contains(".meta"))
                {
                    continue;
                }

                paths.Add(path);
            }

            return paths;
        }

        /// <summary>
        /// 复制指定目录的所有文件,不包含子目录及子目录中的文件
        /// </summary>
        /// <param name="sourceDir">原始目录</param>
        /// <param name="targetDir">目标目录</param>
        /// <param name="overWrite">如果为true,表明覆盖同名文件,否则不覆盖</param>
        public static void CopyFiles(string sourceDir, string targetDir, bool overWrite)
        {
            CopyFiles(sourceDir,targetDir,overWrite,false);
        }

        /// <summary>
        /// 复制指定目录的所有文件
        /// </summary>
        /// <param name="sourceDir">原始目录</param>
        /// <param name="targetDir">目标目录</param>
        /// <param name="overWrite">如果为true,覆盖同名文件,否则不覆盖</param>
        /// <param name="copySubDir">如果为true,包含目录,否则不包含</param>
        public static void CopyFiles(string sourceDir, string targetDir, bool overWrite, bool copySubDir)
        {
            string temp_source_dir = sourceDir.Replace(FILTER_SPLIT_CHAR, PATH_SPLIT_CHAR);
            string temp_target_dir = targetDir.Replace(FILTER_SPLIT_CHAR, PATH_SPLIT_CHAR);

            //复制当前目录文件
            foreach (var sourceFileName in Directory.GetFiles(temp_source_dir))
            {
                string temp_src_file_name = sourceFileName.Replace(FILTER_SPLIT_CHAR, PATH_SPLIT_CHAR);
                string targetFileName = Path.Combine(temp_target_dir,
                    temp_src_file_name.Substring(temp_src_file_name.LastIndexOf(PATH_SPLIT_CHAR) + 1));

                if (Ignore(targetFileName))
                {
                    continue;
                }

                if (Exists(targetFileName))
                {
                    if (overWrite)
                    {
                        File.SetAttributes(targetFileName, FileAttributes.Normal);
                        File.Copy(temp_src_file_name, targetFileName, overWrite);
                    }
                }
                else
                {
                    File.Copy(temp_src_file_name, targetFileName, overWrite);
                }
            }

            //复制子目录
            if (copySubDir)
            {
                foreach (var sourceSubDir in Directory.GetDirectories(sourceDir))
                {
                    string temp_sub_src_dir = sourceSubDir.Replace(FILTER_SPLIT_CHAR, PATH_SPLIT_CHAR);
                    string targetSubDir = Path.Combine(temp_target_dir,
                        temp_sub_src_dir.Substring(temp_sub_src_dir.LastIndexOf(PATH_SPLIT_CHAR)));
                    if (!Directory.Exists(targetSubDir))
                    {
                        Directory.CreateDirectory(targetSubDir);
                    }
                    CopyFiles(temp_sub_src_dir,targetSubDir,overWrite,true);
                }
            }
        }

        /// <summary>
        /// 剪切指定目录的所有文件
        /// </summary>
        /// <param name="sourceDir">原始目录</param>
        /// <param name="targetDir">目标目录</param>
        /// <param name="overWrite">如果为true,覆盖同名文件，否则不覆盖</param>
        public static void MoveFiles(string sourceDir, string targetDir, bool overWrite)
        {
            
        }
        
        /// <summary>
        /// 剪切指定目录的所有文件
        /// </summary>
        /// <param name="sourceDir">原始目录</param>
        /// <param name="targetDir">目标目录</param>
        /// <param name="overWrite">如果为true,覆盖同名文件，否则不覆盖</param>
        /// <param name="moveSubDir">如果为true,包含目录,否则不包含</param>
        public static void MoveFiles(string sourceDir, string targetDir, bool overWrite, bool moveSubDir)
        {
            
        }

        /// <summary>
        /// 创建指定目录
        /// </summary>
        /// <param name="targetDir"></param>
        public static void CreateDirectory(string targetDir)
        {
            DirectoryInfo dir = new DirectoryInfo(targetDir);
            if (!dir.Exists)
            {
                dir.Create();
            }
        }

        /// <summary>
        /// 建立子目录
        /// </summary>
        /// <param name="parentDir">目录路径</param>
        /// <param name="subDirName">子目录名称</param>
        public static void CreateDirectory(string parentDir, string subDirName)
        {
            CreateDirectory(parentDir + PATH_SPLIT_CHAR + subDirName);
        }

        public static void DeleteDirectory(string targetDir)
        {
            DirectoryInfo dir_info = new DirectoryInfo(targetDir);
            if (dir_info.Exists)
            {
                DeleteFiles(targetDir,true);
                dir_info.Delete(true);
            }
        }
    }
}