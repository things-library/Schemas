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
* Item types can have 'tags' which are the type tags that can be used to explain a item of that 'type'.  For example a 'person' item type might have tag like 'first_name', and 'last_name' defined as string values.
* Item types can have 'items' which explain what other types of items are typically attached to this item type.  For example a 'vehicle_damage_report' item type might have 'damaged_vehicle' as a typically attached item type.


## Item Type Tags

Item type tags are associated with an item type that define what data is captured to describe the item.  
For example first_name, last_name, email_address would be tags for a 'person' item type.
Each attribute has a data type that explains how the data is captured and validated, for example an email address data type can be validated as a valid email address while a 'number' data type can be validated to be a number.

**Rules**

* Item type tags must have a 'key' property which is snake case.
* Item type tags must have a 'name' property.
* Item type tags must have a data 'type'. (Example: pick list, string, number, currency, date only, date and time, url)

### Item

The item is the core building block of the library as it defines all the various items that are being tracked.

**Rules**

* Items must have a 'key' property which are either a system or user defined key that is unique at the level to which it appears.  Keys can not be changed once set and must be snake case.
* Items must have a 'name' property.
* Items must have a 'type' property that refer to an item type from the 'types' collection.
* Items can have 'tags' that are key/value pairs which describe the item where the key is snake case.  Item tag values are strings.
* Items can have 'items' which are dependencies to the item.  Attached items are also items that have a key, name, type and can have tags and also have attached items.

### Generic Rules

* Every object in the data structure supports a metadata property which is additional key-value attributes that can be used for various system or user needs such as change history tracking.  These key-value pairs are not considered type tags.
