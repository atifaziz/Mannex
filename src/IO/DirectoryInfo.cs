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
    using System.Text;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="DirectoryInfo"/>.
    /// </summary>

    static partial class DirectoryInfoExtensions
    {
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
    }
}