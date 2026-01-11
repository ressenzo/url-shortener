package repository

import (
	"encoding/json"
	"log"
	"url-shortener/stats/internal/application"

	"github.com/streadway/amqp"
)

type rabbitMqConsumer struct {
	channel   *amqp.Channel
	queueName string
	useCase   application.SaveUrlStatUseCase
}

func NewRabbitMqConsumer(
	channel *amqp.Channel,
	queueName string,
	useCase application.SaveUrlStatUseCase,
) *rabbitMqConsumer {
	return &rabbitMqConsumer{
		channel:   channel,
		queueName: queueName,
		useCase:   useCase,
	}
}

func (c *rabbitMqConsumer) Start() error {
	msgs, err := c.channel.Consume(
		c.queueName,
		"",
		true,
		false,
		false,
		false,
		nil,
	)

	if err != nil {
		return err
	}

	go func() {
		for msg := range msgs {
			var payload application.UrlStatDto
			if err := json.Unmarshal(msg.Body, &payload); err != nil {
				log.Println("Invalid message", err)
				return
			}

			result := c.useCase.SaveUrl(payload)

			if result {
				c.channel.Ack(msg.DeliveryTag, false)
			} else {
				c.channel.Nack(msg.DeliveryTag, false, true)
			}
		}
	}()

	return nil
}
