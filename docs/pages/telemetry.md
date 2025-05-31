---
layout: default
title: Telemetry
permalink: /telemetry
---

# Telemetry

The library schema supports robust logging and telemetry using schema backed attributes.  The telemetry sentence structure is based on extensive work over the years with NMEA 0183 GPS sentences which are designed to be resilent and performant utilizing a simple way to calculate checksum.



**Rules**

* Never use decimal values as they are culture and region specific.
* Scale all values to be integers, Example: 72.1% humidity is scaled to 721 with a scale factor of 10.
* Use short symbol values for type and tag attributes as display names are part of the schema and not needing to be transmitted and stored in every message.

## Sentences Structure:

* Sentence always starts with a '$' 
* Timestamp immediately follows the '$'
* End of the sentence is marked with a '*'
* Two digit checksum value immediately after the '*'
* Line feed character follows the two digit checksum but not required.
* Avoid the use of decimals and commas. (Due to being culture specific)

| Segment | Description |
| -- | -- |
| $ | Beginning of a sentence and does not assume the beginning |
| ############# | EPOCH time in seconds (10 digit) or miliseconds (13 digit) |
| Sentence ID | Key value representing the type of sentence | 
| {key}:{value} | Payload is a series of key/value pairs in no specific order |
| * | Ending of the sentence payload |
| XX | Checksum is a simple XOR of all bytes between $ and * |

```
$1724387844026|BT|r:1*17
$1724387845908|PV|r:1|p:4618*65
$1724387847251|SC|r:1|s:150|p:Morphine*1E
$1724387847931|PA|r:1|s:150|p:Morphine|q:1|p:000*01
$1724387848782|SC|r:1|s:143|p:PPE Mask*70
$1724387849602|PA|r:1|s:143|p:PPE Mask|q:1|p:000*79
$1724387850520|ET|r:1|q:2*33
```

#### Pattern:  ${EPOCH_TIME}|{SENTENCE_ID}|{Data_Item_1}|{Data_Item_2}|{Data_Item_3}*{CHECKSUM}
```
$1724387868113|SENS|t:215|h:471|p:965|sig:-68*42
```
Item Version (MQTT Payload):
```json
{
  "type": "sens",
  "date": 1724387868113,
  "tags": {
    "t": 215,
    "h": 471,
    "p": 965,
    "sig": -68
  }
}
```
or
```json
{"date":1724387868113,"type":"sens","tags":{"t":215,"h":471,"p":965,"sig":-68}}
```
