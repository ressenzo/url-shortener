package repository

import (
	"time"

	"go.mongodb.org/mongo-driver/bson/primitive"
)

type UrlStatModel struct {
	Id               primitive.ObjectID `bson:"_id"`
	OriginalUrl      string             `bson:"OriginalUrl"`
	LastAccess       time.Time          `bson:"lastAccess"`
	AccessesQuantity int                `bson:"accessesQuantity"`
}
