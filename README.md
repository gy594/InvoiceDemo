# InvoiceDemo
As Loading too much data from the invoice web API to analyse data and output the records, my idea is to separate the operations to two major parts:
1.	Azure Function App to do the daily pull data from the 3party API with small batch of data daily range. Then store data to database. The Function APP Timer trigger daily e.g. 11pm only query one day data and store to DB.
2.	Existing web app creating a page to view the data. For accounts records would be paginated to small calls to DB to view the records with calculated result. (the improvement should be adding a flag to only calculate once in the beginning or compare the timestamp to avoid re-calculate. Only trigger the calculation on demand. Or, add some cache strategy to cache the results.) currently, I save the calculated result but did not put a flag but not time today to do it. For the Invoice records it would display up to 10 records only for each account, but it is not pre-loaded. It would load the record on demand (clicking the link).
Usage:
1.	Pull the code from GitHub.
2.	Update the connection string in appsetting.json in “InvoiceDemo” project.
3.	If you would like to use the Azure function app 
a.	Update the local setting to json file 
b.	Or deploy to Azure and update the Azure portal configuration
This demo design as a typical structure
1.	Domain 
2.	Repository
3.	Service
4.	Web app (using Razor page just for easy in-built from MS)
5.	Azure function App 
