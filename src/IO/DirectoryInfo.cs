#region License, Terms and Author(s)
//
// Mannex - Extension methods for .NET
// Copyright (c) 2009 Atif Aziz. All rights reserved.
//
//  Author(s):
//
//      Atif Aziz, http://www.raboof.com
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

namespace Mannex.IO
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="DirectoryInfo"/>.
    /// </summary>

    static partial class DirectoryInfoExtensions
    {
        const string DefaultSearchPattern = "*";

        /// <summary>
        /// Returns all the parents of the directory.
        /// </summary>
        /// <remarks>
        /// This method uses deferred execution. In addition, it does not 
        /// check for the existence of the directory or its parents.
        /// </remarks>

        public static IEnumerable<DirectoryInfo> Parents(this DirectoryInfo dir)
        {
            return dir.SelfAndParents().Skip(1);
        }

        /// <summary>
        /// Returns the directory and all its parents.
        /// </summary>
        /// <remarks>
        /// This method uses deferred execution. In addition, it does not 
        /// check for the existence of the directory or its parents.
        /// </remarks>

        public static IEnumerable<DirectoryInfo> SelfAndParents(this DirectoryInfo dir)
        {
            if (dir == null) throw new ArgumentNullException("dir");
            return SelfAndParentsImpl(dir);
        }

        static IEnumerable<DirectoryInfo> SelfAndParentsImpl(DirectoryInfo dir)
        {
            for (; dir != null; dir = dir.Parent)
                yield return dir;
        }


        /// <summary>
        /// Same as <see cref="DirectoryInfo.GetFiles()"/>
        /// except filters hidden and system files.
        /// </summary>

        public static FileInfo[] GetVisibleFiles(this DirectoryInfo dir)
        {
            return GetVisibleFiles(dir, DefaultSearchPattern);
        }

        /// <summary>
        /// Same as <see cref="DirectoryInfo.GetFiles(String)"/>
        /// except filters hidden and system files.
        /// </summary>

        public static FileInfo[] GetVisibleFiles(this DirectoryInfo dir, string searchPattern)
        {
            return GetVisibleFiles(dir, searchPattern, SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Same as <see cref="DirectoryInfo.GetFiles(String,SearchOption)"/>
        /// except filters hidden and system files.
        /// </summary>

        public static FileInfo[] GetVisibleFiles(this DirectoryInfo dir, string searchPattern, SearchOption searchOption)
        {
            return dir.GetFiles(searchPattern, searchOption)
                      .Where(e => e.IsUserVisible())
                      .ToArray();
        }

        /// <summary>
        /// Same as <see cref="DirectoryInfo.GetDirectories()"/>
        /// except filters hidden and system files.
        /// </summary>

        public static DirectoryInfo[] GetVisibleDirectories(this DirectoryInfo dir)
        {
            return GetVisibleDirectories(dir, DefaultSearchPattern);
        }

        /// <summary>
        /// Same as <see cref="DirectoryInfo.GetDirectories(String)"/>
        /// except filters hidden and system directories.
        /// </summary>

        public static DirectoryInfo[] GetVisibleDirectories(this DirectoryInfo dir, string searchPattern)
        {
            return GetVisibleDirectories(dir, searchPattern, SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Same as <see cref="DirectoryInfo.GetDirectories(String,SearchOption)"/>
        /// except filters hidden and system directories.
        /// </summary>

        public static DirectoryInfo[] GetVisibleDirectories(this DirectoryInfo dir, string searchPattern, SearchOption searchOption)
        {
            return dir.GetDirectories(searchPattern, searchOption)
                      .Where(e => e.IsUserVisible())
                      .ToArray();
        }

        /// <summary>
        /// Same as <see cref="DirectoryInfo.GetFileSystemInfos()"/>
        /// except filters hidden and system entries.
        /// </summary>

        public static FileSystemInfo[] GetVisibleFileSystemInfo(this DirectoryInfo dir)
        {
            return GetVisibleFileSystemInfo(dir, DefaultSearchPattern);
        }

        /// <summary>
        /// Same as <see cref="DirectoryInfo.GetFileSystemInfos(String)"/>
        /// except filters hidden and system entries.
        /// </summary>

        public static FileSystemInfo[] GetVisibleFileSystemInfo(this DirectoryInfo dir, string searchPattern)
        {
            return dir.GetFileSystemInfos(searchPattern)
                      .Where(e => e.IsUserVisible())
                      .ToArray();
        }

        #if NET4

        /// <summary>
        /// Same as <see cref="DirectoryInfo.GetFileSystemInfos(String,SearchOption)"/>
        /// except filters hidden and system entries.
        /// </summary>

        public static FileSystemInfo[] GetVisibleFileSystemInfo(this DirectoryInfo dir, string searchPattern, SearchOption searchOption)
        {
            return dir.GetFileSystemInfos(searchPattern, searchOption)
                      .Where(e => e.IsUserVisible())
                      .ToArray();
        }

        /// <summary>
        /// Same as <see cref="DirectoryInfo.EnumerateFiles()"/>
        /// except filters hidden and system files.
        /// </summary>

        public static IEnumerable<FileInfo> EnumerateVisibleFiles(this DirectoryInfo dir)
        {
            return EnumerateVisibleFiles(dir, DefaultSearchPattern);
        }

        /// <summary>
        /// Same as <see cref="DirectoryInfo.EnumerateFiles(String)"/>
        /// except filters hidden and system files.
        /// </summary>

        public static IEnumerable<FileInfo> EnumerateVisibleFiles(this DirectoryInfo dir, string searchPattern)
        {
            return EnumerateVisibleFiles(dir, searchPattern, SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Same as <see cref="DirectoryInfo.EnumerateFiles(String,SearchOption)"/>
        /// except filters hidden and system files.
        /// </summary>

        public static IEnumerable<FileInfo> EnumerateVisibleFiles(this DirectoryInfo dir, string searchPattern, SearchOption searchOption)
        {
            return from e in dir.EnumerateFiles(searchPattern, searchOption)
                   where e.IsUserVisible()
                   select e;
        }

        /// <summary>
        /// Same as <see cref="DirectoryInfo.EnumerateDirectories()"/>
        /// except filters hidden and system directories.
        /// </summary>

        public static IEnumerable<DirectoryInfo> EnumerateVisibleDirectories(this DirectoryInfo dir)
        {
            return EnumerateVisibleDirectories(dir, DefaultSearchPattern);
        }

        /// <summary>
        /// Same as <see cref="DirectoryInfo.EnumerateDirectories(String)"/>
        /// except filters hidden and system directories.
        /// </summary>

        public static IEnumerable<DirectoryInfo> EnumerateVisibleDirectories(this DirectoryInfo dir, string searchPattern)
        {
            return EnumerateVisibleDirectories(dir, searchPattern, SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Same as <see cref="DirectoryInfo.EnumerateDirectories(String,SearchOption)"/>
        /// except filters hidden and system directories.
        /// </summary>

        public static IEnumerable<DirectoryInfo> EnumerateVisibleDirectories(this DirectoryInfo dir, string searchPattern, SearchOption searchOption)
        {
            return from e in dir.EnumerateDirectories(searchPattern, searchOption)
                   where e.IsUserVisible()
                   select e;
        }

        /// <summary>
        /// Same as <see cref="DirectoryInfo.EnumerateFileSystemInfos()"/>
        /// except filters hidden and system entries.
        /// </summary>

        public static IEnumerable<FileSystemInfo> EnumerateVisibleFileSystemInfo(this DirectoryInfo dir)
        {
            return EnumerateVisibleFileSystemInfo(dir, DefaultSearchPattern);
        }

        /// <summary>
        /// Same as <see cref="DirectoryInfo.EnumerateFileSystemInfos(String)"/>
        /// except filters hidden and system entries.
        /// </summary>

        public static IEnumerable<FileSystemInfo> EnumerateVisibleFileSystemInfo(this DirectoryInfo dir, string searchPattern)
        {
            return EnumerateVisibleFileSystemInfo(dir, searchPattern, SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Same as <see cref="DirectoryInfo.EnumerateFileSystemInfos(String,SearchOption)"/>
        /// except filters hidden and system entries.
        /// </summary>

        public static IEnumerable<FileSystemInfo> EnumerateVisibleFileSystemInfo(this DirectoryInfo dir, string searchPattern, SearchOption searchOption)
        {
            return from e in dir.EnumerateFileSystemInfos(searchPattern, searchOption)
                   where e.IsUserVisible()
                   select e;
        }

        #endif
    }
}