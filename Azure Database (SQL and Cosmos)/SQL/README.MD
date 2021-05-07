## Objectives

After you complete this lab, you will be able to:

-   Creating SQL database on Azure.

-   Create database model for survey application.

## Instructions

- Log in into Azure portal 
- Choose create resource
- Find SQL Database
- On basic settings create new resource gorup with name Gode9Group [YourName] by using create new (example Code9Ruzica).
- Specify database name Gode9Database [YourName]. This value should be unique.
- Specify server name code9server[YourName] by using CreateNew. Specify server adming login and password.
- Want to use elastic pool leave to default no.
- Compute and storage: go to configure database. Use basic because of cost (4.99 USD per months)
- Go to next: Networking
- Enable public endpoint
- Allow Azure services and resources to access this server: Yes
- Add current client IP address: Yes
- Next for security. Just continue to Additional Settings
- Next go to tags and create and review. Press create.
- Wait some moment for database to be created.
- Press GoToResource
- Go to overview and copy Server name parameter.
- Open sql server management studio. 
- Connect to database using Server name, username and password.
- Execute SQL script DatabaseModel
- Execute SQL script InsertData
- Create database diagram by adding all available tables

