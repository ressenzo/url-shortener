## MongoDB
To create MongoDb container, execute:
```
docker run -d --name mongodb -p 27017:27017 -e MONGO_INITDB_ROOT_USERNAME=admin -e MONGO_INITDB_ROOT_PASSWORD=admin123 mongo:latest
```

If you want to persist storage, execute:
```
docker run -d --name mongodb -p 27017:27017 -v mongo-data:/data/db -e MONGO_INITDB_ROOT_USERNAME=admin -e MONGO_INITDB_ROOT_PASSWORD=admin123 mongo:latest
```

And to connect:
```
docker exec -it mongodb mongosh -u admin -p admin123
```

Because of that, your ```appsettings.json``` must have the following block code:
```
"ConnectionStrings": {
    "Mongo": "mongodb://admin:admin123@localhost:27017?directConnection=true&serverSelectionTimeoutMS=2000"
}
```
