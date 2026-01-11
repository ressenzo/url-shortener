package application

import (
	"errors"
	"testing"
	"time"
	"url-shortener/stats/internal/domain"
)

type mockUrlStatRepository struct {
	getUrlStatFunc  func(id string) (*domain.UrlStat, error)
	saveUrlStatFunc func(stat domain.UrlStat) (domain.UrlStat, error)
}

func (m *mockUrlStatRepository) GetUrlStat(id string) (*domain.UrlStat, error) {
	return m.getUrlStatFunc(id)
}

func (m *mockUrlStatRepository) SaveUrlStat(stat domain.UrlStat) (domain.UrlStat, error) {
	return m.saveUrlStatFunc(stat)
}

func TestSaveUrlStat_ErrorToGet(t *testing.T) {
	repo := &mockUrlStatRepository{
		getUrlStatFunc: func(id string) (*domain.UrlStat, error) {
			return nil, errors.New("database error")
		},
		saveUrlStatFunc: func(stat domain.UrlStat) (domain.UrlStat, error) {
			return domain.UrlStat{}, nil
		},
	}

	useCase := NewSaveUrlCase(repo)
	result := useCase.SaveUrl(UrlStatDto{Id: "short123"})

	if result {
		t.Error("expected false, got true")
	}
}

func TestSaveUrlStat_NewUrlStat(t *testing.T) {
	repo := &mockUrlStatRepository{
		getUrlStatFunc: func(id string) (*domain.UrlStat, error) {
			return nil, nil
		},
		saveUrlStatFunc: func(stat domain.UrlStat) (domain.UrlStat, error) {
			return domain.UrlStat{}, nil
		},
	}

	useCase := NewSaveUrlCase(repo)
	result := useCase.SaveUrl(UrlStatDto{
		Id:          "short123",
		OriginalUrl: "https://example.com",
	})

	if !result {
		t.Error("expected true, got false")
	}
}

func TestSaveUrlStat_ExistingUrlStat(t *testing.T) {
	existingStat := &domain.UrlStat{
		Id:               "short123",
		OriginalUrl:      "https://example.com",
		AccessesQuantity: 5,
		LastAccess:       time.Now(),
	}

	repo := &mockUrlStatRepository{
		getUrlStatFunc: func(id string) (*domain.UrlStat, error) {
			return existingStat, nil
		},
		saveUrlStatFunc: func(stat domain.UrlStat) (domain.UrlStat, error) {
			return *existingStat, nil
		},
	}

	useCase := NewSaveUrlCase(repo)
	result := useCase.SaveUrl(UrlStatDto{Id: "short123"})

	if !result {
		t.Error("expected true, got false")
	}
}

func TestSaveUrlStat_ErrorToSave(t *testing.T) {
	existingStat := &domain.UrlStat{
		Id:               "short123",
		OriginalUrl:      "https://example.com",
		AccessesQuantity: 5,
		LastAccess:       time.Now(),
	}

	repo := &mockUrlStatRepository{
		getUrlStatFunc: func(id string) (*domain.UrlStat, error) {
			return existingStat, nil
		},
		saveUrlStatFunc: func(stat domain.UrlStat) (domain.UrlStat, error) {
			return *existingStat, errors.New("database error")
		},
	}

	useCase := NewSaveUrlCase(repo)
	result := useCase.SaveUrl(UrlStatDto{Id: "short123"})

	if result {
		t.Error("expected false, got true")
	}
}
