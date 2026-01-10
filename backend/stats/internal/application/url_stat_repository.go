package application

import "url-shortener/stats/internal/domain"

type UrlStatRepository interface {
	SaveUrlStat(urlStat domain.UrlStat)
	GetUrlStat(id string) *domain.UrlStat
}
