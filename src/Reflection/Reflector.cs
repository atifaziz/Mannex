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
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    static class Reflector
    {
        public static Func<object, Func<T1, TResult>> GetMethodBinderFromCallTemplate<T, T1, TResult>(Expression<Func<T, TResult>> template)
        {
            if (template == null) throw new ArgumentNullException("template");
        
            if (template.Body.NodeType != ExpressionType.Call) 
                throw new ArgumentException("Lambda expression body is not a method call.", "template");

            var templateMethod = ((MethodCallExpression) template.Body).Method;

            if (templateMethod.DeclaringType != typeof(T)) 
                throw new ArgumentException(string.Format("Lambda expression body must be a call on an object of {0} type.", typeof(T).FullName), "template");
            if (templateMethod.IsStatic) 
                throw new ArgumentException("Lambda expression body is a static method call when it must be an instance method call.", "template");
        
            var name = templateMethod.Name;
            var templateParameters = templateMethod.GetParameters();
            var parameterTypes = new[] { typeof(T1) };
        
            if (templateParameters.Length != parameterTypes.Length) 
                throw new ArgumentException(string.Format("Lambda expression body must be an instance method call accepting an argument count of {0}.", parameterTypes.Length), "template");
        
            var mismatch = Enumerable.Range(0, templateParameters.Length)
                                     .Select(i => new 
                                     { 
                                         Parameter    = templateParameters[i], 
                                         ExpectedType = parameterTypes[i] 
                                     })
                                     .FirstOrDefault(e => e.Parameter.ParameterType != e.ExpectedType);

            if (mismatch != null) 
                throw new ArgumentException(string.Format("Parameter '{0}' of method called in the lambda expression body must be of {1} type.", mismatch.Parameter.Name, mismatch.ExpectedType), "template");

            MemberFilter filter = (member, _) =>
            {
                var m = (MethodInfo) member;
                return member.Name == name
                       && m.ReturnType == typeof(TResult) 
                       && parameterTypes.SequenceEqual(from p in m.GetParameters() select p.ParameterType);
            };
        
            return obj =>
            {
                var members = obj.GetType().FindMembers(MemberTypes.Method, BindingFlags.Public | BindingFlags.Instance, filter, null);
                return members.Length == 1
                    ? (Func<T1, TResult>) Delegate.CreateDelegate(typeof(Func<T1, TResult>), obj, (MethodInfo) members[0], true)
                    : null;
            };        
        }
    }
}