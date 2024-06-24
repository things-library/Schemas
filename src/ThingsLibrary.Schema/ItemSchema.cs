﻿namespace ThingsLibrary.Schema
{
    /// <summary>
    /// Iten
    /// </summary>
    [DebuggerDisplay("{Name} (Key: {Key}, Type: {Type})")]
    public class ItemSchema : Base.SchemaBase
    {        
        /// <summary>
        /// Resource Key
        /// </summary>  
        [JsonPropertyName("key")]
        [Display(Name = "Key"), StringLength(50, MinimumLength = 2)]
        [RegularExpression(Base.SchemaBase.RootKeyPattern, ErrorMessage = Base.SchemaBase.RootKeyPatternDescription)]
        public string? Key { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        [Display(Name = "Name"), StringLength(50, MinimumLength = 2), Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Date (If item is an 'event')
        /// </summary>
        /// <remarks>Designed for maintaining chronological listing</remarks>
        [JsonPropertyName("date")]
        public DateTime? Date { get; set; }
                
        /// <summary>
        /// Item Type 
        /// </summary>
        [JsonPropertyName("type")]
        [Display(Name = "Item Type"), StringLength(50, MinimumLength = 2), Required]
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternDescription)]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Attributes
        /// </summary> 
        /// <remarks>Value must be a string or array of strings</remarks>
        [JsonPropertyName("attributes"), JsonIgnoreEmptyCollection]
        public Dictionary<string, object> Attributes { get; set; } = new();

        /// <summary>
        /// Attachments
        /// </summary>
        [ValidateObject<ItemSchema>]
        [JsonPropertyName("attachments"), JsonIgnoreEmptyCollection]
        public List<ItemSchema> Attachments { get; set; } = new();

        
        /// <summary>
        /// Constructor
        /// </summary>
        public ItemSchema()
        {
            //nothing
        }
    }
}
