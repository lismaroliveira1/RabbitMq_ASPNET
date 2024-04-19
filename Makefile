## up: starts all containers in the background without forcing build
up:
	@echo "Starting Docker images..."
	docker build .
	docker-compose up -d
	@echo "Docker images started!"

## up_build: start services
start:
	@echo "Starting services..."
	@echo "Starting Client Service"
	dotnet run --project src/services/Client/Client.API/Client.API.csproj
	@echo "Starting Logger Service"
	dotnet run --project src/services/ServerLogger/ServerLogger.API/ServerLogger.API.csproj
	read "Sevices started succesfully... Press enter to continue"

## down: stop docker compose
build:
	@echo "Building all services..."
	dotnet build .
	@echo

