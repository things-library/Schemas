using System.ComponentModel.DataAnnotations.Schema;

namespace ThingsLibrary.Schema
{
    /// <summary>
    /// Attribute
    /// </summary>
    [DebuggerDisplay("{Key}: {Value}")]
    public class ItemAttributeSchema : Base.SchemaBase
    {        
        /// <summary>
        /// Library Unique Key
        /// </summary>
        /// <remarks>(Pattern: {library_key}/{item_type_key}</remarks>
        [JsonIgnore]
        [Display(Name = "Key"), StringLength(50, MinimumLength = 1), Required]
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternDescription)]
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Attribute Value
        /// </summary>
        [JsonPropertyName("value"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [Display(Name = "Value"), StringLength(50, MinimumLength = 1)]
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Attribute Value
        /// </summary>
        [JsonPropertyName("values"), JsonIgnoreEmptyCollection]
        [Display(Name = "Values"), StringLength(50, MinimumLength = 1)]
        public List<string> Values { get; set; } = new();

        #region --- Extended ---

        /// <summary>
        /// Item (aka: parent)
        /// </summary>
        [NotMapped]
        public ItemSchema? Item { get; set; }

        /// <summary>
        /// Item Type Attribute
        /// </summary>
        [NotMapped]
        public ItemTypeAttributeSchema? ItemTypeAttribute { get; set; }

        /// <summary>
        /// Attribute Value (if pick list)
        /// </summary>
        [NotMapped]
        public ItemTypeAttributeValueSchema? ItemTypeAttributeValue { get; set; }

        /// <summary>
        /// Attribute Values (if pick list)
        /// </summary>
        [NotMapped]
        public Dictionary<string, ItemTypeAttributeValueSchema> ItemTypeAttributeValues { get; set; } = new();

        #endregion

        #region --- Initialization ---

        /// <summary>
        /// Initializes the library so that all things in it have matching attributes and item types.  Creates the relationships between things and attributes
        /// </summary>
        /// <remarks>Normally only needed to be called after deserialization</remarks>
        public void Init(ItemSchema parent)
        {
            this.Item = parent;

            ItemTypeAttributeSchema? itemTypeAttribute;
            if (parent.ItemType?.Attributes.TryGetValue(this.Key, out itemTypeAttribute) == true)
            {
                this.ItemTypeAttribute = itemTypeAttribute;
                if (itemTypeAttribute.Type == "enum")
                {
                    // lookup value if there is one
                    ItemTypeAttributeValueSchema? itemTypeAttributeValue;
                    if (itemTypeAttribute.Values.TryGetValue(this.Value, out itemTypeAttributeValue))
                    {
                        this.ItemTypeAttributeValue = itemTypeAttributeValue;
                    }
                    else
                    {
                        throw new ArgumentException($"Unable to find item type attribute value '{itemTypeAttribute.Key}:{this.Key}'");
                    }
                }
            }
            else
            {
                throw new ArgumentException($"Unable to find item type attribute '{this.Key}'");
            }
        }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemAttributeSchema()
        {
            //nothing
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value (or Value Key)</param>
        public ItemAttributeSchema(string key, string value)
        {
            this.Key = key;
            this.Value = value;
            this.Values = new() { value };
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="values">Values</param>
        public ItemAttributeSchema(string key, List<string> values)
        {
            this.Key = key;
            if (values.Any())
            {
                this.Value = values.First();
            }            
            this.Values = values;
        }
    }
}
