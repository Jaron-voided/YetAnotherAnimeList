#!/bin/bash

API_URL="https://localhost:5001"
HTTP_CMD="http --pretty=all --verify=no  --check-status"

# ───────────────────────────────────────────
#  COLORS
# ───────────────────────────────────────────
GREEN="\e[32m"
RED="\e[31m"
CYAN="\e[36m"
RESET="\e[0m"

# ───────────────────────────────────────────
#  Test Runner Function
# ───────────────────────────────────────────
run_test() {
    DESCRIPTION=$1
    shift
    COMMAND=$@

    echo -e "\n${CYAN}=== $DESCRIPTION ===${RESET}"
    echo -e "Running: $COMMAND\n"

    # Execute the command
    bash -c "$COMMAND"
    RESULT=$?

    if [ $RESULT -eq 0 ]; then
        echo -e "${GREEN}✔ PASSED${RESET}"
    else
        echo -e "${RED}✘ FAILED${RESET}"
    fi
}

# ───────────────────────────────────────────
#  TESTS BEGIN
# ───────────────────────────────────────────

# 1) Anime Tests
run_test "Get Anime by ID (Cowboy Bebop)" \
    "$HTTP_CMD $API_URL/api/anime/1"

run_test "Get Anime by ID (Dandadan future entry)" \
    "$HTTP_CMD $API_URL/api/anime/62516"

run_test "Search Anime by Title 'Trigun'" \
    "$HTTP_CMD $API_URL/api/anime/search title==Trigun"

run_test "Filter Anime by Min Score 8.5" \
    "$HTTP_CMD $API_URL/api/anime/minScore minScore==8.5"

# 2) User Tests
run_test "Create User (testuser1)" \
    "$HTTP_CMD POST $API_URL/api/users username=testuser1 email=test@example.com"

run_test "Get User by Username (testuser1)" \
    "$HTTP_CMD $API_URL/api/users/by-username/testuser1"

run_test "Get User by ID (1)" \
    "$HTTP_CMD $API_URL/api/users/1"

run_test "Delete User (1)" \
    "$HTTP_CMD DELETE $API_URL/api/users/1"

# Recreate user to keep DB clean afterward
run_test "Recreate User (testuser1)" \
    "$HTTP_CMD POST $API_URL/api/users username=testuser1 email=test@example.com"

# 3) User Anime Entries
run_test "Get UserAnimeEntry list for user 1" \
    "$HTTP_CMD $API_URL/api/users/1/entries"

echo -e "\n${CYAN}=== ALL TESTS COMPLETE ===${RESET}\n"
