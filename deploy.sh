#!/bin/bash
set -e

echo "====================================="
echo "Starting Full Deployment (API & App)"
echo "====================================="

# Deploy API
echo "Step 1: Deploying API..."
bash src/Pastebin.Api/deploy.sh

# Deploy App
echo "Step 2: Deploying App..."
bash src/Pastebin.Client/deploy.sh

echo "====================================="
echo "Full Deployment completed!"
echo "====================================="
