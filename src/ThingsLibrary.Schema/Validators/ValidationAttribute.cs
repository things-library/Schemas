using System.Collections;

namespace ThingsLibrary.Schema.Validators
{
    public class ValidateObjectAttribute<T> : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? instance, ValidationContext validationContext)
        {
            if (instance == null)
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

            var subResults = new List<CompositeValidationResult>();

            var compositeResult = new CompositeValidationResult($"Validation failed!",
                new List<string> { $"{validationContext.MemberName}" }
            );

            //if (instance is IDictionary<string, T> dictionary)
            //{
            //    compositeResult.Add(this.ValidateDictionary(dictionary as IDictionary));
            //}
            //else if (instance is IList<T> items)
            //{
            //    compositeResult.Add(this.ValidateList(items as IList));                
            //}
            //else
            if (instance is IList list)
            {
                compositeResult.Add(ValidateList(list));
            }
            else if (instance is IDictionary dictionary)
            {
                compositeResult.Add(ValidateDictionary(dictionary as IDictionary));
            }
            else
            {
                compositeResult.Add(Validate(instance));
            }

            // if we have validation results then we aren't successful
            if (compositeResult.Results.Any())
            {
                return compositeResult;
            }

            return ValidationResult.Success!;
        }

        private List<ValidationResult> Validate(object instance)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(instance, null, null);

            if (Validator.TryValidateObject(instance, context, results, true)) { return results; }

            return results;
        }

        private List<CompositeValidationResult> ValidateList(IList list)
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

        //private List<CompositeValidationResult> ValidateList<T>(IList<T> list)
        //{
        //    var results = new List<CompositeValidationResult>();

        //    int i = -1; // so our first loop is correct
        //    foreach (var item in list)
        //    {
        //        i++;
        //        if (item == null) { continue; }

        //        var subResults = new List<ValidationResult>();
        //        var context = new ValidationContext(item, null, null);

        //        Validator.TryValidateObject(item, context, subResults, true);
        //        if (!subResults.Any()) { continue; }

        //        var compositeResult = new CompositeValidationResult($"Validation failed!",
        //            new List<string> { $"[{i}]" }
        //        );

        //        compositeResult.Add(subResults);

        //        results.Add(compositeResult);
        //    }

        //    return results;
        //}

        private List<CompositeValidationResult> ValidateDictionary(IDictionary dictionary)
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

                var compositeResult = new CompositeValidationResult($"Validation failed!",
                    new List<string> { $"[\"{key}\"]" }
                );

                compositeResult.Add(subResults);

                results.Add(compositeResult);
            }

            return results;
        }

        //private List<CompositeValidationResult> ValidateDictionary<T>(IDictionary<string, T> dictionary)
        //{
        //    var results = new List<CompositeValidationResult>();

        //    foreach (var keyPair in dictionary)
        //    {
        //        if (keyPair.Value == null) { continue; }

        //        var subResults = new List<ValidationResult>();
        //        var context = new ValidationContext(keyPair.Value, null, null);

        //        // Validate the collection item
        //        if (Validator.TryValidateObject(keyPair.Value, context, subResults, true)) { continue; }
        //        if (!subResults.Any()) { continue; }

        //        var compositeResult = new CompositeValidationResult($"Validation failed!",
        //            new List<string> { $"[\"{keyPair.Key}\"]" }
        //        );

        //        compositeResult.Add(subResults);

        //        results.Add(compositeResult);
        //    }

        //    return results;
        //}
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
