package config

import (
	"context"
	"log"
	"os"

	"go.mongodb.org/mongo-driver/mongo"
	"go.mongodb.org/mongo-driver/mongo/options"
)

func DbConfig() *mongo.Database {
	mongoUri := os.Getenv("MONGODB_URI")
	if mongoUri == "" {
		mongoUri = "mongodb://admin:admin123@localhost:27017/?directConnection=true&serverSelectionTimeoutMS=2000"
	}

	client, err := mongo.Connect(
		context.Background(),
		options.Client().ApplyURI(mongoUri),
	)

	if err != nil {
		log.Fatal(err)
	}

	db := client.Database("url-shortener-stats")
	return db
}
