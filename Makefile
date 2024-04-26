FRONT_END_BINARY=frontApp
BROKER_BINARY=brokerApp
AUTH_BINARY=authApp

## up: starts all containers in the background without forcing build
up:
	@echo "Starting Docker images..."
	docker-compose up
	@echo "Docker images started!"

## up_build: stops docker-compose (if running), builds all projects and starts docker compose
up_build:
	@echo "Stopping docker images (if running...)"
	docker-compose down
	@echo "Building (when required) and starting docker images..."
	docker-compose up --build
	@echo "Docker images built and started!"

## down: stop docker compose
down:
	@echo "Stopping docker compose..."
	docker-compose down
	@echo "Done!"

## build_motorcycle: builds the motorcycle api binary as a linux executable
build_client:
	@echo "Building motorcycle binary..."
	docker build -f src/services/Client/Client.API/Dockerfile  .
	@echo "Done!"

clean:
	@echo "Cleaning all..."
	docker system prune
	@echo "Done!"

test:
	@echo "Testing all..."
	dotnet clean
	dotnet test
	@echo "Done!"

bus:
	@echo "Starting bus server"
	docker run -it --rm --name rabbitmq --hostname rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management