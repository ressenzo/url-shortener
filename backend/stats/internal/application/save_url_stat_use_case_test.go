package application

import (
	"testing"
	"time"
	"url-shortener/stats/internal/domain"
)

type mockUrlStatRepository struct {
	savedStats map[string]*domain.UrlStat
	getCount   int
	saveCount  int
}

func (m *mockUrlStatRepository) GetUrlStat(id string) *domain.UrlStat {
	m.getCount++
	return m.savedStats[id]
}

func (m *mockUrlStatRepository) SaveUrlStat(stat domain.UrlStat) {
	m.saveCount++
	m.savedStats[stat.Id] = &stat
}

func TestSaveUrlNewStat(t *testing.T) {
	repo := &mockUrlStatRepository{savedStats: make(map[string]*domain.UrlStat)}
	useCase := NewSaveUrlCase(repo)

	result := useCase.SaveUrl(
		UrlStatDto{
			Id:          "test-id",
			OriginalUrl: "https://example.com",
		},
	)

	if !result {
		t.Errorf("SaveUrl should return true")
	}
	if repo.saveCount != 1 {
		t.Errorf("expected 1 save, got %d", repo.saveCount)
	}
	if repo.savedStats["test-id"].AccessesQuantity != 1 {
		t.Errorf("expected AccessesQuantity 1, got %d", repo.savedStats["test-id"].AccessesQuantity)
	}
}

func TestSaveUrlExistingStat(t *testing.T) {
	existingStat := &domain.UrlStat{
		Id:               "test-id",
		OriginalUrl:      "https://example.com",
		LastAccess:       time.Now().Add(-time.Hour),
		AccessesQuantity: 5,
	}
	repo := &mockUrlStatRepository{savedStats: map[string]*domain.UrlStat{"test-id": existingStat}}
	useCase := NewSaveUrlCase(repo)

	result := useCase.SaveUrl(UrlStatDto{
		Id:          "test-id",
		OriginalUrl: "https://example.com",
	})

	if !result {
		t.Errorf("SaveUrl should return true")
	}
	if repo.saveCount != 1 {
		t.Errorf("expected 1 save, got %d", repo.saveCount)
	}
	if repo.savedStats["test-id"].AccessesQuantity != 6 {
		t.Errorf("expected AccessesQuantity 6, got %d", repo.savedStats["test-id"].AccessesQuantity)
	}
}
