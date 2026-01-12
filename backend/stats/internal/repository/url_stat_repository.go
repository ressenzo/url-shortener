package repository

import (
	"context"
	"url-shortener/stats/internal/application"
	"url-shortener/stats/internal/domain"

	"go.mongodb.org/mongo-driver/bson"
	"go.mongodb.org/mongo-driver/mongo"
	"go.mongodb.org/mongo-driver/mongo/options"
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
		"_id": id,
	}
	var urlStatModel UrlStatModel
	err := u.collection.FindOne(context.Background(), filter).Decode(&urlStatModel)
	if err != nil {
		if err == mongo.ErrNoDocuments {
			return nil, nil
		}
		return nil, err
	}

	urlStat := domain.UrlStat{
		Id:               urlStatModel.Id,
		OriginalUrl:      urlStatModel.OriginalUrl,
		AccessesQuantity: urlStatModel.AccessesQuantity,
		LastAccess:       urlStatModel.LastAccess,
	}
	return &urlStat, nil
}

func (u *urlStatRepository) SaveUrlStat(urlStat domain.UrlStat) (domain.UrlStat, error) {
	urlStatModel := UrlStatModel{
		Id:               urlStat.Id,
		OriginalUrl:      urlStat.OriginalUrl,
		LastAccess:       urlStat.LastAccess,
		AccessesQuantity: urlStat.AccessesQuantity,
	}
	filter := bson.M{"_id": urlStatModel.Id}
	opts := options.Replace().SetUpsert(true)
	_, err := u.collection.ReplaceOne(context.Background(), filter, urlStatModel, opts)
	return urlStat, err
}
