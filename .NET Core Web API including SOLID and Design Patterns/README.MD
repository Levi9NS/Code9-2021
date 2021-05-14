## Objectives

After you complete this lab, you will be able to:
-   Use azure service for hosting .NET Core Web API.
-   Create services SOLID principles and Design Patterns.

## Instructions
- Log in into Azure portal. 
- For creation SQL server database model, please use exercise from Azure Database (SQL and Cosmos)\SQL folder.
- Choose create resource.
- Find App Services.
- Create App Service.
- Use existing or create new group Code9 Group [Your Name].
- Specify name SurveyAPI[YourName].
- Runtime stack: .NET Core 3.1 (LTS).
- New App Service plan: ManagedPlan.
- Sku and size. Go to change size. Use DevTest/F1/Free.
- Next. Deployment and preview not change anything.
- Next. Enable monitoring put to disable.
- Next to the end and create web app.
- Take GitHub branch of Levi9NS/Code9-2021 repo. This will be created by presenter.
- One SuerveyAPI solution from Visual Studio.
- Right click on project and than publish.
- Choose Azure and next.
- Choose Azure App Service (Windows).
- Sign In to your Azure Account.
- From the list choose SuveryAPI[YourName] application service.
- Open SQL server database in Azure. Copy connection string. Paste it into AppSettings.json. Don't forget to rewrite password.
- Choose Publish (generates pubxml file).
- Again press publish.
- Go to your application service and from overview tab copy url and check if application is deployed.

## Homework
- Create API for storing and getting Survery General information, Questions and OfferedAnswers
- Send to us Web API URL (We will use this for testing)
- Send to us SQL server name and credentials (We will use this for testing)
