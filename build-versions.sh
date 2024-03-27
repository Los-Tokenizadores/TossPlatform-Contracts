#!/bin/bash
forge build -o ./Toss.Contracts/ByteCode/paris --evm-version paris
./ByteCodeExtractor/ByteCodeExtractor paris
forge build -o ./Toss.Contracts/ByteCode/shanghai --evm-version shanghai
./ByteCodeExtractor/ByteCodeExtractor shanghai
