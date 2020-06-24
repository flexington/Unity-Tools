using System;
using System.Collections.Generic;

namespace flexington.Tools
{
    [Serializable]
    public struct ResourceEntity
    {
        public string AssetPath;
        public string FolderName;
        public string Icon;
        public List<string> Prefabs;
        public List<ResourceEntity> SubFolders;
    }
}