namespace ThingsLibrary.Schema.ServiceCanvas
{
    public class CanvasInfoCorsSchema
    {
        /// <summary>
        /// Origin
        /// </summary>
        [Display(Name = "Origin"), Required]
        public string Origin { get; init; } = "*";

        /// <summary>
        /// Methods
        /// </summary>
        [Display(Name = "Methods"), Required]
        public IEnumerable<string> Methods { get; init; } = new List<string>();

        /// <summary>
        /// Headers
        /// </summary>
        [Display(Name = "Headers"), Required]
        public IEnumerable<string> Headers { get; init; } = new List<string>();

    }
}
