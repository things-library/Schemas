namespace ThingsLibrary.Schema.ServiceCanvas
{
    public class CanvasMessageBusSchema
    {
        /// <summary>
        /// Dependency Name
        /// </summary>
        [Display(Name = "Name"), StringLength(200), Required]
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Message Bus Type
        /// </summary>
        public string Type { get; init; } = string.Empty;

        /// <summary>
        /// Connection String variable name
        /// </summary>
        /// <example>ServiceDatabase</example>
        [Display(Name = "ConnectionString Variable"), StringLength(200), Required]
        public string ConnectionStringVariable { get; init; } = string.Empty;

        /// <summary>
        /// Send Channels
        /// </summary>
        public Dictionary<string, CanvasMessageBusChannelSchema> SendChannels { get; init; } = new Dictionary<string, CanvasMessageBusChannelSchema>();

        /// <summary>
        /// Receive Channels
        /// </summary>
        public Dictionary<string, CanvasMessageBusChannelSchema> ReceiveChannels { get; init; } = new Dictionary<string, CanvasMessageBusChannelSchema>();

        public CanvasMessageBusSchema()
        {
            //nothing
        }
    }
}

