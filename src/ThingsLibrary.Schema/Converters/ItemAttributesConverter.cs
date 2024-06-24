//namespace ThingsLibrary.Schema.Converters
//{
//    /// <summary>
//    /// Converts a dictionary of key-value or key-array-values to a list of ItemAttributes
//    /// </summary>
//    public class ItemAttributesConverter : JsonConverter<List<ItemAttributeSchema>>
//    {
//        /// <summary>
//        /// Read the json data and convert it to a list of thing attributes
//        /// </summary>
//        /// <param name="reader"><see cref="Utf8JsonReader"/></param>
//        /// <param name="typeToConvert">Type to convert</param>
//        /// <param name="options">Json Serializer Options</param>
//        /// <returns></returns>
//        /// <exception cref="JsonException"></exception>
//        public override List<ItemAttributeSchema> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//        {
//            if (reader.TokenType != JsonTokenType.StartObject)
//            {
//                throw new JsonException($"JsonTokenType was of type {reader.TokenType}, only objects are supported");
//            }

//            var attributes = new List<ItemAttributeSchema>();

//            while (reader.Read())
//            {
//                if (reader.TokenType == JsonTokenType.EndObject) { return attributes; }
//                if (reader.TokenType != JsonTokenType.PropertyName) { throw new JsonException("JsonTokenType was not PropertyName"); }

//                // read the key field of the object
//                var key = reader.GetString();
//                if (string.IsNullOrWhiteSpace(key)) { throw new JsonException("Failed to get property name"); }

//                reader.Read();

//                var valueObj = ExtractValue(ref reader);
//                if (valueObj is string value)
//                {
//                    attributes.Add(new ItemAttributeSchema(key, value));

//                }
//                else if (valueObj is List<string> values)
//                {
//                    foreach (var valueStr in values)
//                    {
//                        attributes.Add(new ItemAttributeSchema(key, valueStr)
//                        {
//                            Type = AttributeDataTypes.Enum   //must be enum type if we have a list of values
//                        });
//                    }
//                }
//                else
//                {
//                    throw new ArgumentException($"Unable to parse attribute value '{valueObj}'");
//                }
//            }

//            return attributes;
//        }

//        /// <summary>
//        /// Write out the thing attributes schema
//        /// </summary>
//        /// <param name="writer"></param>
//        /// <param name="thingAttributes"></param>
//        /// <param name="options"></param>
//        public override void Write(Utf8JsonWriter writer, List<ItemAttributeSchema> thingAttributes, JsonSerializerOptions options)
//        {
//            var attributes = new Dictionary<string, object>();

//            foreach (var thingAttribute in thingAttributes)
//            {
//                if (attributes.ContainsKey(thingAttribute.Key))
//                {
//                    var value = attributes[thingAttribute.Key];
//                    if (value is List<string> valueArray)
//                    {
//                        valueArray.Add(thingAttribute.Value);
//                    }
//                    else if (value is string valueStr)
//                    {
//                        attributes[thingAttribute.Key] = new List<string>(2)
//                        {
//                            valueStr,
//                            thingAttribute.Value
//                        };
//                    }
//                    else
//                    {
//                        //not expected
//                        //Log.Warning("Value '{ItemAttributeValue}' is of a type '{ItemAttributeValueType}' that is not expected.", value, value.GetType());
//                    }
//                }
//                else
//                {
//                    attributes[thingAttribute.Key] = thingAttribute.Value;
//                }
//            }

//            writer.WriteRawValue(JsonSerializer.Serialize(attributes, options));
//        }

//        /// <summary>
//        /// Figure out if the value is a string or an array of strings
//        /// </summary>
//        /// <param name="reader"><see cref="Utf8JsonReader"/></param>
//        /// <returns>String value or Array of string values</returns>
//        /// <exception cref="JsonException"></exception>
//        private object ExtractValue(ref Utf8JsonReader reader)
//        {
//            switch (reader.TokenType)
//            {
//                case JsonTokenType.String:
//                    {
//                        return reader.GetString() ?? string.Empty;
//                    }

//                case JsonTokenType.StartArray:
//                    {
//                        var list = new List<string>();
//                        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
//                        {
//                            list.Add(reader.GetString() ?? string.Empty);
//                        }
//                        return list;
//                    }

//                default:
//                    {
//                        throw new JsonException($"'{reader.TokenType}' is not supported");
//                    }
//            }
//        }
//    }
//}



