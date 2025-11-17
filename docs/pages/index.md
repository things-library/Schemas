---
layout: default
title: Overview
permalink: /
---

# Schema Overview

The general concept of the Things Library is that every item has a 'type' of item which has a series of key-value attributes that describe that type of item.
Every item can have attachments which are other items that have key/value attributes. Attributes are global to the item type to which they belong and help explain what the value is and how to parse it such as a pick list item or a date or time or decimal number.

One of the goals is to make the general data structure as simple as possible at the item level. In order to have a proper item data structure, it must have a unique key, type and name.

There are two core schema documents.  The **telemetry schema** is designed to be as flexible as possible with the least number of requirements as the only requrements is a date and a type.
The **library schema** is designed to be a robust schema to hold many items that are all tied together in various ways such as item attributes are tied to type attributes.

## Schema Documents

The latest schema documents are always found in the **/latest** folder but within the document it will refer to the actual version it represents:

* [Item Schema](item-schema.md)
* [Library Schema](library-schema.md)
* [Telemetry / Logging](telemetry.md)
