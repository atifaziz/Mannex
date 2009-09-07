namespace Mannex.Web
{
    #region Imports

    using System;
    using System.Collections.Specialized;
    using System.Text;
    using System.Web;

    #endregion

    /// <summary>
    /// Extension methods for <see cref="NameValueCollection"/>.
    /// </summary>

    static partial class NameValueCollectionExtensions
    {
        /// <summary>
        /// Creates a query string from the key and value pairs found
        /// in the collection.
        /// </summary>
        /// <remarks>
        /// A question mark (?) is prepended if the resulting query string
        /// is not empty.
        /// </remarks>

        public static string ToQueryString(this NameValueCollection collection)
        {
            if (collection == null) throw new ArgumentNullException("collection");

            var sb = new StringBuilder();

            var names = collection.AllKeys;
            for (var i = 0; i < names.Length; i++)
            {
                var name = names[i];
                var values = collection.GetValues(i);

                if (values == null)
                    continue;
                
                foreach (var value in values)
                {
                    sb.Append('&');

                    if (!string.IsNullOrEmpty(name))
                        sb.Append(name).Append('=');

                    sb.Append(string.IsNullOrEmpty(value) 
                              ? string.Empty 
                              : HttpUtility.UrlPathEncode(value));
                }
            }

            if (sb.Length == 0)
                return string.Empty;
            
            sb[0] = '?';
            return sb.ToString();
        }
    }
}
