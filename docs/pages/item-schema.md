---
layout: default
title: Basic Item Schema
permalink: /item-schema
---

# Basic Item Schema Document

The basic item schema is designed to be a very flexible schema to maximize compatibility across systems.  The only required properties are 'name' and 'type'.  Once an item is imported into a library it is required to have a much more strict data structure seen in the library schema documentation below.

The latest schema document can be found here:
* [Basic Item Json Schema](https://schema.thingslibrary.io/latest/item.json)

The minimal item structure looks like this:
```json
{    
    "name": "Example Movie (2024)",
    "type": "movie" 
}
```
A more typical item structure would look like this:
```json
{   
    "name": "Example Movie (2024)",
    "type": "movie",
    "attributes": {
        "Release Date": "2024-02-21",
        "Release Year": "2024",
        "Studio": "BigPac Entertainment",
        "Content Rating": "PG-13",
        "Duration": "01:53:29.4710000",
        "Genre": [
          "Science Fiction",
          "Action"
        ],        
        "Role": [
          "Tom Feather",
          "Emily Sharp",
          "Bill Smith"
        ]
    }
}
```

