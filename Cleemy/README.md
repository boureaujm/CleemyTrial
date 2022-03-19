docker-compose.yml

+ sql server
******************************************************************

version: "3"
services:
  sql-server:
    image: mcr.microsoft.com/mssql/server:2019-latest
    hostname: sql-server
    container_name: sql-server
    ports:
      - "1433:1433"
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=XNc7PA5nxxW8ny

docker-compose up

+ initialize base
******************************************************************

Add-Migration -Name "Initial"

Create a ApplicationContextFactory for providing connection string

Update-Database

