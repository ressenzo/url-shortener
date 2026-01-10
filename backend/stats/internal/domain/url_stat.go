package domain

import "time"

type UrlStat struct {
	Id               string
	OriginalUrl      string
	AccessesQuantity int
	LastAccess       time.Time
}

func (u *UrlStat) AddAccess() {
	u.AccessesQuantity += 1
	u.LastAccess = time.Now()
}
