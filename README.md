# Rest API Test Suite – Amused Group Coding Assignment

# Project Overview
This project is an automated test suite created to test the https://restful-api.dev API using C# and the xUnit framework, as requested in the assignment PDF. It follows the 5 required test scenarios outlined in the instructions and ensures the API behaves correctly across its core features: create, read, update, and delete.

# What is being measured by the project?

# 1. Get All Objects
The first test sends a GET request to the /objects endpoint.
It checks whether the server successfully responds and returns a list of all objects available in the system.
I verify this by:
Making sure the response was successful (HTTP 200).
Checking that the response body contains data, like a list or collection of objects.

# 2. Create an Object (POST Request)
In this test, I am testing the creation of a new object using a POST request.Send JSON data with a name and some additional fields (year and price).After the API processes the request, it should return a success response (200 OK) and include:
A newly generated object ID, which store in a variable for later tests.
A confirmation that the name we sent (Apple MacBook Pro 16) is in the response.
This test proves that the API can handle object creation correctly.

# 3. Get Object by ID
Now that I have created an object, which want to make sure we can retrieve it using its unique ID.

Send a GET request to /objects/{id} and the test checks that the API returns a successful response and includes the correct object name.This step helps confirm that the object was really created and is stored correctly by the server.

# 4. Update the Object
Next, I test the update functionality using a PUT request. Sending updated information (a new name and price) to the same object using its ID. The server should return a success response, and the response body should now reflect:
The updated name (Apple iPhone Pro 16)
The updated price (1850.99)
This confirms that the API allows updates to existing data and stores the changes correctly.

# 5. Delete the Object
Finally, I test if the object can be removed. Sending a DELETE request to the object's URL. After deletion, Sending another GET request to confirm the object is no longer available. The API should respond with a 404 Not Found, which means the object was deleted successfully.

To keep things connected between these tests (create → read → update → delete), we save the ID of the object that was created. This ID (_createdObjectId) is used in later tests to refer to the same object. If the ID isn’t available, the tests will create the object again before continuing. This approach ensures all tests can run independently without manual setup.

# Technologies Used

C# (.NET 8) – The primary programming language.
xUnit – The test framework for writing and running unit tests
HttpClient – To make HTTP requests to the REST API
System.Text.Json – For parsing and generating JSON payloads
Visual Studio – For development purposes

# How to Run the Tests

# Prerequisites
Before running the tests, make sure, have the right tools installed:
1. .NET SDK (version 6.0 or later)
This is the software development kit needed to build and run C# projects.
2. A Code Editor or IDE
I used: Visual Studio (Community Edition is free)

# Steps to Run the Tests: 
1. Clone the Repository from GitHub
   If the project is hosted on GitHub, you’ll want to get a copy onto your local machine.
   Open a terminal (or Command Prompt), and run:
       git clone https://github.com/your-username/rest-api-assignment.git
       cd rest-api-assignment

2. Restore the Project Dependencies
   .NET projects often rely on packages (like libraries) defined in the .csproj file. To download and install them, run:
        dotnet restore
   This makes sure your project has everything it needs to build and run.

3. Run the Tests
   Build the solution and Run Execute below command line: dotnet test

# Expected Output

After a successful test run, you’ll see output similar to:
Passed: 0
Failed: 0

# Actual Results

1. Get list of all objects
![Image](https://github.com/user-attachments/assets/e37dbc20-2f9c-4a73-80bd-5cb07dd45da5)
2. Add an object using POST
![Image](https://github.com/user-attachments/assets/9fc6612a-709b-488f-93af-8c527ef76a70)
3. Get a single object using the above added ID
![Image](https://github.com/user-attachments/assets/6f53f573-3391-4508-b381-74ce997f3c5e)
4. Update the object added in Step 2
![Image](https://github.com/user-attachments/assets/89e5e56c-7fe9-45f4-be63-55a1c13b0e78)
5. Delete the object using DELETE. 
![Image](https://github.com/user-attachments/assets/ba2d9bb6-f614-451e-a802-f6ef95177a53)
