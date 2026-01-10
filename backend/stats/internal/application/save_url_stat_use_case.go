package application

import (
	"time"
	"url-shortener/stats/internal/domain"
)

type SaveUrlStatUseCase interface {
	SaveUrl(request UrlStatDto) bool
}

type saveUrlUseCase struct {
	urlStatRepo UrlStatRepository
}

func NewSaveUrlCase(repo UrlStatRepository) SaveUrlStatUseCase {
	return &saveUrlUseCase{
		urlStatRepo: repo,
	}
}

func (s *saveUrlUseCase) SaveUrl(request UrlStatDto) bool {
	urlStat := s.urlStatRepo.GetUrlStat(request.Id)
	if urlStat == nil {
		urlStat = &domain.UrlStat{
			Id:               request.Id,
			OriginalUrl:      request.OriginalUrl,
			LastAccess:       time.Now(),
			AccessesQuantity: 1,
		}
		s.urlStatRepo.SaveUrlStat(*urlStat)
	} else {
		urlStat.AddAccess()
		s.urlStatRepo.SaveUrlStat(*urlStat)
	}

	return true
}
