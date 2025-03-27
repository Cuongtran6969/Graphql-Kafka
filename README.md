# README: GraphQL API with Kafka Messaging

## Overview
This project demonstrates how to implement a GraphQL API using **GraphQL .NET** and integrate it with **Apache Kafka** for event-driven messaging. It allows CRUD operations on student data and publishes messages to Kafka topics when student events occur.

## Features
- **GraphQL API** for managing students (Create, Update, Delete)
- **Kafka Integration** to send messages when student data changes
- **Event-Driven Architecture** using `StudentEvent`

## Installation
### Prerequisites
- .NET 6 or later
- Apache Kafka (running on `localhost:9092` or your configured instance)
- A tool to test GraphQL queries (e.g., **Banana Cake Pop**, **Postman**, or **GraphQL Playground**)

### Setup
1. **Clone the Repository**
   ```sh
   git clone https://github.com/Cuongtran6969/Graphql-Kafka
   cd GraphQLPractive
   ```
2. **Install Dependencies**
   ```Confluent.Kafka, GraphQL, GraphQL.DataLoader, GraphQL.Server.Transports.AspNetCore
   ```GraphQL.Server.Ui.Playground, Microsoft.EntityFrameworkCore.SqlServer, Microsoft.EntityFrameworkCore.Tools
3. **Run Kafka in docker**
   ```docker-compose up -d

4. **Run the Project**
   ```sh
   dotnet run
   ```

## GraphQL API
### Endpoints
- **Create a Student** (`createStudent`)
- **Update a Student** (`updateStudent`)
- **Delete a Student** (`deleteStudent`)

### Example Queries
#### Create a Student
```https://localhost:[your-port]/graphql
{
"query": "mutation { createStudent(name: \"Tran Van Cuong\", age: 21, class: \"12A4\") { id name age class createdAt updatedAt } }"
}
```
#### Update a Student
```https://localhost:[your-port]/graphql
{
"query": "mutation { updateStudent(id: 1035, name: \"Tran Van Cuong\", age: 22, class: \"11A1\") { id name age class createdAt updatedAt } }"
}
```
#### Delete a Student
```https://localhost:[your-port]/graphql
{
"query": "mutation { deleteStudent(id: 1035) }"
}
```

## Kafka Integration
The project uses **KafkaProducerService** to send messages when student records are created, updated, or deleted.

### Kafka Event Triggers
| Event               | Kafka Message Sent |
|---------------------|------------------|
| `StudentCreated`   | `{ student }` |
| `StudentUpdated`   | `{ status, message, data, timestamp }` |
| `StudentDeleted`   | `{ status, message, timestamp }` |

### Kafka Producer Example
When a student is created, the following JSON message is sent to Kafka:
```json
{
    "data": {
        "createStudent": {
            "id": "1035",
            "name": "Tran Dang Anh",
            "age": 24,
            "class": "12A1",
            "createdAt": "2025-03-27T18:26:37.8724107Z",
            "updatedAt": null
        }
    }
}
```

### Configuring Kafka Producer
Kafka messages are produced to the topic **api-responses**.

Modify `KafkaProducerService` to change Kafka settings:
```csharp
var config = new ProducerConfig {
    BootstrapServers = "localhost:9092"
};
```

## Conclusion
This project integrates GraphQL API with Kafka for real-time messaging. By leveraging event-driven communication, the system ensures scalable and responsive API interactions. For further improvements, consider implementing a Kafka consumer to process messages asynchronously.

