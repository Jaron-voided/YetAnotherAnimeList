#!/usr/bin/env bash

# Base URL for your API
BASE="https://localhost:5001/api/anime"

# HTTPie options for local HTTPS
OPTS="--verify=no"

echo ""
echo "====================================="
echo "  TEST: Get Anime by ID (MalId = 1)"
echo "====================================="
http $OPTS $BASE/1


echo ""
echo "====================================="
echo "  TEST: Search Anime by Title"
echo "  Query: 'Naruto'"
echo "====================================="
http $OPTS "$BASE/search" title=="Naruto"


echo ""
echo "====================================="
echo "  TEST: Search by Minimum Score"
echo "  score >= 8.0"
echo "====================================="
http $OPTS "$BASE/minScore" minScore==8.0


echo ""
echo "====================================="
echo "  TEST: Search by Type"
echo "  Example: type==TV"
echo "====================================="
http $OPTS "$BASE/type" type==TV


echo ""
echo "====================================="
echo "  TEST: Search by Status"
echo "  Example: status==Airing"
echo "====================================="
http $OPTS "$BASE/status" status==Airing


echo ""
echo "====================================="
echo "  TEST: Search by Rating"
echo "  Example: rating==R"
echo "====================================="
http $OPTS "$BASE/rating" rating==R


echo ""
echo "====================================="
echo "  ALL TESTS COMPLETE"
echo "====================================="
