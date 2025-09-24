# AppModernizers2 - E-commerce Microservices

This repository contains three microservices that have been modernized from a monolithic e-commerce application:

## Services Overview

### 1. User Service (Port 8083)
- **Purpose**: Manages user registration and authentication
- **Database**: PostgreSQL (userdb)
- **Endpoints**:
  - `GET /users` - Get all users
  - `POST /users` - Create a new user
  - `GET /actuator/health` - Health check

### 2. Product Service (Port 8082)
- **Purpose**: Manages product catalog
- **Database**: PostgreSQL (productdb)
- **Endpoints**:
  - `GET /products` - Get all products
  - `POST /products` - Create a new product
  - `GET /actuator/health` - Health check

### 3. Order Service (Port 8081)
- **Purpose**: Manages order processing
- **Database**: PostgreSQL (orderdb)
- **Endpoints**:
  - `GET /orders` - Get all orders
  - `POST /orders` - Create a new order
  - `GET /actuator/health` - Health check

## Prerequisites

- Java 17 or higher
- Maven 3.6 or higher
- Docker and Docker Compose (for containerized deployment)
- PostgreSQL (for standalone deployment)

## Quick Start

### Option 1: Docker Compose (Recommended)

1. **Build and start all services:**
```bash
docker-compose up --build
```

2. **Validate services:**
```bash
./validate-services.sh
```

### Option 2: Standalone Deployment

1. **Set up PostgreSQL databases:**
```sql
CREATE DATABASE userdb;
CREATE DATABASE productdb;
CREATE DATABASE orderdb;
```

2. **Build all services:**
```bash
# User Service
cd User-service
mvn clean package
java -jar target/user-service-0.0.1-SNAPSHOT.jar

# Product Service (in new terminal)
cd Product-service
mvn clean package
java -jar target/product-service-0.0.1-SNAPSHOT.jar

# Order Service (in new terminal)
cd Order-service
mvn clean package
java -jar target/order-service-0.0.1-SNAPSHOT.jar
```

## API Usage Examples

### Create a User
```bash
curl -X POST http://localhost:8083/users \
  -H "Content-Type: application/json" \
  -d '{"username":"john_doe","email":"john@example.com","password":"password123"}'
```

### Create a Product
```bash
curl -X POST http://localhost:8082/products \
  -H "Content-Type: application/json" \
  -d '{"name":"Laptop","price":999.99}'
```

### Create an Order
```bash
curl -X POST http://localhost:8081/orders \
  -H "Content-Type: application/json" \
  -d '{"userId":1,"productId":1,"quantity":2,"totalPrice":1999.98}'
```

### Health Checks
```bash
curl http://localhost:8083/actuator/health  # User Service
curl http://localhost:8082/actuator/health  # Product Service
curl http://localhost:8081/actuator/health  # Order Service
```

## Architecture

The services follow a microservices architecture with:
- **Separate databases** for each service
- **REST API** communication
- **Spring Boot** framework
- **JPA/Hibernate** for data persistence
- **Docker** containerization
- **Health monitoring** via Spring Actuator

## Database Schema

### User Service
```sql
CREATE TABLE "user" (
    id BIGSERIAL PRIMARY KEY,
    username VARCHAR(255),
    email VARCHAR(255),
    password VARCHAR(255)
);
```

### Product Service
```sql
CREATE TABLE product (
    id BIGSERIAL PRIMARY KEY,
    name VARCHAR(255),
    price DECIMAL(10,2)
);
```

### Order Service
```sql
CREATE TABLE orders (
    id BIGSERIAL PRIMARY KEY,
    user_id BIGINT,
    product_id BIGINT,
    quantity INTEGER,
    total_price DECIMAL(10,2)
);
```

## Testing

Each service includes integration tests:

```bash
# Run tests for all services
cd User-service && mvn test
cd Product-service && mvn test
cd Order-service && mvn test
```

## Configuration

Services can be configured via environment variables:

- `SPRING_DATASOURCE_URL` - Database connection URL
- `SPRING_DATASOURCE_USERNAME` - Database username
- `SPRING_DATASOURCE_PASSWORD` - Database password

## Monitoring

All services expose metrics and health information via Spring Boot Actuator:
- Health: `/actuator/health`
- Info: `/actuator/info`
- Metrics: `/actuator/prometheus`

## Validation Results

✅ **All services are fully functional:**
- Compilation successful
- Unit tests passing
- Integration tests passing
- REST APIs working
- Database connectivity verified
- Docker containers operational
- Cross-service communication validated
