using System.Diagnostics.CodeAnalysis;

namespace ThingsLibrary.Schema.Library
{
    /// <summary>
    /// Attribute
    /// </summary>
    [DebuggerDisplay("{Key}: {Value}")]
    public class ItemAttributeGenericDto<T> : ItemAttributeDto
    {
        /// <inheritdoc />
        public new string Value => this.GetValueStr();

        /// <inheritdoc />
        [JsonPropertyName("value")]
        [Display(Name = "Value"), StringLength(50, MinimumLength = 1), NotNull]
        public T RawValue { get; set; }

        /// <inheritdoc />
        public ItemAttributeGenericDto(string key, T value)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(key);
            ArgumentNullException.ThrowIfNull(value);
                        
            this.Key = key;
            this.RawValue = value;
            this.DataType = AttributeDataTypes.GetType(typeof(T));
        }

        /// <summary>
        /// Set value as a string
        /// </summary>
        /// <param name="value"></param>
        private string GetValueStr()
        {
            // majority case
            if (this.RawValue is string valueStr)
            {
                return valueStr;
            }
            else if (this.RawValue is DateOnly valueDate)
            {
                return valueDate.ToString("yyyy-MM-dd");
            }
            else if (this.RawValue is TimeOnly valueTime)
            {
                return valueTime.ToString("HH:mm:ss");
            }
            else if (this.RawValue is DateTime valueDateTime)
            {
                return valueDateTime.ToString("O");
            }
            else if (this.RawValue is DateTimeOffset valueDateTimeOffset)
            {
                return valueDateTimeOffset.ToString("O");
            }            
            else if (this.RawValue is bool valueBool)
            {
                return $"{valueBool}".ToLower();
            }
            else
            {
                //DEFAULT VALUE / DATA TYPE
                return $"{this.RawValue}";     //no need to set the data type as it is defaulted to 'string'            
            }
        }
    }
}
