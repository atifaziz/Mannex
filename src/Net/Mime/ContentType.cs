﻿#region License, Terms and Author(s)
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

namespace Mannex.Net.Mime
{
    #region Imports

    using System;
    using System.Net.Mime;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="ContentType"/>.
    /// </summary>

    static partial class ContentTypeExtensions
    {
        /// <summary>
        /// Determines whether content type is plain text.
        /// </summary>

        public static bool IsPlainText(this ContentType contentType)
        {
            if (contentType == null) throw new ArgumentNullException("contentType");
            return EqualsOrdinalIgnoreCase(MediaTypeNames.Text.Plain, contentType.MediaType);
        }

        /// <summary>
        /// Determines whether content media type is text.
        /// </summary>

        public static bool IsText(this ContentType contentType)
        {
            if (contentType == null) throw new ArgumentNullException("contentType");
            return EqualsOrdinalIgnoreCase("text", GetMediaBaseType(contentType));
        }

        /// <summary>
        /// Determines whether content type identifies an HTML document.
        /// </summary>

        public static bool IsHtml(this ContentType contentType)
        {
            if (contentType == null) throw new ArgumentNullException("contentType");
            return EqualsOrdinalIgnoreCase(MediaTypeNames.Text.Html, contentType.MediaType);
        }

        /// <summary>
        /// Determines whether content media type identifies an image.
        /// </summary>

        public static bool IsImage(this ContentType contentType)
        {
            if (contentType == null) throw new ArgumentNullException("contentType");
            return EqualsOrdinalIgnoreCase("image", GetMediaBaseType(contentType));
        }

        /// <summary>
        /// Gets the base media of the content type, e.g. text from text/plain.
        /// </summary>

        public static string GetMediaBaseType(this ContentType contentType)
        {
            if (contentType == null) throw new ArgumentNullException("contentType");
            var mediaType = contentType.MediaType;
            return mediaType.Substring(0, mediaType.IndexOf('/'));
        }

        /// <summary>
        /// Gets the media sub-type of the content type, e.g. plain from text/plain.
        /// </summary>

        public static string GetMediaSubType(this ContentType contentType)
        {
            if (contentType == null) throw new ArgumentNullException("contentType");
            var mediaType = contentType.MediaType;
            return mediaType.Substring(mediaType.IndexOf('/') + 1);
        }

        private static bool EqualsOrdinalIgnoreCase(string left, string right)
        {
            return left.Equals(right, StringComparison.OrdinalIgnoreCase);
        }
    }
}