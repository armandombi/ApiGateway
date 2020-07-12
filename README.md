# Checkout.com API Gateway

This is a code test for Checkout.com

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

To run this code on your machine you will need to have the following installed:
- Latest version of Docker (Currently in version 2.3.0.3), which can be downloaded [here](https://docs.docker.com/get-docker/)
- .Net Core 3.1 which can be downloaded [here](https://dotnet.microsoft.com/download)
- A web browser such as Chrome, Edge, Firefox, etc.

### Installing

#### Clone

- Clone this repo to your local machine using:
```shell
git clone https://github.com/armandombi/ApiGateway.git
```
#### Setup

- Once you have cloned the repository change into the projects folder and run docker-compose up:

- ![Compose](http://g.recordit.co/oVPXPzyjzR.gif)

   This will start the process of downloading all the necessary images, building the dependencies and starting the API so wait until this process has finished

- Open a new browser tab and navigate to http://localhost:5000/index.html and the swagger API page should be displayed
- In order to start making requests from swagger you will need to do some fake authentication by adding the Authentication Header with the Bearer keyword inside as shown:

- ![Auth](http://g.recordit.co/hOoEaJlIQe.gif)

- There is no token validation for simplicity
- Now you can start testing the API and making requests

* You can also choose to run the project by opening the solution in Visual Studio 2019 and running the Docker compose from within

## Features

- A Payment API that allows the processing and retrieval of payments
- A Bank Simulation API that allows to mock bank payment processing
- Implementation of Swagger in main API to provide user documentation and a client to test the API
- Containerization using docker-compose to provide a simple way to execute and test the API
- Using [Clean Architecture](https://pusher.com/tutorials/clean-architecture-introduction) 
- Authorization to provide security to the API
- Added Log capabilities throughout the application that can be easily expanded and extended using different Serilog sinks

## Running the tests

- To run the test you will use .Net Core 3.1 so please verify this is working in your system by typing dotnet in any cmd window and this should be a recognized command
- From the main application folder (C:\Source\ApiGateway in my case) we change into the test project folder "ApiGateway.Tests"
- Execute "dotnet build" to build the test project
- Execute "dotnet test" to run all the tests

* You can also choose to run the tests from the project by opening the solution in Visual Studio 2019 and running the tests on the test explorer

## Deployment

To deploy this to a live system we can add yml file to provide instructions to build the API and all dependencies and deploy them using tools like Azure Devops, Bitbucket Pipelines, Jenkins, among others. 

## Limitations

* Only Dummy Authorization was implemented no validation of token or further checks are made for simplicity
* Only a small amount of tests was included in the project
* The system has only been tested in a Windows environment running in docker with Linux containers

## Things to Improve

- Adding further test to the API
- Adding the test suite to the docker-compose to only build the solution if all test are successful
- Add metrics capabilities by using Application insights, Raygun or any other metrics tool used by the company (This can be done with Serilog sinks)
- Adding Nginx and letsencrypt to provide SSL capabilities
- Implement Event Sourcing to improve message tracing, reliability and scalability capabilities
- Add an API Gateway such as Ocelot to better handle requests in a centralized way, load balancing, attack protection among other features

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
