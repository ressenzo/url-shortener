package repository

import (
	"time"
)

type UrlStatModel struct {
	Id               string    `bson:"_id"`
	OriginalUrl      string    `bson:"OriginalUrl"`
	LastAccess       time.Time `bson:"LastAccess"`
	AccessesQuantity int       `bson:"AccessesQuantity"`
}
