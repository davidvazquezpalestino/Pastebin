#!/bin/bash
set -e

BASE_DIR="/var/www/api-infosoft"
APP_DIR="$BASE_DIR/Infosoft.Backend.WebApi"
IMAGE_NAME="webapi-infosoft"
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
  git clone -b $BRANCH https://davidvazquezpalestino.visualstudio.com/Infosoft/_git/Infosoft.Backend.WebApi
else
  echo "Actualizando repositorio..."
  cd "$APP_DIR"
  git fetch origin
  git checkout $BRANCH
  git reset --hard origin/$BRANCH
fi

# 2. Build de imagen
echo "Construyendo imagen Docker..."
docker build -t $IMAGE_NAME .

# 3. Detener y eliminar contenedores existentes
echo "Eliminando contenedores previos..."
docker rm -f webapi-infosoft1 webapi-infosoft2 webapi-infosoft3 webapi-infosoft4 || true

# 4. Levantar nuevas instancias
echo "Levantando contenedores..."
docker run -d -e TZ=$TZ -p 8010:80 --name webapi-infosoft1 $IMAGE_NAME
docker run -d -e TZ=$TZ -p 8011:80 --name webapi-infosoft2 $IMAGE_NAME
docker run -d -e TZ=$TZ -p 8012:80 --name webapi-infosoft3 $IMAGE_NAME
docker run -d -e TZ=$TZ -p 8013:80 --name webapi-infosoft4 $IMAGE_NAME

echo "====================================="
echo "Deploy finalizado correctamente"
echo "====================================="
