## Objectives

After you complete this lab, you will be able to:
-   Use azure service for hosting Azure Web aplication.
-   Communicate with ASP.NET core web API

## Instructions
- Log in into Azure portal. 
- For creation SQL server database model, please use exercise from Azure Database (SQL and Cosmos)\SQL folder.
- Fore creation .NET core web API, please use exercise from .NET Core Web API including SOLID and Design Patterns
- Choose create resource.
- Find App Services.
- Create App Service.
- Use existing or create new group Gode9 Group [Your Name]..
- Specify name SurveyWEB[YourName].
- Runtime stack: .NET Core 3.1 (LTS).
- New App Service plan: ManagedPlan.
- Sku and size. Go to change size. Use DevTest/F1/Free.
- Next. Deployment and preview not change anything.
- Next. Enable monitoring put to disable.
- Next to the end and create web app.
- Open SuerveyWEB solution from Visual Studio.
- Right click on project and than publish.
- Choose Azure and next.
- Choose Azure App Service (Windows).
- Sign In to your Azure Account.
- From the list choose SuveryWEB[YourName] application service.
- Choose Publish (generates pubxml file).
- Again press publish.
- Go to your application service and from overview tab copy url and check if application is deployed.

## Homework
- Create page for showing results of survey
- Endpoint for that is available on Web API
- Send to us link to the Web app (we will use for testing purpose)
