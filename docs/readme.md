# Things Library Open Schema

## Overview

The general concept of the Things Library is that every library item has a series of key-value attributes that describe the item.
Every item can have attachments which are other items that have key/value attributes. Attributes are global to the item type to which they belong and help explain what the value is and how to parse it such as a pick list item or a date or time or decimal number.

One of the goals is to make the general data structure as simple as possible at the item level. In order to have a proper item data structure, it must have a name, type.

There are two core schema documents.  The **library schema** is designed to be a robust schema to hold many items that are all tied together in various ways such as item attributes are tied to type attributes.  The **canvas schema** is designed to provide a strongly typed definition language around application services.

## Schema Documents

The latest schema documents are always found in the **/latest** folder but within the document will refer to the actual version it represents:

* [Library Schema](https://schema.thingslibrary.io/latest/library.json)
* [Service Canvas Schema](https://schema.thingslibrary.io/latest/canvas.json)