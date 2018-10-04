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

namespace Mannex.ComponentModel.Design
{
    #region Imports

    using System;
    using System.ComponentModel.Design;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="IServiceContainer"/>.
    /// </summary>

    static partial class IServiceContainerExtensions
    {
        /// <summary>
        /// Adds the specified service to the service container.
        /// </summary>

        public static void AddService<T>(this IServiceContainer container, T service)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            container.AddService(typeof(T), service);
        }
    }
}
