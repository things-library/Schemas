---
layout: default
title: HTTP Headers
permalink: /http-headers
---

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



