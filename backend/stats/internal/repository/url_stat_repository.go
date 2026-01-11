package repository

import (
	"context"
	"url-shortener/stats/internal/application"
	"url-shortener/stats/internal/domain"

	"go.mongodb.org/mongo-driver/bson"
	"go.mongodb.org/mongo-driver/mongo"
)

type urlStatRepository struct {
	collection *mongo.Collection
}

func NewUrlStatRepository(db *mongo.Database) application.UrlStatRepository {
	return &urlStatRepository{
		collection: db.Collection("urls"),
	}
}

func (u *urlStatRepository) GetUrlStat(id string) (*domain.UrlStat, error) {
	filter := bson.M{
		"id": id,
	}
	var urlStat domain.UrlStat
	err := u.collection.FindOne(context.Background(), filter).Decode(&urlStat)
	if err != nil {
		return nil, err
	}

	return &urlStat, nil
}

func (u *urlStatRepository) SaveUrlStat(urlStat domain.UrlStat) (domain.UrlStat, error) {
	_, err := u.collection.InsertOne(context.Background(), urlStat)
	return urlStat, err
}
