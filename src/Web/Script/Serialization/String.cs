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

namespace Mannex.Web.Script.Serialization
{
    #region Imports

    using System;
    using System.Diagnostics;
    using System.Web.Script.Serialization;
    using System.Text;

    #endregion

    static partial class StringExtensions
    {
        #pragma warning disable 618
        
        // warning CS0618: 
        // 'System.Web.Script.Serialization.JavaScriptSerializer.JavaScriptSerializer()' is obsolete: 
        // 'The recommended alternative is System.Runtime.Serialization.DataContractJsonSerializer.'
        
        private static readonly JavaScriptSerializer _serializer = new JavaScriptSerializer();
        
        #pragma warning restore 618

        public static string ToJsonString(this string str)
        {
            return _serializer.Serialize(str);
        }

        public static string ToJsonString(this string str, char quote)
        {
            if (quote == '"')
                return str.ToJsonString();

            if (quote != '\'')
                throw new ArgumentException("Quote character must be a single or double quote.", "quote");

            var sb = new StringBuilder(str.Length + 10);
            str.BuildJsonStringTo(sb);
            Debug.Assert(str.Length >= 2);
            sb[0] = quote;
            sb[sb.Length - 1] = quote;
            return sb.ToString();
        }

        public static void BuildJsonStringTo(this string str, StringBuilder output)
        {
            if (output == null) throw new ArgumentNullException("output");
            _serializer.Serialize(str, output);
        }
    }
}
