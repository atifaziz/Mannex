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

namespace Mannex.Reflection
{
    #region Imports

    using System;
    using System.IO;
    using System.Reflection;
    using System.Text;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="Assembly"/>.
    /// </summary>

    static partial class AssemblyExtensions
    {
        /// <summary>
        /// Loads the specified manifest resource, scoped by the namespace 
        /// of the specified type, from this assembly and returns
        /// it ready for reading as <see cref="TextReader"/>.
        /// </summary>

        public static TextReader GetManifestResourceReader(this Assembly assembly, Type type, string name)
        {
            return GetManifestResourceReader(assembly, type, name, null);
        }

        /// <summary>
        /// Loads the specified manifest resource, scoped by the namespace 
        /// of the specified type, from this assembly and returns
        /// it ready for reading as <see cref="TextReader"/>. A parameter
        /// specifies the text encoding to be used for reading.
        /// </summary>

        public static TextReader GetManifestResourceReader(this Assembly assembly, Type type, string name, Encoding encoding)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            var stream = assembly.GetManifestResourceStream(type, name);
            if (stream == null)
                return null;
            return encoding == null 
                 ? new StreamReader(stream, true) 
                 : new StreamReader(stream, encoding);
        }

        /// <summary>
        /// Loads the specified manifest resource and returns it as a string, 
        /// scoped by the namespace  of the specified type, from this assembly. 
        /// </summary>

        public static string GetManifestResourceString(this Assembly assembly, Type type, string name)
        {
            return GetManifestResourceString(assembly, type, name, null);
        }

        /// <summary>
        /// Loads the specified manifest resource and returns it as a string, 
        /// scoped by the namespace  of the specified type, from this assembly. 
        /// A parameter specifies the text encoding to be decode the resource
        /// bytes into text.
        /// </summary>

        public static string GetManifestResourceString(this Assembly assembly, Type type, string name, Encoding encoding)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            using (var reader = assembly.GetManifestResourceReader(type, name))
                return reader != null ? reader.ReadToEnd() : null;
        }
    }
}
