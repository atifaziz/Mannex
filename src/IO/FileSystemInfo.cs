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
    using System;
    using System.IO;

    /// <summary>
    /// Extension methods for <see cref="FileSystemInfo"/>.
    /// </summary>

    static partial class FileSystemInfoExtensions
    {
        /// <summary>
        /// Determines if the file system entry should be generally hidden
        /// (has <see cref="FileAttributes.Hidden"/> or
        /// <see cref="FileAttributes.System"/> set) in a user interface
        /// based on its attributes.
        /// </summary>
        /// <remarks>
        /// On Unix systems, if <see cref="FileSystemInfo.Name"/> starts with
        /// a period or dot (.) then it is considered user-invisible.
        /// </remarks>

        public static bool IsUserVisible(this FileSystemInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            return 0 == (info.Attributes & (FileAttributes.Hidden | FileAttributes.System))
                && (!Environment.OSVersion.IsUnix() || info.Name.TryCharAt(0) != '.');
        }

        /// <summary>
        /// Determines if the file system entry has at least the the given
        /// bits of <see cref="FileAttributes"/> set.
        /// </summary>

        public static bool HasAttributes(this FileSystemInfo info, FileAttributes attributes)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            return (info.Attributes & attributes) == attributes;
        }

        /// <summary>
        /// Returns <see cref="DirectoryInfo.Parent"/> when the file system
        /// entry represents a directory and <see cref="FileInfo.Directory"/>
        /// otherwise.
        /// </summary>
        /// <remarks>
        /// This method is useful for getting the parent of a
        /// <see cref="FileSystemInfo"/> object irrespective of it being a
        /// file (<see cref="FileInfo"/>) or directory
        /// (<see cref="DirectoryInfo"/>).
        /// </remarks>

        public static DirectoryInfo GetParentDirectory(FileSystemInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            var dir = info as DirectoryInfo;
            return dir != null ? dir.Parent : ((FileInfo) info).Directory;
        }

        /// <summary>
        /// Reconstructs <see cref="FileSystemInfo.FullName"/> of the file
        /// system entry by walking the entry and all its parents
        /// (except root) and getting the original name as stored in the
        /// file system.
        /// </summary>
        /// <remarks>
        /// This is useful with case-insensitive file systems like NTFS where
        /// the user can specify the path to a file or directory in one case
        /// when its stored in another. The root is not reconstructed so paths
        /// using mapped local drive letters or UNC paths under Windows, for
        /// example, are returned verbatim.
        /// </remarks>

        public static string ReconstructFullName(this FileSystemInfo info)
        {
            // Inspiration & credit:
            // http://stackoverflow.com/a/326153/6682

            var parent = GetParentDirectory(info);
            return parent != null
                 ? Path.Combine(parent.ReconstructFullName(), parent.GetFileSystemInfos(info.Name)[0].Name)
                 : info.FullName;
        }
    }
}