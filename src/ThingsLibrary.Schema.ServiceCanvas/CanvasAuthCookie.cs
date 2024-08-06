namespace ThingsLibrary.Schema.ServiceCanvas
{
    public class CanvasAuthCookieSchema
    {
        /// <summary>
        /// Cookie Name
        /// </summary>
        [Required, StringLength(200, ErrorMessage = "Name must be between 3 and 200 characters", MinimumLength = 3)]
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Cookie Path 
        /// </summary>
        public string? Path { get; init; }

        /// <summary>
        /// Cookie Domain
        /// </summary>
        public string? Domain { get; init; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CanvasAuthCookieSchema()
        {
            //nothing
        }
    }
}
