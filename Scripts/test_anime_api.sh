#!/usr/bin/env bash

BASE="https://localhost:5001/api/anime"
OPTS="--verify=no"

echo "===== Get by ID (Cowboy Bebop) ====="
http $OPTS "$BASE/1" | jq '{malId, title, startDate, endDate, score, streaming}'

echo "===== Get by ID (future anime w/ nulls) ====="
http $OPTS "$BASE/62516" | jq '{malId, title, startDate, endDate, score, streaming}'

echo "===== Search by Title ====="
http $OPTS "$BASE/search" title=="Trigun" | jq 'map({malId, title, streaming})[:5]'

echo "===== Search by Minimum Score (8+) ====="
http $OPTS "$BASE/minScore" minScore==8 | jq 'map({title, score})[:5]'

echo "===== Search by Rating (R) ====="
http $OPTS "$BASE/rating" rating==R | jq 'map({title, rating})[:5]'
