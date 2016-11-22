/*
 *  Copyright (c) 2006 - 2011 IowaComputerGurus Inc, All rights reserved.
 *  
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
 *  TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
 *  THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
 *  CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 *  DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ICG.Modules.SimpleFileList.Components
{
    /// <summary>
    /// This class is responsible for all of the IO activies to the file system.
    /// </summary>
    public static class FileUtility
    {
        /// <summary>
        /// Gets the available directories for the user to actually filter.
        /// </summary>
        /// <param name="rootDirectory">The root directory.</param>
        /// <returns>A collection of available directories</returns>
        public static IEnumerable<string> GetAvailableDirectories(string rootDirectory)
        {
            var rootInfo = new DirectoryInfo(rootDirectory);
            var subDirectoryInfo = rootInfo.GetDirectories("*.*", SearchOption.AllDirectories);
            var returnList = new List<string>();
            Array.ForEach(subDirectoryInfo, x => returnList.Add(x.FullName.Replace(rootDirectory, string.Empty)));
            return returnList;
        }

        /// <summary>
        /// Gets a listing of all files in a folder, filtering out a list of excluded file types
        /// </summary>
        /// <param name="folder">The folder to search.</param>
        /// <param name="excludeExtensions">The extensions that should be skipped.</param>
        /// <returns></returns>
        public static IEnumerable<FileDisplay> GetSafeFileList(string folder, IEnumerable<string> excludeExtensions, string sortOrder)
        {
            var folderInfo = new DirectoryInfo(folder);
            var fileInfo = folderInfo.GetFiles();
            var filteredList = from x in fileInfo
                               where !excludeExtensions.Contains(Path.GetExtension(x.FullName).ToLower())
                               select new FileDisplay { FileName = Path.GetFileName(x.FullName), FullPath = x.FullName, LastModifiedDate = x.LastWriteTime};
            
            //Sort as needed
            switch (sortOrder)
            {
                case "FA":
                    return filteredList.OrderBy(x => x.FileName);
                case "FD":
                    return filteredList.OrderByDescending(x => x.FileName);
                case "DA":
                    return filteredList.OrderBy(x => x.LastModifiedDate);
                case "DD":
                    return filteredList.OrderByDescending(x => x.LastModifiedDate);
            }
            
            //Otherwise go through as is
            return filteredList;
        }
    }
}