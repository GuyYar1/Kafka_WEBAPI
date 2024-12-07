Here’s a concrete `README.md` file for your project, detailing the setup process, installation prerequisites, and use cases:

---

# Kafka Web API

## Overview

This project is a Web API built using ASP.NET Core and Kafka for message processing. It integrates Kafka producers and consumers, allowing you to send and receive messages between Kafka topics. The API allows interacting with Kafka through HTTP requests and is structured for scalability.

### Key Features:
- Integration with Kafka for producing and consuming messages.
- RESTful API endpoints using ASP.NET Core.
- Use of `Serilog` for logging Kafka interactions.
- Supports Swagger UI for API testing.

---

## Prerequisites

To run this project, you need the following:

### 1. Kafka (Precompiled Scala Version)
Kafka must be installed and configured for the application to work. The application connects to a running Kafka broker to produce and consume messages.

#### Steps to install Kafka:
1. **Download Kafka**: Download a precompiled Scala version of Kafka from the [official Kafka website](https://kafka.apache.org/downloads).
2. **Extract Kafka**: Extract the downloaded `.tgz` file to a folder of your choice.
   
### 2. Zookeeper Setup
Kafka depends on Zookeeper, which is a centralized service for maintaining configuration information and providing distributed synchronization.

#### Steps to install and run Zookeeper:
1. Inside the Kafka directory, find `zookeeper.properties` (under the `config` folder).
2. Start Zookeeper by running the following command from the Kafka directory:
   ```bash
   bin/zookeeper-server-start.sh config/zookeeper.properties
   ```
   This will start Zookeeper on the default port `2181`.

### 3. Running Kafka Broker (Kafka Cluster)
After setting up Zookeeper, the next step is to start the Kafka broker (Kafka cluster).

#### Steps to run the Kafka broker:
1. Start Kafka by running the following command from the Kafka directory:
   ```bash
   bin/kafka-server-start.sh config/server.properties
   ```
   Kafka will now be running and will be accessible from the default port `9092`.

### 4. Create a Kafka Topic
To interact with Kafka in this project, a specific topic needs to be created.

#### Steps to create the topic:
1. Open a new terminal window.
2. Run the following Kafka command to create a topic (e.g., `test-topic`):
   ```bash
   bin/kafka-topics.sh --create --topic test-topic --bootstrap-server localhost:9092 --partitions 1 --replication-factor 1
   ```
3. Verify the topic is created by listing all available topics:
   ```bash
   bin/kafka-topics.sh --list --bootstrap-server localhost:9092
   ```
   You should see `test-topic` listed.

---

## Project Setup

### Step 1: Clone the Repository

```bash
git clone https://your-repository-url-here
cd Kafka_WEBAPI
```

### Step 2: Install NuGet Packages

Make sure all dependencies are installed by restoring NuGet packages:

```bash
dotnet restore
```

### Step 3: Configure Kafka Settings

In the `appsettings.json` file, update the Kafka settings with the appropriate values for your Kafka instance:

```json
{
  "KafkaSettings": {
    "BootstrapServers": "localhost:9092",
    "ProducerTopic": "test-topic",
    "ConsumerTopic": "test-topic",
    "Keywords": ["example", "test"]
  }
}
```

Ensure that `ProducerTopic` and `ConsumerTopic` match the Kafka topics you’ve created.

### Step 4: Run the Application

You can now run the application using:

```bash
dotnet run
```

The application should start, and you can test it using Swagger UI.

---

## Known Issue

- **Issue**: After setting up Kafka and Zookeeper and running the project, there may be intermittent connection issues when the Kafka broker is not fully initialized before the API attempts to connect. Ensure that Kafka and Zookeeper are both running before starting the API.

---

## Testing the API with Swagger

Once the application is running, you can test it using Swagger UI:

### Access Swagger UI

Navigate to:

```
http://localhost:5000/swagger
```

Here are some use cases that you can try directly from Swagger:

### Use Case 1: Produce a Message

- **Description**: Sends a message to the Kafka producer topic.
- **Endpoint**: `POST /api/produce`
- **Request Body**:
  ```json
  {
    "message": "Hello Kafka"
  }
  ```

### Use Case 2: Consume Messages

- **Description**: Consumes a message from the Kafka consumer topic.
- **Endpoint**: `GET /api/consume`
- **Response**:
  ```json
  {
    "message": "Hello Kafka"
  }
  ```

### Use Case 3: Check Kafka Status

- **Description**: Check if Kafka and Zookeeper are properly running.
- **Endpoint**: `GET /api/status`
- **Response**:
  ```json
  {
    "status": "Kafka is running"
  }
  ```

### Use Case 4: List Kafka Topics

- **Description**: Lists all the Kafka topics currently available.
- **Endpoint**: `GET /api/topics`
- **Response**:
  ```json
  {
    "topics": ["test-topic"]
  }
  ```

---

## Project Blueprint

### 1. **KafkaProducerService**:
   - Sends messages to the Kafka producer topic.
   - Uses `ProducerBuilder` to configure and send messages to Kafka.

### 2. **KafkaConsumerService**:
   - Listens for and consumes messages from the Kafka consumer topic.
   - Uses `ConsumerBuilder` to configure the consumer and handle incoming messages.

### 3. **KafkaFacade**:
   - Acts as a mediator between the producer and consumer services.
   - Provides a simplified API for interacting with Kafka.

### 4. **Worker**:
   - A background service to continuously process Kafka messages or run tasks.
   
---

## Conclusion

This project provides a basic framework for integrating Kafka with a .NET Web API, enabling real-time message processing. The application demonstrates how to set up a Kafka producer and consumer, as well as provide a simple API interface to interact with Kafka topics. It can be expanded further to handle more complex message processing scenarios.
