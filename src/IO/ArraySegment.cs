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
    using System.IO;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="ArraySegment{T}"/>.
    /// </summary>

    public static class ArraySegmentExtensions
    {
        /// <summary>
        /// Creates a read-only stream on top of the supplied buffer.
        /// </summary>
        
        public static Stream OpenRead(this ArraySegment<byte> buffer)
        {
            return buffer.Array.OpenRead(buffer.Offset, buffer.Count);
        }

        /// <summary>
        /// Creates a read-write stream on top of the supplied buffer.
        /// </summary>

        public static Stream OpenReadWrite(this ArraySegment<byte> buffer)
        {
            return buffer.Array.OpenReadWrite(buffer.Offset, buffer.Count);
        }
    }
}