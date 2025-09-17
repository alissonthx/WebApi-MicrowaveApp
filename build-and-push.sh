echo "Building images ..."
sudo docker build -f MicrowaveApp.WebApi/Dockerfile -t microwave-api:latest .

echo "Pushing images to Docker Hub..."

sudo docker tag microwave-api:latest alissonmmiquelace/microwave-api:latest
sudo docker push alissonmmiquelace/microwave-api:latest

echo "Building images ..."
sudo docker build -f MicrowaveApp.AvaloniaUI/Dockerfile -t alissonmmiquelace/microwave-avalonia-ui:latest .

echo "Pushing images to Docker Hub..."

sudo docker tag microwave-avaloniaui-ui:latest alissonmmiquelace/microwave-avaloniaui-ui:latest
sudo docker push alissonmmiquelace/microwave-avaloniaui-ui:latest

echo "Images pushed successfully!"