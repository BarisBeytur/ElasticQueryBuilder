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
   git clone https://github.com/BarisBeytur/ElasticQueryBuilder.git
   cd ElasticQueryBuilder

2. Restore dependencies:
   dotnet restore

3. Create the appsettings.json or environment variables with your Elasticsearch credentials.

4. Build and run the application:
dotnet run


## API Endpoints

**Login to Elasticsearch**
  - Endpoint: /api/Query/LoginElastic
  - Method: POST
  - Request Body:
    ```bash
    {
      "username": "your-username",
      "password": "your-password",
      "fingerPrint": "your-ssl-fingerprint",
      "url": "https://your-elastic-url"
    }


**Get Index Fields**
   - Endpoint: /api/Query/fields
   - Method: GET
   - Query Parameter:
         index: The name of the index to retrieve fields from.

**Build Query**
   - Endpoint: /api/Query/build-query
   - Method: POST
   - Request Body:
     ```bash
      {
        "index": "index-name",
        "filters": {
          "fieldName1": "value1",
          "fieldName2": "value2"
        }
      }


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


![Adsız tasarım](https://github.com/user-attachments/assets/903adc98-307c-44ac-ac0c-e952761e94b2)

   

