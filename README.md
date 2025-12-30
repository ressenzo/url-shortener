# URL Shortener

This project consists of multiple applications: a URL generator backend, a stats worker, and a React frontend, along with MongoDB and RabbitMQ as dependencies.

## Running the Applications

The entire environment is managed via Docker Compose. You can run the full stack or individual applications with their dependencies using profiles.

### Prerequisites
- Docker and Docker Compose installed

### Running the Full Environment
To start all services:
```bash
docker-compose --profile full up
```

### Running Individual Applications

#### URL Generator Backend
Starts the generator API with MongoDB and RabbitMQ:
```bash
docker-compose --profile generator up
```
- API available at: http://localhost:5000

#### Stats Worker
Starts the stats worker with MongoDB and RabbitMQ:
```bash
docker-compose --profile stats up
```

#### Frontend
Starts the React frontend with the generator backend and its dependencies:
```bash
docker-compose --profile frontend up
```
- Frontend available at: http://localhost:3000

### Running in Debug Mode

To run the backend services in debug mode (Development environment), set the `ASPNETCORE_ENVIRONMENT` variable:

```bash
ASPNETCORE_ENVIRONMENT=Development docker-compose --profile generator up
```

This enables detailed error pages, logging, and other development features for the .NET applications.

**Note:** The frontend runs as a production build in Docker. For frontend development with hot reload, run it locally using `npm start` in the `frontend/` directory.

### Stopping Services
To stop all running services:
```bash
docker-compose down
```

### Database Connection
When running locally (outside Docker), use:
```
"ConnectionStrings": {
    "Mongo": "mongodb://admin:admin123@localhost:27017?directConnection=true&serverSelectionTimeoutMS=2000"
}
```

Inside Docker containers, the connection string uses the service names:
```
mongodb://admin:admin123@url-shortener-mongo:27017/?authSource=admin
```
