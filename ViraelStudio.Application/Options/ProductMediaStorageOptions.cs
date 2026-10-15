using System;
using System.Collections.Generic;
using System.Text;

namespace ViraelStudio.Application.Options
{
    public class ProductMediaStorageOptions
    {
        public string DirectoryName { get; set; } = String.Empty;
        public int MaxFileSizeMb { get; set; } = 50;
        public string[] AllowedImageExtensions { get; set; } = [];
        public string[] AllowedVideoExtensions { get; set; } = [];
    }
}
