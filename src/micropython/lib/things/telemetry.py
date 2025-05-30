#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Telemetry objects and standarized messaging
"""
# ======================================================================
__company__   = 'Starlight Software Co.'
__author__    = 'Mark Lanning'
__copyright__ = '2025 Starlight Software Co.'
__version__   = '0.1.0'
# ======================================================================

from things.item import ThingItem


# ======================================================================
# Telemetry events and encoding
# ======================================================================
class Telemetry():
    def __init__(self):        
        self.events = {} 
        self.sequence = 0
    
    # add the event to the correct pile of events
    def add(self, event):        
        # see if we need to make a new array of items
        if event.type not in self.events:
            self.events[event.type] = [] # start new array
        
            # add our sequencing numbering in case we need to rebuild or check we didn't miss something
            if(event.sequence is None):                
                event.sequence = self.sequence
                self.sequence += 1 # increment since we are used it
                
            self.events[event.type].append(event)
        else:
            self.user_events.append(event)

# ======================================================================
class TelemetryEvent(ThingItem):
    def __init__(self, epoch, type:str, sequence:int = None, tags = {}):
        assert epoch is not None, 'Timestamp is required for telemetry events'
            
        super().__init__(epoch, type, tags)
        self.sequence = sequence
        
    @staticmethod
    def parse(sentence):
        
        item = ThingItem.parse(sentence)
        
        return TelemetryEvent(epoch=item.date, type=item.type, tags=item.tags)
        

def epoch_ms():
    return (utime.time() * 1000) + time.ticks_ms() % 1000
    
    # 1 748 382 243
    # 801 679 718
    
    