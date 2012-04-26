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

namespace Mannex
{
    public static partial class ActionExtensions { }
    public static partial class ArrayExtensions { }
    public static partial class BooleanExtensions { }
    public static partial class DateTimeExtensions { }
    public static partial class EnumExtensions { }
    public static partial class EventHandlerExtensions { }
    public static partial class ExceptionExtensions { }
    public static partial class FuncExtensions { }
    public static partial class Int32Extensions { }
    public static partial class Int64Extensions { }
    public static partial class PredicateExtensions { }
    public static partial class StringExtensions { }
    public static partial class TypeExtensions { }
    public static partial class UriExtensions { }
    public static partial class IServiceProviderExtensions { }
    
    namespace Collections.Specialized
    {
        public static partial class NameValueCollectionExtensions { }
    }
    
    namespace Collections.Generic
    {
        public static partial class DictionaryExtensions { }
        public static partial class IComparerExtensions { }
        public static partial class ListExtensions { }
        public static partial class PairingExtensions { }
    }

    namespace ComponentModel
    {
        public static partial class INotifyPropertyChangedExtensions { }

        namespace Design
        {
            public static partial class IServiceContainerExtensions { }
        }
    }

    namespace Data
    {
        public static partial class IDataReaderExtensions { }
        public static partial class IDataRecordExtensions { }
    }

    namespace Diagnostics
    {
        public static partial class ProcessExtensions { }
    }

    namespace Globalization
    {
        public static partial class CalendarExtensions { }
        public static partial class DateTimeFormatInfoExtensions { }
    }

    namespace Json
    {
        public static partial class NameValueCollectionExtensions { }
        public static partial class StringExtensions { }
    }

    namespace Net
    {
        public static partial class WebClientExtensions { }

        namespace Mime
        {
            public static partial class ContentTypeExtensions { }
        }
    }

    namespace IO
    {
        public static partial class ArrayExtensions { }
        public static partial class ArraySegmentExtensions { }
        public static partial class FileInfoExtensions { }
        public static partial class StreamExtensions { }
        public static partial class StringExtensions { }
        public static partial class TextReaderExtensions { }
    }

    namespace Reflection
    {
        public static partial class AssemblyExtensions { }
        public static partial class ICustomAttributeProviderExtensions { }
    }

    namespace Security.Cryptography
    {
        public static partial class ArrayExtensions { }
    }

    namespace Text.RegularExpressions
    {
        public static partial class MatchExtensions { }
        public static partial class StringExtensions { }
    }

    namespace Threading
    {
        public static partial class TimeSpanExtensions { }
        
        #if NET4
        public static partial class TaskFactoryExtensions { }
        #endif
    }

    namespace Web
    {
        public static partial class HtmlStringExtensions { }
        public static partial class NameValueCollectionExtensions { }
        public static partial class UriExtensions { }

        namespace Script
        {
            namespace Serialization
            {
                public static partial class ObjectExtensions { }
            }
        }

        namespace UI
        {
            public static partial class ControlExtensions { }
            public static partial class DataBindingExtensions { }
        }
    }
}
