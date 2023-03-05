# Teams Management
Teams Management system provide to manage your teams and players. The project needs PostgreSQL, .Net 7.0. But don't worry about installment steps, there is no necessary to install all parts of these. This project needs only one requirement which is [Docker Desktop](https://www.docker.com/products/docker-desktop).

## Tech
- .Net 7.0
- PostgreSQL 
- Docker


## Build with Docker
[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)
- First of all we need to go `docker-compose.yml` 's path on our console.

     ![image](https://user-images.githubusercontent.com/38660944/222956785-e0971abd-d298-433c-a2e5-797269f1601c.png)
     
- Making sure we are on the right path, we can start build and install parts, to do that we just write below command.
  ```sh
  docker-compose up --build -d 
  ```
- This build time can take some time to install some images as I said, just wait until see below picture.

     ![image](https://user-images.githubusercontent.com/38660944/222951948-a583febf-6b0f-46db-a988-deb7a6252ab0.png)

- This step provides everything what we need for create teams management system. When containers go up, some seed datas will be added simultaneously like teams and players.


## About
- [PostgreSQL DB](http://localhost:56002) is running on   `localhost:56002`
- [PgAdmin for Postgre](http://localhost:56003) is running on `localhost:56003` with `username:admin@teamsmanagement` `password:admin`
- [TeamsManagement](http://localhost:56101/swagger) is running on `localhost:56101`

This configurations managed by 'docker-compose.yml' file. In addition to this PgAdmin provide to show PostgreSQL Database.


## How to see DB in PgAdmin
  - Firstly click the [PgAdmin for Postgre](http://localhost:56003) then you will see below page just write `username:admin@teamsmanagement` `password:admin`.

    ![image](https://user-images.githubusercontent.com/38660944/222953058-7eacd0d9-d309-449a-b139-baf2255886ff.png)

  - Secondly step is click to right mouse button on Servers tab.
  
    ![image](https://user-images.githubusercontent.com/38660944/149299381-3a42ea46-b30c-4072-9470-21e8df84f6b5.png)
    
  - Finally write something on Name in General Tab after that go to Connection Tab and please fill with all datas as you can see below. Password is `admin`.
  
    ![image](https://user-images.githubusercontent.com/38660944/222953123-06628eb4-23d9-46f0-8447-017d67faaddc.png)
    
After all step you will see the TeamsManagement Database on the left and you will find the tables from Schemas/TeamsManagement/Tables.

## Test
### Postman
I shared a Postman Collection as you see below. You can download this file and import to Postman.

[Postman-Collection](https://drive.google.com/file/d/13dLXLoyXO4aW9HoYVfOICbxGUsg8oNL8/view?usp=sharing)

![image](https://user-images.githubusercontent.com/38660944/222953515-b7994694-891d-4241-afa9-7d566aab691f.png)

### Swagger
  ### Team Endpoints
  
  ![image](https://user-images.githubusercontent.com/38660944/222955781-5272f82a-92f6-4ee8-97f5-4849cf798b88.png)
  - This endpoint returns all teams.
---------------------
  ![image](https://user-images.githubusercontent.com/38660944/222955818-f10239e2-dfeb-4d82-9aeb-485ece50edbb.png)
  - This endpoint needs TeamId to return all players in a team.
---------------------

  ### Player Endpoints
  
  ![image](https://user-images.githubusercontent.com/38660944/222955872-826d3a61-b5b0-4a74-85d6-7d50c79bcb67.png)
  - This endpoint returns all players include teamlesses players.
---------------------
  ![image](https://user-images.githubusercontent.com/38660944/222955930-4eb86dbe-951c-4c10-8dcc-a180df8c1060.png)
  - This endpoint needs PlayerId to returns all details about single player.
---------------------
  ![image](https://user-images.githubusercontent.com/38660944/222956004-410e5f46-75d3-46cf-892d-8415e78778d8.png)
  - This endpoint creates a new player and if you send teamId in body request, the player will join to the team.
---------------------
  ![image](https://user-images.githubusercontent.com/38660944/222956093-545ea659-1039-4822-baff-db22f3576b68.png)
  - This endpoint need PlayerId to updates some details(name, date of birth, height) for single player.
---------------------
  ![image](https://user-images.githubusercontent.com/38660944/222956203-7924f397-48dc-445c-a143-1c5183d75b2c.png)
  - This endpoint need PlayerId to updates which team to join. If you will not send teamId in body request, the player will be teamless.
---------------------

## Clear
  When all tests are done, you can use this command below to down everything.
  ```sh
  docker-compose up --build -d 
  ```
  ![image](https://user-images.githubusercontent.com/38660944/222957932-68aae795-6f5c-4ad7-82af-7fa511ac0310.png)
