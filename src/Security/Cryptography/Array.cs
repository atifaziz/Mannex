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

namespace Mannex.Security.Cryptography
{
    #region Imports

    using System;
    using System.Security.Cryptography;

    #endregion

    /// <summary>
    /// Data protection extension method for <see cref="Array"/> of bytes.
    /// </summary>

    static partial class ArrayExtensions
    {
        /// <summary>
        /// Protects an array of bytes containing sensitive by encrypting 
        /// the data based on the user profile or machine.
        /// </summary>

        public static byte[] Protect(this byte[] bytes, DataProtectionScope scope)
        {
            return Protect(bytes, scope, null);
        }

        /// <summary>
        /// Protects an array of bytes containing sensitive by encrypting 
        /// the data based on the user profile or machine. An additional
        /// parameter specifies the entropy to randomize the encrpytion.
        /// </summary>

        public static byte[] Protect(this byte[] bytes, DataProtectionScope scope, byte[] entropy)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));
            return ProtectedData.Protect(bytes, entropy, scope);
        }

        /// <summary>
        /// Unprotects an array of bytes containing sensitive by decrypting 
        /// the data based on the user profile or machine.
        /// </summary>

        public static byte[] Unprotect(this byte[] bytes, DataProtectionScope scope)
        {
            return Unprotect(bytes, scope, null);
        }

        /// <summary>
        /// Unprotects an array of bytes containing sensitive by decrypting 
        /// the data based on the user profile or machine. An additional
        /// parameter specifies the entropy to randomize the encrpytion.
        /// </summary>

        public static byte[] Unprotect(this byte[] bytes, DataProtectionScope scope, byte[] entropy)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));
            return ProtectedData.Unprotect(bytes, entropy, scope);
        }
    }
}
