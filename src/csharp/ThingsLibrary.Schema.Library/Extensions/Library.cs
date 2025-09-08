// ================================================================================
// <copyright file="Library.cs" company="Starlight Software Co">
//    Copyright (c) 2025 Starlight Software Co. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// </copyright>
// ================================================================================

using ThingsLibrary.Schema.Library.Base;

namespace ThingsLibrary.Schema.Library.Extensions
{
    public static class LibraryExtensions
    {
        public static string DefaultLanguageCode { get; set; } = "en-US";

        public static RootItemDto ToResultsDto(this List<RootItemDto> list)
        {
            var results = new RootItemDto
            {
                Type = "library",

                Key = "results",
                Name = "Results"                
            };
            // just incase there is a duplicate which is possible for a listing
            foreach (var item in list)
            {
                results.Items[item.Key] = item;
            }

            return results;
        }

        /// <summary>
        /// Get the tag name from the definitions doc
        /// </summary>
        /// <param name="library">Library</param>
        /// <param name="typeKey">Type</param>
        /// <param name="tagKey">Tag Name</param>
        /// <returns></returns>
        public static string GetTypeTagName(this RootItemDto library, string typeKey, string tagKey)
        {
            if (!library.Types.ContainsKey(typeKey)) { return tagKey; }

            var type = library.Types[typeKey];
            if (!type.Tags.ContainsKey(tagKey)) { return tagKey; }

            return type.Tags[tagKey].Name;
        }

        /// <summary>
        /// Get the tag position from the definitions doc
        /// </summary>
        /// <param name="library">Library</param>
        /// <param name="typeKey">Type</param>
        /// <param name="tagKey">Tag Name</param>
        /// <returns></returns>
        public static short GetTypeTagPosition(this RootItemDto library, string typeKey, string tagKey)
        {
            if (!library.Types.ContainsKey(typeKey)) { return 999; }

            var type = library.Types[typeKey];
            if (!type.Tags.ContainsKey(tagKey)) { return 999; }

            return type.Tags[tagKey].Sequence ?? 999;
        }


        /// <summary>
        /// Attach item to existing library making it a child item
        /// </summary>
        /// <param name="library">Existing Library</param>
        /// <param name="item">Item</param>
        /// <exception cref="ArgumentException">If item key already exists as child item</exception>
        public static void Attach(this RootItemDto library, RootItemDto item)
        {
            //NOTE: the assumption is that the item already has all the type definitions!!

            if (library.Items.ContainsKey(item.Key)) { throw new ArgumentException($"Item already exists for key '{item.Key}'"); }

            // add any missing definitions
            foreach (var itemType in item.Types)
            {
                // see if the type exists
                if (library.Types.TryGetValue(itemType.Key, out var libraryItemType))
                {
                    // see if the tag exists, create if missing
                    foreach (var typeTag in itemType.Value.Tags)
                    {
                        if (libraryItemType.Tags.ContainsKey(typeTag.Key)) { continue; }

                        libraryItemType.Tags.Add(typeTag.Key, typeTag.Value.Clone());
                    }
                }
                else
                {
                    // add the entire type
                    library.Types.Add(itemType.Key, itemType.Value.Clone());
                }
            }

            // if existing library, add the items of that library not the library itself
            if(item.Type == "library")
            {
                foreach(var childItem in item.Items)
                {
                    // root item already exists?
                    if (library.Items.ContainsKey(childItem.Key))
                    {
                        continue;
                    }

                    library.Items.Add(childItem.Key, childItem.Value.Clone());
                }
            }
            else
            {
                library.Items.Add(item.Key, ((ItemDto)item).Clone());
            }
        }


        /// <summary>
        /// Attach only the definition types and tags that are in use in the library6
        /// </summary>
        /// <param name="library"></param>
        /// <param name="definitions"></param>
        /// <param name="languageCode">LanguageCode (ie: en, en-US, de, fr)</param>
        public static void AttachTypeDefinitions(this RootItemDto library, RootItemDto definitions, string languageCode)
        {
            // make sure every type and tag is generated in the 'types' definitions
            library.GenerateDefinitions();

            foreach (var libraryType in library.Types)
            {
                // does the definitions have the type key?
                if (definitions.Types.TryGetValue(libraryType.Key, out var definitionType))
                {
                    // we only want to add definition to the tags that are in use
                    foreach(var libraryTypeTag in libraryType.Value.Tags)
                    {
                        // does the definition type have this tag key?
                        if(definitionType.Tags.TryGetValue(libraryTypeTag.Key, out var definitionTypeTag))
                        {
                            libraryType.Value.Tags[libraryTypeTag.Key] = definitionTypeTag.Clone(languageCode); //replace the existing tag
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Sets the name / units fields to the metadata version for the provided language, clearing metadata after
        /// </summary>
        /// <param name="itemType">Item Type</param>
        /// <param name="languageCode">Language Code (ie: en, en-US, en-UK, de, fr, es)</param>
        private static void SetLanguage(this ItemTypeDto itemType, string languageCode)
        {
            // nothing to do? 
            if (languageCode == LibraryExtensions.DefaultLanguageCode) { return; }

            // find the translation if exists
            if (itemType.Meta.ContainsKey($"name_{languageCode}"))
            {
                itemType.Name = itemType.Meta[$"name_{languageCode}"];
            }
            else if (languageCode.Contains('-')) // see if there is a region code attached and just match on root language
            {
                languageCode = languageCode.Split('-')[0];
                if (itemType.Meta.ContainsKey($"name_{languageCode}"))
                {
                    itemType.Name = itemType.Meta[$"name_{languageCode}"];
                }
            }

            // set all the tags to the specified language
            foreach (var tag in itemType.Tags.Values)
            {
                tag.SetLanguage("name", languageCode);
                tag.SetLanguage("units", languageCode);
            }
        }

        private static void SetLanguage(this ItemTypeTagDto itemTypeTag, string property, string languageCode)
        {
            // nothing to do? 
            if (languageCode == LibraryExtensions.DefaultLanguageCode) { return; }

            // find the translation if exists
            if (itemTypeTag.Meta.ContainsKey($"${property}_{languageCode}"))
            {
                itemTypeTag.Name = itemTypeTag.Meta[$"${property}_{languageCode}"];
            }
            else if (languageCode.Contains('-'))    // see if there is a region code attached and just match on root language
            {
                languageCode = languageCode.Split('-')[0];
                if (itemTypeTag.Meta.ContainsKey($"${property}_{languageCode}"))
                {
                    itemTypeTag.Name = itemTypeTag.Meta[$"{property}_{languageCode}"];
                }
            }            
        }

        private static ItemTypeTagDto Clone(this ItemTypeTagDto itemTypeTag, string languageCode)
        {
            //LanguageCode:  en, en-US

            var newTypeTag = itemTypeTag.Clone();

            newTypeTag.SetLanguage("name", languageCode);
            newTypeTag.SetLanguage("units", languageCode);

            // clear any system metadata (like translations)
            newTypeTag.Meta.Clear();

            return newTypeTag;
        }

        public static void GenerateDefinitions(this RootItemDto library)
        {
            // nothing to do?
            if (library.Type == "library" && library.Items.Count == 0) { return; }

            library.GenerateDefinitions(library);  // start at the top node            
        }

        private static void GenerateDefinitions(this RootItemDto library, ItemDto currentItem)
        {
            if (currentItem.Type != "library")
            {
                // get the matching item type
                ItemTypeDto itemType;
                if (library.Types.ContainsKey(currentItem.Type))
                {
                    itemType = library.Types[currentItem.Type];
                }
                else
                {
                    itemType = new ItemTypeDto
                    {
                        Name = currentItem.Type.ToDisplayName()
                    };
                    library.Types.Add(currentItem.Type, itemType);
                }

                // figure out the current position / sequence number
                short seq = 0;
                if (itemType.Tags.Count > 0) { seq = itemType.Tags.Select(x => x.Value.Sequence ?? 0).Max(); }

                foreach (var tag in currentItem.Tags)
                {
                    // we already have something?  NEXT!
                    if (itemType.Tags.ContainsKey(tag.Key)) { continue; }

                    var itemTypeTag = new ItemTypeTagDto
                    {
                        Name = tag.Key.ToDisplayName(),
                        Type = SchemaBase.DetectDataType(tag.Key, tag.Value), //TODO: try to determine type based on pattern matching
                        Sequence = ++seq
                    };

                    itemType.Tags.Add(tag.Key, itemTypeTag);
                }
            }

            // Recurse the child items
            foreach (var childItem in currentItem.Items.Values)
            {
                GenerateDefinitions(library, childItem);
            }
        }


        /// <summary>
        /// Apply the full definitions from the authorative source to another library
        /// </summary>
        /// <param name="library">Target Library</param>
        /// <param name="definitionLibrary">Library containing the type definitions</param>
        public static void ApplyDefinitions(this RootItemDto library, RootItemDto definitionLibrary)
        {
            foreach (var itemType in library.Types)
            {
                var definitionType = definitionLibrary.Types.FirstOrDefault(x => x.Key == itemType.Key).Value;
                if (definitionType == null) { continue; }

                itemType.Value.ApplyDefinition(definitionType);                
            }
        }

        public static Dictionary<string, ItemTypeDto> GetDefinitionUsage(this RootItemDto library, ItemDto currentItem)
        {
            var usedTypes = new Dictionary<string, ItemTypeDto>();

            PopulateDefinitionUsage(library, usedTypes, currentItem);

            return usedTypes;
        }

        private static void PopulateDefinitionUsage(RootItemDto library, Dictionary<string, ItemTypeDto> usedTypes, ItemDto currentItem)
        {
            // don't include 'library' type as it is just a container
            if (currentItem.Type != "library")
            {
                // get the matching item type
                ItemTypeDto itemType;
                if (usedTypes.ContainsKey(currentItem.Type))
                {
                    itemType = usedTypes[currentItem.Type];
                }
                else
                {
                    itemType = new ItemTypeDto
                    {
                        Name = currentItem.Type.ToDisplayName()
                    };
                    usedTypes.Add(currentItem.Type, itemType);
                }

                // figure out the current position / sequence number
                short seq = 0;
                if (itemType.Tags.Count > 0) { seq = itemType.Tags.Select(x => x.Value.Sequence ?? 0).Max(); }

                foreach (var tag in currentItem.Tags)
                {
                    ItemTypeTagDto itemTypeTag;

                    // we already have something?  NEXT!
                    if (itemType.Tags.ContainsKey(tag.Key))
                    {
                        itemTypeTag = itemType.Tags[currentItem.Type];
                    }
                    else
                    {
                        itemTypeTag = new ItemTypeTagDto
                        {
                            Type = SchemaBase.DetectDataType(tag.Key, tag.Value), //TODO: try to determine type based on pattern matching

                            Name = tag.Key.ToDisplayName(),
                            Sequence = ++seq
                        };
                        itemType.Tags.Add(tag.Key, itemTypeTag);
                    }

                    // if picklist create list of unique keys
                    if(itemTypeTag.Type == ItemTagDataTypesDto.Enum)
                    {
                        var key = SchemaBase.GenerateKey(tag.Value);
                        if (!itemTypeTag.Values.ContainsKey(key))
                        {
                            itemTypeTag.Values.Add(key, tag.Value);
                        }
                    }
                }
            }

            // Recurse the child items
            foreach (var childItem in currentItem.Items.Values)
            {
                PopulateDefinitionUsage(library, usedTypes, childItem);
            }
        }
    }
}
