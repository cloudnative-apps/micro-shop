# micro-shop
Docker, Kubernets, Devops, ELK stack, Microservice patterns
------------------------------
Start Docker Images
run  = docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
stop = docker-compose -f docker-compose.yml -f docker-compose.override.yml down
--
See images
docker images

See running containers
docker ps
--
See application locally
TEST
http://localhost:8000/swagger/index.html
http://localhost:8001/
--
Stop Running Containers
stop = docker-compose -f docker-compose.yml -f docker-compose.override.yml down
-- --
Install the Azure CLI
	https://docs.microsoft.com/en-us/cli/azure/install-azure-cli
	https://docs.microsoft.com/en-us/cli/azure/install-azure-cli-windows?tabs=azure-cli
--
az version

{
  "azure-cli": "2.16.0",
  "azure-cli-core": "2.16.0",
  "azure-cli-telemetry": "1.0.6",
  "extensions": {}
}
--
az login
--
Create a resource group
az group create --name myResourceGroup --location westeurope
--
Create an Azure Container Registry
az acr create --resource-group myResourceGroup --name shoppingacr --sku Basic
az acr create --resource-group micro-shop-rg --name microshopacr --sku Basic

--
Enable Admin Account for ACR Pull
az acr update -n shoppingacr --admin-enabled true
az acr update -n microshopacr --admin-enabled true

--
Log in to the container registry
az acr login --name shoppingacr
Login Succeeded
--
Tag a container image

get the login server address
az acr list --resource-group myResourceGroup --query "[].{acrLoginServer:loginServer}" --output table
shoppingacr.azurecr.io
microshopacr.azurecr.io
--
Tag your images

docker tag shoppingapi:latest shoppingacr.azurecr.io/shoppingapi:v1
docker tag shoppingclient:latest shoppingacr.azurecr.io/shoppingclient:v1

-------------------------microshop--------------------------
docker tag basketapi:latest microshopacr.azurecr.io/basketapi:v1
docker tag catalogapi:latest microshopacr.azurecr.io/catalogapi:v1
docker tag discountapi:latest microshopacr.azurecr.io/discountapi:v1
docker tag discountgrpc:latest microshopacr.azurecr.io/discountgrpc:v1
docker tag ocelotapigw:latest microshopacr.azurecr.io/ocelotapigw:v1
docker tag orderingapi:latest microshopacr.azurecr.io/orderingapi:v1
docker tag shopclient:latest microshopacr.azurecr.io/shopclient:v1
docker tag shoppingaggregator:latest microshopacr.azurecr.io/shoppingaggregator:v1

docker push microshopacr.azurecr.io/discountgrpc:v1
docker push microshopacr.azurecr.io/ocelotapigw:v1
docker push microshopacr.azurecr.io/orderingapi:v1
docker push microshopacr.azurecr.io/shopclient:v1
docker push microshopacr.azurecr.io/shoppingaggregator:v1


microshopacr.azurecr.io/basketapi:v1
microshopacr.azurecr.io/catalogapi:v1
microshopacr.azurecr.io/discountapi:v1
microshopacr.azurecr.io/discountgrpc:v1
microshopacr.azurecr.io/ocelotapigw:v1
microshopacr.azurecr.io/orderingapi:v1
microshopacr.azurecr.io/shopclient:v1
microshopacr.azurecr.io/shoppingaggregator:v1

imagePullPolicy: IfNotPresent

Check
docker images
--
Push images to registry

docker push shoppingacr.azurecr.io/shoppingapi:v1
docker push shoppingacr.azurecr.io/shoppingclient:v1
--
List images in registry
az acr repository list --name shoppingacr --output table
az acr repository list --name microshopa --output table

Result
shoppingapi
shoppingclient
--
See tags
az acr repository show-tags --name shoppingacr --repository shoppingclient --output table

Result
v1
--
Create AKS cluster with attaching ACR
az aks create --resource-group myResourceGroup --name myAKSCluster --node-count 1 --generate-ssh-keys --attach-acr shoppingacr

az aks create --resource-group micro-shop-rg --name MicroShopAksCluster --node-count 1 --generate-ssh-keys --attach-acr microshopacr


--
Install the Kubernetes CLI
az aks install-cli
--
Connect to cluster using kubectl
az aks get-credentials --resource-group micro-shop-rg --name MicroShopAksCluster

shoppingacr.azurecr.io 
kubectl create secret docker-registry acr-secret --docker-server=microshopacr.azurecr.io --docker-username=microshopacr --docker-password=uwU0/mfASKSZSELnGvENbK72/7wN4iMX --docker-email=sharadit2011@hotmail.com  

To verify;
kubectl get nodes
kubectl get all
--
Check Kube Config

kubectl config get-contexts
kubectl config current-context
kubectl config use-context gcpcluster-k8s-1
	Switched to context "gcpcluster-k8s-1"


--
Deploy microservices to AKS

kubectl apply -f .\aks\
--
Clean All AKS and Azure Resources

az group delete --name myResourceGroup --yes --no-wait

-----------------------------------
-----------------------------------
