# RabbitMQExample
A simple example of how to publish and consume events with RabbitMQ.

The purpose of this example is to show develops how to get started working with RabbitMQ and also to show the importance of queueing long-running tasks for processing later.

This example project is made up of 3 components:
1. WebUI: A website which allows a user to make a booking for an event. When a booking is made then it is added to a RabbitMQ queue.
2. ReceiptGenerator: This is a console app that monitors a RabbitMQ queue and generates an email and sends it.
3. Docker/RabbitMQ: A RabbitMQ server runs inside a Docker container along with the other projects.

Requirements
============
- Visual Studio 2022
- Must have Docker Desktop installed

Instructions
============
- Clone the repo
- Make sure Docker Desktop is running
- Open the solution file in Visual Studio
- Make the project called "docker-desktop" the startup project
- Run the solution. A browser window should open and show the booking form.
- You may need to run the solution more than once because it takes time to download the Docker images the first time.
- You may also need to change the "docker-compose" project debugging profile in the launch settings to get it to run.
