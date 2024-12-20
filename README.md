# ElasticQueryBuilder

ElasticQueryBuilder is a .NET-based application that provides an interface for querying Elasticsearch indexes. It offers features like login authentication, retrieving index fields, and building custom queries.

## Features
- **Authentication**: Authenticate with your Elasticsearch instance using username, password, and SSL fingerprint.
- **Retrieve Fields**: Fetch available fields from a specified Elasticsearch index.
- **Build Queries**: Construct and execute custom queries with specified filters.

---

## Technologies Used
- **C#** (ASP.NET Core)
- **Elasticsearch** (Elastic.Clients.Elasticsearch)
- **Dependency Injection**
- **REST API**

---

## Prerequisites
- **.NET 6.0** or later
- **Elasticsearch 7.x/8.x**
- A configured Elasticsearch cluster with valid credentials.

---

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/ElasticQueryBuilder.git
   cd ElasticQueryBuilder

2. Restore dependencies:
   dotnet restore

3. Update the appsettings.json or environment variables with your Elasticsearch credentials.

4. Build and run the application:
dotnet run


## API Endpoints

**Login to Elasticsearch**
  Endpoint: /api/Query/LoginElastic
  Method: POST
  Request Body:
    {
      "username": "your-username",
      "password": "your-password",
      "fingerPrint": "your-ssl-fingerprint",
      "url": "https://your-elastic-url"
    }
  Response:
      200 OK: Login successful.
      401 Unauthorized: Invalid credentials.
      500 Internal Server Error: Connection issues.

**Get Index Fields**
Endpoint: /api/Query/fields
Method: GET
Query Parameter:
      index: The name of the index to retrieve fields from.
Response:
      200 OK: Returns a list of fields.
      400 Bad Request: Missing index name.
      500 Internal Server Error: Retrieval issues.


   

