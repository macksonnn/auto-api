﻿using System.Diagnostics.CodeAnalysis;

namespace Core.Common.Extensions
{
    public static class Helpers
    {
        /// <summary>
        /// Throw an ArgumentNullException if the object is null
        /// </summary>
        /// <typeparam name="T">The type of the to be tested</typeparam>
        /// <param name="argument">The object to be tested</param>
        /// <param name="paramName">The name of the parameter. Is null, the method will try to use it dynamic</param>
        /// <returns>The object or the exception if the object is null</returns>
        public static T ThrowIfNull<T>([AllowNull()] this T argument, string? paramName = null)
        {
            ArgumentNullException.ThrowIfNull(argument, paramName: paramName);

            return argument;
        }
    }
}
