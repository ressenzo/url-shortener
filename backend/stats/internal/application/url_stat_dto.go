package application

import "time"

type UrlStatDto struct {
	Id           string    `json:"Id"`
	OriginalUrl  string    `json:"OriginalUrl"`
	LastAccessAt time.Time `json:"LastAccessAt"`
}
