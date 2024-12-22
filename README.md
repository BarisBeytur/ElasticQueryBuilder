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

This project consists of two applications:
1. **.NET Core Web API**: Provides the backend API for querying Elasticsearch.
2. **.NET Core MVC UI**: A front-end interface for interacting with the Elasticsearch query builder.

Both applications must be running simultaneously for the project to function correctly.


1. **Clone the repository:**
   ```bash
   git clone https://github.com/BarisBeytur/ElasticQueryBuilder.git
   cd ElasticQueryBuilder

2. Restore dependencies for both applications:

   ```bash
   cd ElasticQueryBuilder
   dotnet restore

   
   cd ElasticQueryBuilder.Client
   dotnet restore
   
4. Create the appsettings.json or environment variables with your Elasticsearch credentials. (optional)

5. Build and run the application:

   ```bash
   cd ElasticQueryBuilder
   dotnet run

   cd ElasticQueryBuilder.Client
   dotnet run
   
**Error Handling**
   - The API returns appropriate HTTP status codes (400, 401, 500) along with error messages to help with debugging:
   
   - 400 Bad Request: Missing or invalid input.
   - 401 Unauthorized: Authentication required or failed.
   - 500 Internal Server Error: Unexpected server-side issues.

**Contributing**
   - Feel free to fork the repository, create a feature branch, and submit a pull request. Contributions are welcome!

**License**
   - This project is licensed under the MIT License. See the LICENSE file for more details.

**Contact**
   - For any questions or support, please reach out to [beyturbaris@gmail.com].





![image](https://github.com/user-attachments/assets/d90c4717-4a6a-4a2b-90b0-f48cc9bb9277)


![image](https://github.com/user-attachments/assets/64eae301-f064-442d-a14a-5da17d6f75bf)



   

