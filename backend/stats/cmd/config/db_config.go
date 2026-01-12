package config

import (
	"context"
	"log"

	"go.mongodb.org/mongo-driver/mongo"
	"go.mongodb.org/mongo-driver/mongo/options"
)

func DbConfig() *mongo.Database {
	client, err := mongo.Connect(
		context.Background(),
		options.Client().ApplyURI("mongodb://admin:admin123@localhost:27017/?directConnection=true&serverSelectionTimeoutMS=2000"),
	)

	if err != nil {
		log.Fatal(err)
	}

	db := client.Database("url-shortener-stats")
	return db
}
