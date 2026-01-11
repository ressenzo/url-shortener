package application

import (
	"log"
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
	urlStat, err := s.urlStatRepo.GetUrlStat(request.Id)
	if err != nil {
		log.Println("Error to get url stat", err)
		return false
	}
	if urlStat == nil {
		urlStat = &domain.UrlStat{
			Id:               request.Id,
			OriginalUrl:      request.OriginalUrl,
			LastAccess:       time.Now(),
			AccessesQuantity: 1,
		}
	} else {
		urlStat.AddAccess()
	}
	_, err = saveUrlStat(s, urlStat)
	if err != nil {
		log.Println("Error to save url stat", err)
		return false
	}

	return true
}

func saveUrlStat(s *saveUrlUseCase, urlStat *domain.UrlStat) (domain.UrlStat, error) {
	return s.urlStatRepo.SaveUrlStat(*urlStat)
}
