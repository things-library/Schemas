---
layout: default
title: Item Schema
permalink: /item-schema
---

# Item Schema Document

The item schema is designed to be a very flexible schema to maximize compatibility across systems.  The only required properties are 'name' and 'type'.  Once an item is imported into a library it is required to have a much more strict data structure seen in the library schema documentation below.

The latest schema document can be found here:
* [Item Json Schema](https://schema.thingslibrary.io/latest/item.json)

**Rules**

* Items must have a 'type' property that differenciates it from other types of items.
* Items can have a 'name' property.
* Items can have 'tags' that are key/value pairs which describe the item.
* Items can have attached 'items' which are dependencies to the item.  Item attachments are also items that have a key, name, type and tags (attributes) and also other item attachments.
* Attached Items must have a 'key' property which are either a system or user defined key that is unique at the level to which it appears.  Keys can not be changed once set and must be snake case.  

## Examples

The minimal item structure looks like this:
```json
{    
    "type": "movie" 
}
```

A more typical item structure would look like this:
```json
{   
	"name": "Example Movie (2024)",
	"type": "movie",
	"tags": {
		"Release Date": "2024-02-21",
		"Release Year": "2024",
		"Studio": "BigPac Entertainment",
		"Content Rating": "PG-13",
		"Duration": "01:53:29.4710000",
		"Genre": "Science Fiction"		
	},
	"items": {
		"box_office": {
			"name": "2024 Box Office",
			"type": "boxoffice",
			"tags": {
				"Budget": "$92,000,000",
				"Gross US & Canada": "$241,830,615",
				"Opening Weekend US & Canada": "$41,059,405",
				"Gross Worldwide": "$494,580,615"
			}
		}
	}
}
```
