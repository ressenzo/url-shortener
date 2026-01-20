package config
import (
	"log"
	"os"
	"url-shortener/stats/internal/application"
	"url-shortener/stats/internal/repository"

	"github.com/streadway/amqp"
)

func ConsumerConfig(useCase application.SaveUrlStatUseCase) {
	rabbitMqUrl := os.Getenv("RABBITMQ_URL")
	if rabbitMqUrl == "" {
		rabbitMqUrl = "amqp://guest:guest@localhost:5672/"
	}

	conn, err := amqp.Dial(rabbitMqUrl)
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
