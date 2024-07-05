---
layout: default
title: Library Schema
permalink: /library-schema
---

# Library Schema Definition

The library schema ties it all together with global item type attributes that explain the various options and data types.  This schema is broken down into item types, item type attributes and items with attributes.

The latest library schema document can be found here:
* [Library Json Schema](https://schema.thingslibrary.io/latest/library.json)
 
## Library

The library is the outer most data structure and requires a 'types' and 'items' node.  This structure will also have a reference to the json schema document.

**Rules**

* Libraries must contain an 'types' array which explains what types of items exist in the library.
* Libraries must contain an 'items' array which contains the objects being described using name, type, attributes and attachments.

## Item Types

Item types provide context to the system regarding what kind of item we are trying to describe for example is it a computer or a furnance.  

**Rules**

* Item types must have a 'key' property that is snake case.
* Item types must have a 'name' property.
* Item types can have 'attributes' which are the type attributes that can be used to explain a item of that 'type'.  For example a 'person' item type might have attributes like 'first_name', and 'last_name'.
* Item types can have 'attachments' which explain what other types of items are typically attached to this item type.  For example a 'vehicle_damage_report' item type might have 'damaged_vehicle' as a typically attached item type.


## Item Type Attributes

Item type attributes are associated with an item type that define what data is captured to describe the item.  
For example first_name, last_name, email_address would be attributes for a 'person' item type.
Each attribute has a data type that explains how the data is captured and validated, for example an email address data type can be validated as a valid email address while a 'number' data type can be validated to be a number.

**Rules**

* Item type attributes must have a 'key' property.
* Item type attributes must have a 'name' property.
* Item type attributes must have a data 'type'. (Example: pick list, string, number, currency, date only, date and time, url)

### Item

The item is the core building block of the library as it defines all the various items that are being tracked.

**Rules**

* Items must have a 'key' property which are either a system or user defined key that is unique at the level to which it appears.  Keys can not be changed once set and must be snake case.
* Items must have a 'name' property.
* Items must have a 'type' property that refer to an item type from the 'types' collection.
* Items can have 'attributes' that are key/value pairs which describe the thing where the key is snake case.  Item attribute values must either be a string or a array of string.
* Items can have 'attachments' which are dependencies to the item.  Attachments are also items that have a key, name, type and can have attributes and also have attachments.

### Generic Rules

* Every object in the data structure supports a metadata property which is additional key-value attributes that can be used for various system or user needs such as change history tracking.  These key-value pairs are not considered type attributes.


# HTTP Headers

Json Schema document should be sent with the right headers seen below.

## Request Headers:

| Key | Value | Description |
| -- | -- | -- |
| Accept | application/schema+json | Specifies what is the acceptable content-type for the requesting agent. |
| Accept-Schema | {{URL}} | Specifies the acceptable json schema |

## Response Headers:

| Key | Value | Description |
| -- | -- | -- |
| Schema | {{Url}} | Specifies the schema which returned content follows |
| Cache-Control | public, max-age=31536000 | Caching TTL / duration to use.  |
| Last-Modified | {{date-time}} | Date and time the data was last changed. |
| ETag | {{string}} | Unique tracking code that changes each time the underlying data changes. |
| Content-Type | application/schema+json | |



