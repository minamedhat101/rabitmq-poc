using System.Text;
using RabbitMQ.Client;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
factory.ClientProvidedName = "Sender app";

IConnection connection = factory.CreateConnection();
IModel channel = connection.CreateModel();

string exchangeName = "DemoExchange";
string routingKey = "demo-routing-key";
string queueName = "DemoQueue";

channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, durable:false, exclusive: false, autoDelete: false, arguments: null);
channel.QueueBind(queueName, exchangeName, routingKey, arguments: null);

string message = "Hello World";
byte[] messageBytes = Encoding.UTF8.GetBytes(message);
channel.BasicPublish(exchangeName, routingKey, false, basicProperties: null, messageBytes);

channel.Close();
connection.Close();
