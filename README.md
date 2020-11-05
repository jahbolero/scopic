# Auction App
This project is an auction app built as a test task technical assessment.
# Prerequisite
* This project uses .net core 3.1.7 Angular 10.2, and Node 12.7.0 .

* **Node.js** (https://nodejs.org/en/download/)

* **.Net Core** https://dotnet.microsoft.com/download. Download and install both SDK and Runtime.

* **Angular**, run the following command `npm install -g @angular/cli`.

# Getting started

## Api
1.`$cd scopic-test-server`

2.`$dotnet restore`

3.Change the AwsAccessKey and AwsSecretKey inside the appsettings.json file with the credentials found  <a  href="https://docs.google.com/document/d/1DCgXtcNs6A3WdPw6M62KuHh7NlIHvUjqIDisu-O4z1s/edit" target="_blank">here</a>

4.`$dotnet run`
*Note: Please make sure port 5000 is not in use.*
  
## Client
1.`$cd scopic-test`

2.`$npm install`

3.`$ng serve`

4.Access client at `localhost:4200`
  

## Credentials
There are 2 user types, *User* and *Admin*.

**In order to test the email notification feature, please register using a valid email address.** 

`(User)username:user@email.com|password:user`

`(User)username:user1@email.com|password:user1`

`(User)username:user2@email.com|password:user2`

`(Admin)username:admin|password:admin`

##Documentation
Postman documentation:https://documenter.getpostman.com/view/8117716/TVYNZbD6
