## Objectives

After you complete this lab, you will be able to:
-   Use azure service for hosting Azure Web aplication.
-   Communicate with ASP.NET core web API

## Instructions
- Log in into Azure portal. 
- Go to Storage accounts at home page and press add new storage account
- Basics: Name your storage account, select resource group for Code9, select region as it is selected when API and Database were created, for redundancy select Locally-redundant
- Data Protection: deselect everything
- Everything else leave as it is and create storage account.
- After storage account is created succesfully go to storage account
- Go to static website, enable it and for 'index document name' and 'error document path' put 'index.html' and press save
- After pressing save primary endpoint for your static website will be visible, copy that url
- Clone repository from your branch on github to your local machine
and open Angular Web App->survey-webapp in Visual studio code
- In Visual Studio Code open new terminal and run 'npm install'
- If you havent installed Anuglar cli, run in terminal command 'npm i @angular/cli'
- After that in Enviroment.ts and Enviroment.prod.ts file for 
 apiUrl variable put url of your API on Azure
- After that run command 'ng build --prod'
- After build is passed you will have dist folder now in your survey-webapp 
- Go back to storage account and open containers->$web
- Click on upload button, open dist folder and upload everything from it
- Now your website is on storage account and can be accessed by your primary endpoint but you need to whitelist your primary endpoint on API
- Go to your API service resource on Azure and select CORS tab
- In allowed origins add your primary endpoint of your storage account and also add your 'http://localhost:4200' endpoint if you want to make requests from your local instance of webapp to the API on Azure

## Homework
- Create page adding general survey informations and general questions and answers
- Adjusting functionality for saving survey result with participant data
- Create page for showing results of survey
- Endpoint for that is available on Web API
- Send to us link to the Web app (we will use for testing purpose)
