package repository

import (
	"url-shortener/stats/internal/application"
	"url-shortener/stats/internal/domain"
)

type urlStatRepository struct {
}

func NewUrlStatRepository() application.UrlStatRepository {
	return &urlStatRepository{}
}

func (u *urlStatRepository) GetUrlStat(id string) *domain.UrlStat {
	panic("unimplemented")
}

func (u *urlStatRepository) SaveUrlStat(urlStat domain.UrlStat) {
	panic("unimplemented")
}
