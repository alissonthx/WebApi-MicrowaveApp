echo "Pushing images to Docker Hub..."
docker tag microwave-api:latest alissonmmiquelace/microwave-api:latest
docker push alissonmmiquelace/microwave-api:latest

docker tag microwave-maui-ui:latest alissonmmiquelace/microwave-maui-ui:latest
docker push alissonmmiquelace/microwave-maui-ui:latest

echo "Images pushed successfully!"