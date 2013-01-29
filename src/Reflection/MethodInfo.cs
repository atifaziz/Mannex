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
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using Collections.Generic;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="MethodInfo"/>.
    /// </summary>

    static partial class MethodInfoExtensions
    {
        /// <summary>
        /// Returns a sequence of method parameters paired with supplied 
        /// arguments. Type checking is not performed between parameter 
        /// types and arguments.
        /// </summary>
        /// <remarks>
        /// This method uses deferred execution. Moreover, if fewers 
        /// arguments are supplied than formal parameters defined on the 
        /// method then remaining arguments assume the default value of the 
        /// corresponding optional parameter or <see cref="Missing.Value"/> 
        /// if the parameter is not optional or has no default value defined.
        /// </remarks>

        public static IEnumerable<KeyValuePair<ParameterInfo, object>> ZipArgs(this MethodInfo method, params object[] args)
        {
            if (method == null) throw new ArgumentNullException("method");
            return ZipArgsImpl(method.GetParameters(), args);
        }

        static IEnumerable<KeyValuePair<ParameterInfo, object>> ZipArgsImpl(IList<ParameterInfo> parameters, IList<object> args)
        {
            Debug.Assert(parameters != null);

            return from i in Enumerable.Range(0, parameters.Count)
                   let param = parameters[i]
                   select param.AsKeyTo(args != null && i < args.Count
                                        ? args[i]
                                        : param.IsOptional && (ParameterAttributes.HasDefault == (param.Attributes & ParameterAttributes.HasDefault))
                                        ? param.DefaultValue
                                        : Missing.Value);
        }
    }
}