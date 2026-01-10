package application

import "time"

type UrlStatDto struct {
	Id           string    `json:"id"`
	OriginalUrl  string    `json:"originalUrl"`
	LastAccessAt time.Time `json:"lastAccessAt"`
}
