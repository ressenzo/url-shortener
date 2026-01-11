package application

import "url-shortener/stats/internal/domain"

type UrlStatRepository interface {
	SaveUrlStat(urlStat domain.UrlStat) (domain.UrlStat, error)
	GetUrlStat(id string) (*domain.UrlStat, error)
}
