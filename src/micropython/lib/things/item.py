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

import time

# ======================================================================
class ThingItem():
    def __init__(self, epoch, type:str, tags = {}):        
        self.date = epoch
        self.type = type                
        self.tags = tags # set initial tags
        self.items = tags


    def sentence(self) -> str:
        parts = [f'${self.date}', self.type]
        
        for key in self.tags:
            parts.append(f"{key}:{self.tags[key]}")

        sentence = "|".join(parts)

        # append checksum
        checksum = self.checksum(sentence)

        return f"{sentence}*{checksum}" 


    def checksum(self, sentence) -> str:
        
        # Not the beginning of a sentence
        if len(sentence) == 0 or sentence[0] != '$':
            print('Invalid Sentence: ', sentence)
            return None

        # Start with the first item (checksum is the byte value of the second character)
        checksum = ord(sentence[1])

        # Loop through all chars to get a checksum
        i = 2
        while i < len(sentence):
            if sentence[i] == '*':
                break
            
            checksum ^= ord(sentence[i])  # XOR the checksum with this character's value
            i += 1

        # Append the checksum formatted as a two-character hexadecimal
        return '{:02X}'.format(checksum)


    @staticmethod
    def parse(sentence):
        
        if not sentence or len(sentence) == 0:
            raise ValueError("Telemetry sentence is null or whitespace")

        if not ThingItem.validate_checksum(sentence):
            raise ValueError("Invalid checksum")

        num = sentence.rfind('*')
        if num == -1:
            raise ValueError("Missing checksum delimiter '*'")

        content = sentence[1:num]
        parts = content.split('|')
        
        item = ThingItem(parts[0], type=parts[1])

        for i in range(2, len(parts)):
            pair = parts[i].split(':', 1)
            if len(pair) == 2:
                key, value = pair
                item.tags[key] = value

        return item

            

    @staticmethod
    def append_checksum(sentence):
        """
        Append the checksum to the sentence.
        The checksum is calculated using XOR of all characters between '$' and '*'.
        """
        if not sentence or sentence[0] != '$':
            return sentence

        checksum = ord(sentence[1])
        i = 2
        while i < len(sentence):
            if sentence[i] == '*':
                break
            checksum ^= ord(sentence[i])
            i += 1

        if '*' not in sentence:
            sentence += '*'

        sentence += '{:02X}'.format(checksum)
        return sentence


    @staticmethod
    def to_checksum(sentence):
        """
        Calculate the checksum of the sentence.
        Returns a two-character hexadecimal string.
        """
        if not sentence or sentence[0] != '$':
            return ''

        checksum = ord(sentence[1])
        for i in range(2, len(sentence)):
            if sentence[i] == '*':
                break
            checksum ^= ord(sentence[i])

        return '{:02X}'.format(checksum)


    @staticmethod
    def validate_checksum(sentence):
        """
        Validate that the checksum at the end of the sentence is correct.
        """
        checksum = ThingItem.to_checksum(sentence)
        if not checksum:
            return False
        
        return sentence.endswith(checksum)

