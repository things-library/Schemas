namespace ThingsLibrary.Schema
{
    /// <summary>
    /// Item type attachments
    /// </summary>    
    [DebuggerDisplay("{Type} ({Name})")]
    public class ItemTypeAttachmentSchema : Base.SchemaBase
    {        
        /// <summary>
        /// Item Type
        /// </summary>        
        [JsonPropertyName("type")]
        [Display(Name = "Type Key"), StringLength(50, MinimumLength = 2), Required]
        [RegularExpression(Base.SchemaBase.KeyPattern, ErrorMessage = Base.SchemaBase.KeyPatternDescription)]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Item Type Name (Override)
        /// </summary>
        [JsonPropertyName("name")]
        [Display(Name = "Name"), StringLength(50, MinimumLength = 2), Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Item Type Description (Override)
        /// </summary>
        [JsonPropertyName("description")]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Is this item type required to be attached
        /// </summary>
        [JsonPropertyName("required")]
        [Display(Name = "Required")]
        public bool IsRequired { get; set; }

        /// <summary>
        /// Limit attachments to a specific number (0 = unlimited)
        /// </summary>
        /// <example>"Genre": ["Comedy","Romance"]</example>
        [JsonPropertyName("limit")]
        [Display(Name = "Limit")]
        public int Limit { get; set; }


        /// <summary>
        /// Item Type Attachment
        /// </summary>
        public ItemTypeAttachmentSchema()
        {
            //nothing
        }
    }
}

