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
    using System.Linq.Expressions;
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

        /// <summary>
        /// Compiles a function that can be used to call a static method 
        /// without the performance penalties of late-binding and invocation.
        /// </summary>
        /// <remarks>
        /// Build targeting .NET Framework 3.5 does not support static 
        /// methods with a return type of <see cref="System.Void"/>. Use 
        /// <see cref="Type.Missing"/> for an argument to the invoker to 
        /// have the default value for an optional argument to be filled in.
        /// </remarks>
        
        public static Func<object[], object> CompileStaticInvoker(this MethodInfo method)
        {
            if (method == null) throw new ArgumentNullException("method");
            if (!method.IsStatic) throw new ArgumentException(null, "method");
            
            var returnsVoid = method.ReturnType == typeof(void);
        #if !NET4
            if (returnsVoid) throw new ArgumentException(null, "method");
        #endif

            var argsParameter = Expression.Parameter(typeof(object[]), "args");

            var parameters = method.GetParameters();
            
            var args = 
                from p in parameters
                let arg = Expression.ArrayIndex(argsParameter, Expression.Constant(p.Position))
                let carg = p.ParameterType == arg.Type
                         ? (Expression) arg
                         : Expression.Convert(arg, p.ParameterType)
                select p.HasDefaultValue
                     ? Expression.Condition(Expression.Equal(arg, Expression.Constant(Type.Missing)), Expression.Constant(p.DefaultValue), carg)
                     : carg;
            
            var e = (Expression) Expression.Call(method, args.ToArray());
            if (!returnsVoid) 
                e = Expression.Convert(e, typeof(object));
        #if NET4
            var statements = new List<Expression>
            {
                Expression.IfThen(Expression.MakeBinary(ExpressionType.NotEqual, Expression.ArrayLength(argsParameter), Expression.Constant(parameters.Length)), Expression.Throw(Expression.Constant(new ArgumentException(null, argsParameter.Name)))),
                e,
            };
            if (returnsVoid) 
                statements.Add(Expression.Constant(null));
            e = Expression.Block(statements);
        #endif
            
            return Expression.Lambda<Func<object[], object>>(e, argsParameter).Compile();
        }
    }
}