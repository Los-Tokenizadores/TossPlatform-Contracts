#!/bin/bash
rm -R Toss.Contracts/ByteCode
FOUNDRY_PROFILE=paris forge build -o ./Toss.Contracts/ByteCode/paris
./ByteCodeExtractor/ByteCodeExtractor paris
FOUNDRY_PROFILE=shanghai forge build -o ./Toss.Contracts/ByteCode/shanghai
./ByteCodeExtractor/ByteCodeExtractor shanghai
