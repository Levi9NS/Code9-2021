
## Objectives

After you complete this lab, you will be able to:
-   Creating Cosmos database on Azure
-   Create simple database model

## Instructions
- Log in into Azure portal 
- Choose create resource
- Find Cosmos Database and add new Cosmos DB Account
- Create new resource group (if doesn't exists) code9group[YourName]
- Specify Account Name as code9account[YourName]
- Leave rest of parameters on default value
- Go to next: Global distribution
- Leave default values
- Go to next: Networking
- Leave default values. We want to access to DB from all over the place.
- Go next to Back up policy. Leave default values.
- Go to encryption. Leave default values.
- Go to tags. Leave default values.
- To to Review and Create. Press Create.
- Wait for database creation
- Go to resource
- Data explorer
- Create new container
- Specify databseId as Customers[YourName]
- Specify database throughput to be manual, because of cost
- Specify container to be code9[YourName]
- PartitionyKey = City
- Press OK
- Open code9[YourName] and go to items. Create new item on by one using:

{
  "customerid": 1,
  "customername": "John",
  "city": "Miami"
}

{
  "customerid": 2,
  "customername": "Jane",
  "city": "New York"
}

{
  "customerid": 3,
  "customername": "Michael",
  "city": "Florda"
}

{
  "customerid": 4,
  "customername": "David",
  "city": "New York"
}

- Go to new query. Execute it.
- In json response there should be four items.

Examples:
-- Embedding data

{ 
    "customerid": 1, 
    "customername": "John", 
    "city": "Miami",
    "orders" : [
    {
        "orderId": 1000,
        "productId": 100,
        "quantity": 2
    }]
}

--Referencing data

{
  "customerid": 1,
  "customername": "John",
  "city": "Miami",
  "orders":[1000,1002]
}

{
    "orderId": 1000,
    "productId": 100,
    "quantity": 2
}

{
    "orderId": 1002,
    "productId": 101,
    "quantity": 4
}
