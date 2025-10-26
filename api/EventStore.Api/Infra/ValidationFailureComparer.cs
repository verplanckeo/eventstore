using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace EventStore.Api.Infra
{
    /// <summary>
    /// Fluent ValidationFailure comparison
    /// </summary>
    public class ValidationFailureComparer : IEqualityComparer<ValidationFailure>
    {
        // Note: I know this is very basic, but for now I don't want double error strings to be in the list.
        /// <summary>
        /// Check if 2 validation failures are the same
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(ValidationFailure x, ValidationFailure y)
        {
            return (x?.ErrorMessage == y?.ErrorMessage) && (x?.Severity == y?.Severity);
        }

        /// <summary>
        /// Override GetHashCode with fields which are used to check if 2 objects are the same.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(ValidationFailure obj)
        {
            return HashCode.Combine(obj.ErrorMessage, obj.Severity);
        }
    }
}