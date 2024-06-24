﻿using System.ComponentModel.DataAnnotations;
using ThingsLibrary.Schema.Validators;

namespace ThingsLibrary.Schema.Tests.Extensions
{
    public static class ClassExtensions
    {
        #region --- Validation --- 

        /// <summary>
        /// Validate the class based on the Data Annotations
        /// </summary>
        /// <param name="instance">class object to validate</param>
        /// <param name="flatten">Flattens all CompositeValidationResults into a single listing</param>
        /// <returns></returns>
        /// <remarks>This method evaluates each ValidationAttribute instance that is attached to the object type. It also checks whether each property that is marked with RequiredAttribute is provided. It does not recursively validate the property values of the object.</remarks>
        public static ICollection<ValidationResult> ToValidationResults(this object instance, bool flatten = false)
        {
            var results = new List<ValidationResult>();

            Validator.TryValidateObject(instance, new ValidationContext(instance), results, true);

            if (flatten)
            {
                return results.Flatten();
            }
            else
            {
                return results;
            }
        }

        /// <summary>
        /// Flatten the composite validation results into a single flat listing
        /// </summary>
        /// <param name="results">Validation Results</param>
        /// <returns></returns>
        public static ICollection<ValidationResult> Flatten(this ICollection<ValidationResult> results)
        {
            var list = new List<ValidationResult>();

            foreach (var result in results)
            {
                if (result is CompositeValidationResult compositeResult)
                {
                    list.AddRange(compositeResult.Flatten());
                }
                else
                {
                    list.Add(result);
                }
            }

            //return list.OrderBy(x => x.MemberNames.FirstOrDefault()).ToList();
            return list;
        }

        /// <summary>
        /// Flatten the composite validation results into a single flat listing
        /// </summary>
        /// <param name="result">Composite Validation Result</param>
        /// <returns></returns>
        public static ICollection<ValidationResult> Flatten(this CompositeValidationResult result)
        {
            var list = new List<ValidationResult>();

            if (result.Results.Any())
            {
                var memberName = result.MemberNames.FirstOrDefault();
                foreach (var subResult in result.Results)
                {
                    if (subResult is CompositeValidationResult compositeResult)
                    {
                        var flatList = compositeResult.Flatten();
                        foreach(var item in flatList)
                        {
                            list.Add(item);
                        }
                    }
                    else
                    {
                        list.Add(new ValidationResult(subResult.ErrorMessage, new List<string> { $"{memberName}.{subResult.MemberNames.FirstOrDefault()}" }));
                    }
                }
            }
            else
            {
                list.Add(result as ValidationResult);
            }

            return list;
        }

        #endregion

    }
}