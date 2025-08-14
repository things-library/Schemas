// ================================================================================
// <copyright file="ValidationAttribute.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

using System.Collections;

namespace ThingsLibrary.Schema.Canvas.Validators
{
    [System.AttributeUsage(System.AttributeTargets.Property, Inherited = true)]
    public class ValidateCollectionItemsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null && validationContext.MemberName != null)
            {
                var propertyInfo = validationContext.ObjectType.GetProperty(validationContext.MemberName);

                var isNullable = propertyInfo?.CustomAttributes.Any(x => x.AttributeType.Name == "NullableAttribute");
                if (isNullable == true)
                {
                    return ValidationResult.Success!;
                }
                else
                {
                    return new ValidationResult($"{validationContext.MemberName} is required.",
                        new List<string> { validationContext.MemberName }
                    );
                }
            }

            var compositeResult = new CompositeValidationResult($"Validation failed!",
                new List<string> { $"{validationContext.MemberName}" }
            );

            if (value is IList list)
            {
                compositeResult.Add(ValidateList(list));
            }
            else if (value is IDictionary dictionary)
            {
                compositeResult.Add(ValidateDictionary(dictionary));
            }
            else if (value != null)
            {
                compositeResult.Add(Validate(value));
            }

            // if we have validation results then we aren't successful
            if (compositeResult.Results.Any())
            {
                return compositeResult;
            }

            return ValidationResult.Success!;
        }

        private static List<ValidationResult> Validate(object instance)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(instance, null, null);

            if (Validator.TryValidateObject(instance, context, results, true)) { return results; }

            return results;
        }

        private static List<CompositeValidationResult> ValidateList(IList list)
        {
            var results = new List<CompositeValidationResult>();

            int i = -1; // so our first loop is correct
            foreach (var item in list)
            {
                i++;
                if (item == null) { continue; }

                var subResults = new List<ValidationResult>();
                var context = new ValidationContext(item, null, null);

                Validator.TryValidateObject(item, context, subResults, true);
                if (!subResults.Any()) { continue; }

                var compositeResult = new CompositeValidationResult($"Validation failed!",
                    new List<string> { $"[{i}]" }
                );

                compositeResult.Add(subResults);

                results.Add(compositeResult);
            }

            return results;
        }

        private static List<CompositeValidationResult> ValidateDictionary(IDictionary dictionary)
        {
            var results = new List<CompositeValidationResult>();

            foreach (var key in dictionary.Keys)
            {
                var value = dictionary[key];
                if (value == null) { continue; }

                var subResults = new List<ValidationResult>();
                var context = new ValidationContext(value, null, null);

                // Validate the collection item
                if (Validator.TryValidateObject(value, context, subResults, true)) { continue; }
                if (!subResults.Any()) { continue; }

                var compositeResult = new CompositeValidationResult($"Validation failed!", new List<string> { $"[\"{key}\"]" });

                compositeResult.Add(subResults);

                results.Add(compositeResult);
            }

            return results;
        }
    }

    public class CompositeValidationResult : ValidationResult
    {
        private readonly List<ValidationResult> _results = new List<ValidationResult>();

        public IEnumerable<ValidationResult> Results
        {
            get
            {
                return _results;
            }
        }

        public CompositeValidationResult(string errorMessage) : base(errorMessage)
        {
            //nothing
        }

        public CompositeValidationResult(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames)
        {
            //nothing
        }

        protected CompositeValidationResult(ValidationResult validationResult) : base(validationResult)
        {
            //nothing
        }

        public void Add(ValidationResult validationResult)
        {
            _results.Add(validationResult);
        }

        public void Add(List<ValidationResult> validationResults)
        {
            _results.AddRange(validationResults);
        }

        public void Add(List<CompositeValidationResult> validationResults)
        {
            _results.AddRange(validationResults);
        }
    }
}
