## Getting Started
>
> 1. Clone Repository:  
`git clone https://github.com/khaizon/CarparkWebAPI.git`
> 2. Change MySQL connection settings in /CarparkAPIApp.csproj:  
`{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost; userid=root; port=3306; database=mysqlcarpark; sslmode=required; password=1234567890"
  }  
  }
`
> 3. `cd CarparkWebAPI`
> 4. `dotnet run`
> 5. Open API guide in browser:  
`https://localhost:5001/swagger/index.html` 

## Authentication API
> 1. Register Account  
`HttpPost: https://localhost:5001/api/authmanagement/register`  
Body:  
`{email: [email],
firstName: [firstname],
lastName: [lastname],
password: [password],
contactNumber [contactNumber]}`
> 2. Login  
`HttpPost: https://localhost:5001/api/authmanagement/login`  
Body:  
`{email: [email],
password: [password]}`
> 3. Get User Details:  
`HttpGet: https://localhost:5001/api/authmanagement/getDetails`  
Add "Authorization Header":  
`Key: "Authorization", Value: "Bearer [jwtToken]"`

## Carpark Availability API

> 1. Get User Details:  
`HttpGet: https://localhost:5001/api/carpark`
Add "Authorization Header":  
`Key: "Authorization", Value: "Bearer [jwtToken]"`
