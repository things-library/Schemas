# Things Library Open Schema

The general concept of the Things Library is that every 'thing' or item has a series of key-value attributes that describe the item.
Every item can have attachments which are other items that have key/value attributes. Attributes are global to the item type to which they belong and help explain what the value is and how to parse it such as a pick list item or a date or time or decimal number.

One of the goals is to make the general data structure as simple as possible at the item level. In order to have a proper item data structure, it must have a name, type.

## Item Types

Item types provide context to the system regarding what kind of item we are trying to describe for example is it a computer or a furnance.
Sometimes there is a need for the system to keep track of its own item types to help manage various modules and features of the system.
For instance, an item type for keeping track of borrowing history and other aspects of borrowing is a system managed item type.
These system item types cannot be renamed or deleted however additional attributes can be added to them.

## Item Type Attributes

Attributes are associated with an item type that define what data is captured to describe the item.  For example first_name, last_name, email_address would be attributes for a 'user' item type.
Each attribute has a data type that explains how the data is captured and validated, for example a email address data type can be validated as a valid email address while a 'number' data type can be validated to be a number. 
Data types allow for the attribute values to be used for filtering, automated processes and many other uses.

Item types and attributes can be flagged as being 'system' managed.  System managed means that users can not edit or delete these records however a user can add additional attributes to a system item type or additional values to a system attribute assuming the attribute isn't locked.

## Facts

* A Library contains 'items', each item is required to have a name and type but can also have a user defined key, attributes and attachments.
* Every item has a system or user defined key that is unique at the level to which it appears.  Keys can not be changed once set.
* Every item has a 'type' which explains what the item is as well as what attributes are used to describe the item.  For example a bicycle might have a 'type' of 'bike' which brings along possible attributes like brand, model, serial number, purchase date, wheel size, etc.
* Every item can have 'attributes' that are key/value pairs which describe the thing.
* Every item can have 'attachments' which are dependencies to the item.  Attachments are also items that have a key, name, type and can have attributes and also have attachments.
* Every object in the data structure supports a metadata property which is additional key-value attributes that can be used for various system or user needs such as things like last edited date.

## Rules

* Items have an item type that explains how the thing is categorized (Example: car, furnace, electronics).
* Item types have various attributes used to define the thing. (Example: air filter size, last filter change date, recommended tire pressure, purchase date)
* Item type attributes have a data type. (Example: pick list, string, number, currency, date only, date and time, url)
* Keys must consist of lowercase alphanumeric characters or underscores which is known as snake case.
* Keys are cannot be changed once created and must be unique to their container, for example a root item key must be unique to the entire library where an attached or child item's key must be unique within the children items.


# Item Schema

The individual item schema is designed to be a very flexible schema to maximize compatibility across systems.  The only required fields are 'name' and 'type'.  

Upon import of a item into a things library, various additional pieces of data will be added and changed.  For example attribute keys will be changed to a snake case format so that the system can support translations and not be case sensitive.

The minimal structure looks like this:
```json
{    
    "name": "Example Movie (2024)",
    "type": "movie" 
}
```

A more typical structure would look like this:
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


# Latest Schema Document

The latest schema documents can be found here:

* [Library Json Schema](https://schema.thingslibrary.io/latest/library.json)
* [Library Item Json Schema](https://schema.thingslibrary.io/latest/item.json)
