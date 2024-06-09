# Things Library Schema

The general concept of the Things Library is that every 'thing' has a series of key-value attributes that describe the 'thing'.
Every 'thing' can have attachments which are other things that have key/value attributes. Attributes are global to the library and help explain what the value is and how to parse it such as a pick list item or a date or time or decimal number.

One of the goals is to make the general data structure as simple as possible at the 'thing' level. In order to have a proper thing data structure, it must have a unique reference key, name, type, and attributes that are key-value or key-array of values.

# Item Types

Item types provide context to the system and user regarding what kind of 'thing' we are trying to describe.  
Sometimes there is a need for the system to keep track of its own item types to help manage various features of the system.
For instance, an item type for keeping track of borrowing history and other aspects of borrowing is a system managed item type.
These system item types cannot be renamed or deleted however additional attributes can be added to them.

# Attributes

Attributes are associated with a item type that help track the various details needed to be captured for a certain kind of thing.
Each attribute has a data type that explains how the data be captured and validated, for example a email address data type can be validated as a valid email address while a 'number' data type can be validated to be a number. 
Data types allow for the attribute values to be used for filtering, automated processes and many other uses.

Both attributes and item types can be flagged as system properties.

Item types and Attributes can be flagged as being 'system' managed.  System managed means that users can not edit or delete these records however a user can add additional attributes to a system item type or additional values to a system attribute assuming the attribute isn't locked.

# Facts

* A Library contains 'things', each thing has a key (id), type and name.  
* Every 'thing' has a key which is unique to the library at the level to which it appears.
* Every 'thing' has a 'type' that explains what the thing is as well as what attributes are used to describe the 'thing'.  For example a bicycle might have a 'type' of 'bike' which brings along possible attributes like brand, model, serial number, purchase date, wheel size, etc.
* Every 'thing' can have 'attributes' that are key/value pairs which describe the thing.
* Every 'thing' can have 'attachments' which are dependencies to the 'thing'.  Attachments are also 'things' that have a name, key, type and can have attributes and also have attachments.

# Rules

* 'Things' have an 'item type' that explains how the thing is categorized (Example: car, furnace, electronics).
* 'Item Types' have various attributes used to define the thing. (Example: air filter size, last filter change date, recommended tire pressure, purchase date)
* Item type 'Attributes' have a data type. (Example: pick list, string, number, currency, date only, date and time, url)
* Keys must consist of lowercase alphanumeric characters or underscores.
* Keys are cannot be changed once created and must be unique to their container, for example a root 'thing' key must be unique to the library where an attached thing's key must be unique within the attachment things.


### Example Thing Json:
```json
{
    "key": "unique-key",
    "name": "Example Movie (2024)",
    "type": "movie",
    "attributes": {
        "release-date": "2024-02-21",
        "release-year": "2024",
        "studio": "BigPac Entertainment",
        "content-rating": "PG-13",
        "duration": "01:53:29.4710000",
        "genre": [
          "Science Fiction",
          "Action"
        ],        
        "role": [
          "Tom Feather",
          "Emily Sharp",
          "Bill Smith"
        ]
    }
}
```
