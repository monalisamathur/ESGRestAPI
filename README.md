# ESGRestAPI
ESG Rest API for Create and Read

Visual Studio : 2019
MySql : 8.0.25


API URLs

1. To record customer details 
POST : http://localhost:47537/esgcustomer

The data from read from the CSV file is now being send to the restapi with a post method. This post method accepts the input in the form of json as : 
{"CustomerRef":16564,"CustomerName":"dsgdsg","AddressLine1":"Uk3","AddressLine2":"uk3","Town":"Oxford","County":"Oxfordshire","Country":"UK","Postcode":"OX4 3DF"}  , now this record goes through the HTTPpost method named
RecordCustomerDetails which firstly inserts the data in the table created in mssql by establishing a connection between the database.

Now once the data has been inserted in the table (custrec)it then passes through a function of validations. Like validation that the customerref to be an integer cannot be empty and cannot exceed a certain size. The similar checks are applied on all the fields of
record inserted. If it passes the validation the records are created in the table diplaying a httpcode as 201 and a msg. If it fails to pass the validation then httpcode 400 appears. 

There cannot be a duplicate customerref it has to be unique so if tried to insert the same record it will not proceed and show a error msg.

2. To fetch customer details
GET : http://localhost:47537/esgcustomer?customerref=12243

The method of httpget is GetCustomerDetails which takes customerref as a parameter. If the parameter is not an integerit throws a error msg. Now we call the database to retrive the customerdetails from the table. The function 
called is fetchRecord in the class CustomerDataInsert which again takes customerref as parameter, it then establishes a connection and fetch the data from the table. If the data not present throws the error simply.




