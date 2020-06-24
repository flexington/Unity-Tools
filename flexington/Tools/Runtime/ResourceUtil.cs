using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace flexington.Tools
{
    public static class ResourceUtil
    {
        /// <summary>
        /// Path to the Resource folder, used to remove it from the total path
        /// </summary>
        private static string _resourcePath;

        /// <summary>
        /// Actual root path where the function should start to search
        /// </summary>
        private static string _rootPath;

        public static ResourceEntity GetAllPrefabs(string subfolder = null)
        {
            // Get Resource path
            _resourcePath = $"{Application.dataPath}/Resources/";

            // Gather prefabs
            return GetPrefabs(_resourcePath + subfolder);
        }

        private static ResourceEntity GetPrefabs(string path)
        {
            ResourceEntity entity = new ResourceEntity();

            // Replace Backslash
            path = path.Replace("\\", "/");

            // Get Asset Path and remove resource folder path
            entity.AssetPath = path.Replace(_resourcePath, "");

            // Get Name of the folder
            entity.FolderName = path.Substring(path.LastIndexOf("/") + 1);

            // Get Icon, replace backslash and remove resource folder path
            entity.Icon = Directory
                .GetFiles(path, "*Icon*.prefab")
                .FirstOrDefault()?
                .Replace("\\", "/")
                .Replace(_resourcePath, "")
                .Replace(".prefab", "");

            // Get Prefabs, replace backslash, remove icon prefab and resource folder path
            Regex regex = new Regex("^((?!Icon).)*$");
            entity.Prefabs = Directory.GetFiles(path, "*.prefab")
                .Where(x => regex.IsMatch(x))
                .Select(x => x = x.Replace("\\", "/").Replace(_resourcePath, "").Replace(".prefab", ""))
                .ToList();

            // Do the same for all subfolders
            string[] directories = Directory.GetDirectories(path);
            if (directories.Length > 0) entity.SubFolders = new List<ResourceEntity>();
            for (int i = 0; i < directories.Length; i++)
            {
                entity.SubFolders.Add(GetPrefabs(directories[i]));
            }

            // Return resoucres
            return entity;
        }

        private static GameObject GetGameObject(string path)
        {
            return (GameObject)Resources.Load(path);
        }
    }
}