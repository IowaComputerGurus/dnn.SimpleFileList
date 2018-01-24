using System;

namespace ICG.Modules.SimpleFileList.Components
{
    /// <summary>
    ///     This class represents a single file that is available for
    ///     display within the module
    /// </summary>
    public class FileDisplay
    {
        /// <summary>
        ///     Gets or sets the full path.
        /// </summary>
        /// <value>The full path.</value>
        public string FullPath { get; set; }

        /// <summary>
        ///     Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; set; }

        /// <summary>
        ///     Gets or sets the last modified date.
        /// </summary>
        /// <value>The last modified date.</value>
        public DateTime LastModifiedDate { get; set; }
    }
}