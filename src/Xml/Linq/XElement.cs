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

namespace Mannex.Xml.Linq
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using Collections.Generic;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="XElement"/>.
    /// </summary>

    static partial class XElementExtensions
    {
        /// <summary>
        /// Similar to <see cref="XmlNamespaceManager.GetNamespacesInScope"/>,
        /// returns a sequence of namespace names and their prefix that are
        /// in scope this element.
        /// </summary>

        public static IEnumerable<KeyValuePair<string, XNamespace>> GetNamespacesInScope(this XElement element)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));

            // With some inspiration from:
            // http://www.hanselman.com/blog/GetNamespacesFromAnXMLDocumentWithXPathDocumentAndLINQToXML.aspx

            return
                from e in element.AncestorsAndSelf()
                from d in from a in e.Attributes()
                          where a.IsNamespaceDeclaration
                          select XNamespace.Get(a.Value).AsKeyTo(a.Name.LocalName)
                group d.Value by d.Key into g
                select g.First().AsKeyTo(g.Key);
        }
    }
}
