#!/bin/bash

set -e  # Oprește execuția dacă apare o eroare

echo "=== PASUL 1: Oprire și ștergere toate volumele și containerele ==="
docker compose down -v

echo "=== PASUL 2: Construire și repornire containere ==="
docker compose up --build -d

echo "=== PASUL 3: Verificare status containere ==="
docker ps

echo "=== Pipeline finalizat cu succes! ==="
