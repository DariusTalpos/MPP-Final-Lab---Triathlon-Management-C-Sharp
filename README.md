# MPP Final Lab - Triathlon-Management (C# Version)

Java version:

EN: This application is the final version of the project received at the "Projecting and Programming Methods" course at Babeș-Bolyai University.
It is a client-server application that stores data using a PostgresSQL database, and has multiple functionalities:
1. Log In - by starting the client side of the application, and having the server running beforehand, the app user can log in into the application by using a valid username and the associated password in the log in screen.
2. Viewwing participants, rounds, results - the main menu of the application presents the user a list of participants (names and points) and a list of rounds (names). By choosing a round, the user can view the paricipants of that round and the amount of points they obtained.
3. Adding new rounds and results - by pressing the "Add New Round/Score Data" button, the user is sent to a new window where they can insert a round name, an amount of points and select a participant. If they choose to submit this score, the data will be saved (if the round name is not in the database, a new round with this name will be created). The user can also choose to cancel this action by pressing the "Cancel button".
4. Log Out - by pressing the "Log out" button in the main menu, the user will log out of the application.

This application was built using the C# programming language, using several .NET libraries. It is also compatible with the Java version of the application, allowing the C# client to connect to the Java server using Protobuf. It can also use the REST services written in the Java application (if the Java REST server is running).
