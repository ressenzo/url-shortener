package config

import (
	"log"
	"url-shortener/stats/internal/application"
	"url-shortener/stats/internal/repository"

	"github.com/streadway/amqp"
)

func ConsumerConfig(useCase application.SaveUrlStatUseCase) {
	conn, err := amqp.Dial("amqp://guest:guest@localhost:5672/")
	if err != nil {
		log.Fatal(err)
	}

	ch, err := conn.Channel()
	if err != nil {
		log.Fatal(err)
	}

	queueName := "url-shortener.access"
	ch.QueueDeclare(queueName, true, false, false, false, nil)

	consumer := repository.NewRabbitMqConsumer(
		ch,
		queueName,
		useCase,
	)
	if err := consumer.Start(); err != nil {
		log.Fatal(err)
	}

	log.Println("Consumer running...")
}
