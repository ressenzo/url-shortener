package main

import (
	"url-shortener/stats/cmd/config"
	"url-shortener/stats/internal/application"
	"url-shortener/stats/internal/repository"
)

func main() {
	db := config.DbConfig()
	urlStatRepo := repository.NewUrlStatRepository(db)
	useCase := application.NewSaveUrlCase(urlStatRepo)
	config.ConsumerConfig(useCase)
	select {}
}
