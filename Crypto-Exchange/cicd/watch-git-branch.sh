#!/bin/bash

BRANCH="main"  # Schimbă dacă folosești alt branch
INTERVAL=60    # Cât de des verificăm (secunde)

echo "Monitorizare modificări pe branch-ul '$BRANCH'..."

# Mergem în directorul proiectului
cd "$(dirname "$0")"

# Inițializăm commit-ul anterior pentru comparație
LAST_COMMIT=$(git rev-parse $BRANCH)

while true; do
    sleep $INTERVAL  # Așteptăm X secunde

    # Tragem schimbările (fără a modifica nimic)
    git fetch origin $BRANCH > /dev/null 2>&1

    # Obținem commit-ul cel mai nou
    NEW_COMMIT=$(git rev-parse origin/$BRANCH)

    if [ "$LAST_COMMIT" != "$NEW_COMMIT" ]; then
        echo "🔄 Schimbare detectată în '$BRANCH'! Redeploy..."
        git pull origin $BRANCH
        ./local-ci-cd.sh
        LAST_COMMIT=$NEW_COMMIT  # Actualizăm commit-ul urmărit
    fi
done
