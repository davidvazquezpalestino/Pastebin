#!/bin/bash
set -e

BASE_DIR="$(cd "$(dirname "$0")/../.." && pwd)"
APP_DIR="$BASE_DIR"
IMAGE_NAME="pastebin-api"
BRANCH="main"
TZ="America/Mexico_City"

echo "====================================="
echo "Deploy API Infosoft (simple)"
echo "Rama: $BRANCH"
echo "Timezone: $TZ"
echo "====================================="

# 1. Obtener código
if [ ! -d "$APP_DIR/.git" ]; then
  echo "Clonando repositorio..."
  cd "$BASE_DIR"
  git clone -b $BRANCH https://github.com/davidvazquezpalestino/pastebin.git .
else
  echo "Actualizando repositorio..."
  cd "$APP_DIR"
  git fetch origin
  git checkout $BRANCH
  git reset --hard origin/$BRANCH
fi

# Volver al directorio raíz para el build de Docker
cd "$BASE_DIR"

# 2. Build de imagen
echo "Construyendo imagen Docker..."
docker build -t $IMAGE_NAME -f src/Pastebin.Api/Dockerfile .

# 3. Detener y eliminar contenedores existentes
echo "Eliminando contenedores previos..."
docker rm -f pastebin-api || true

docker run -d -e TZ=$TZ -p 8050:80 --name pastebin-api $IMAGE_NAME

echo "====================================="
echo "Deploy finalizado correctamente"
echo "====================================="
