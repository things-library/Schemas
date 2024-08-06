namespace ThingsLibrary.Schema.ServiceCanvas
{
    public class CanvasMessageBusChannelSchema
    {
        /// <summary>
        /// Payload Entity Schema / Namespace
        /// </summary>
        [Display(Name = "Address"), StringLength(200), Required]
        public string Address { get; init; } = string.Empty;
    }
}

