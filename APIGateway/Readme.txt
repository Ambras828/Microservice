#  Microservices Project

This is a microservices-based project built using **.NET Core 7.0**, focusing on clean and layered architecture, best practices, and real-world patterns including **Factory Pattern**, **CQRS**, **JWT Authentication**,**GlobalExceptionHandling using Middleware", and **API Gateway**.

##  Services

### Authentication Service
- Handles user login and JWT token generation.
- **Endpoint:** `POST /api/auth/login`
- **Tech:** .NET 7, JWT
- **Security:** Swagger disabled (Only accessible via API Gateway)

---

### Category Service
- CRUD operations for categories.
- Includes factory pattern to handle entity creation.
- **Tech Stack:**
  - .NET Core 7.0
  - Layered Architecture (Used Factory pattern)
  - Dapper with Stored Procedures
  - JWT Authentication
- **Entity Fields:**
  - `Id` (GUID)
  - `CategoryName`
     `CategoryType`
  - `Orders`
- **Swagger:** Disabled

---

###  Products Service
- Full CRUD operations with CQRS implementation.
- Image support (JPEG only).
- **Tech Stack:**
  - .NET Core 7.0
  - CQRS Pattern
  - Dapper with Stored Procedures
  - JWT Authentication
- **Entity Fields:**
  - `Id`, `Name`, `Category`, `Manufacturer`, `Quantity`, `Price`, `Image`
- **Swagger:** Disabled

---

###  Company Service
- Manages company data using Factory pattern.
- **Tech Stack:**
  - .NET Core 7.0
  - Clean Architecture (used Factory Pattern for getting DbConnection)	
  - Dapper with Stored Procedures
  - JWT Authentication
- **Entity Fields:**
  - `Id`, `Name`, `StreetAddress`, `City`, `State`, `PostalAddress`, `Zip`, `ContactNumber`
- **Example Companies:** Samsung, Sony, Apple
- **Swagger:** Disabled

---

##  Communication
- **API Gateway** using **Ocelot** for routing requests.
- All services are accessible only via the gateway.
- Each microservice uses a middleware to **allow only API Gateway requests**.

---

##  Security
- JWT Authentication implemented across all services.
- Swagger is disabled in all services to prevent direct API access.
- Middleware verifies requests originate from the API Gateway.

---

##  Testing
Use **Postman** to authenticate and test services via API Gateway:

1. **Login:**
   - `http://localhost:5000/api/auth/login`
   - Body:
     ```json
     {
       "username": "admin",
       "password": "admin123"
     }
     ```
   - Response will include a **JWT token**

2. **Authorized Request Example (e.g., Get Category by ID):**
   - Add token to **Authorization** header as: `Bearer <token>`
   - Call: `GET http://localhost:5000/api/category/C95B289B-C674-47D7-BECD-09DC7395633B`

---

##  How to Run
Clone the project
Clone this repository to your local machine.

Update the connection string
In each microservice's appsettings.json, update the connection string as shown below to match your environment:
"ConnectionStrings": {
  "DefaultConnection": "Server=PIC4393;Database=Microservice;Trusted_Connection=True;TrustServerCertificate=True"
}

Configure startup projects
In Visual Studio, set all microservices and the API Gateway to start together by configuring them as multiple startup projects.

Restore and configure the database
Make sure the database (Microservice) is restored and accessible by all services.

Use Postman for testing
Use Postman to test the APIs via the API Gateway endpoints.