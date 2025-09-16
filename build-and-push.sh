echo "Pushing images to Docker Hub..."
docker tag microwave-api:latest alissonmmiquelace/microwave-api:latest
docker push alissonmmiquelace/microwave-api:latest

docker tag microwave-avaloniaui-ui:latest alissonmmiquelace/microwave-avaloniaui-ui:latest
docker push alissonmmiquelace/microwave-avaloniaui-ui:latest

echo "Images pushed successfully!"