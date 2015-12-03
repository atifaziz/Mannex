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
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Extension methods for <see cref="IDisposable"/> objects.
    /// </summary>

    static partial class DisposableExtensions
    {
        /// <summary>
        /// Performs application-defined tasks associated with freeing,
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        /// <remarks>
        /// This extension is useful when a type explicitly implements
        /// <see cref="IDisposable.Dispose"/> and would require a static
        /// type binding to <see cref="IDisposable"/> in order to be able to
        /// just call <see cref="IDisposable.Dispose"/>.
        /// </remarks>

        [DebuggerStepThrough]
        public static void Dispose(this IDisposable disposable)
        {
            if (disposable == null) throw new ArgumentNullException("disposable");
            disposable.Dispose();
        }
    }
}