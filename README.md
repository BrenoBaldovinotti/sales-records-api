# Developer Evaluation Project

---

## Project Overview

This repository contains the implementation for managing sales records with functionalities like creating, updating, deleting, and retrieving sales.

For detailed instructions about the challenge requirements and initial setup, refer to [INSTRUCTIONS.md](INSTRUCTIONS.md).

---

## Running the Project

Follow these steps to set up and run the project locally:

### Pre-requisites

1. **Install Docker for Windows**
   - Ensure Docker is installed and running on your machine.

2. **Install PostgreSQL Locally**
   - If not already installed, install PostgreSQL. 
   - **Note**: The Docker setup in this project already runs a PostgreSQL container on port `5432`. Configure your local PostgreSQL to use port `5433` in the `appsettings.json` file and on `pgAdmin`.

3. **Create the Database**
   - Use the credentials specified in `appsettings.json` to create the database and user.

### Steps to Run

2. **Clean and Restore Solution**
   - Open the solution in your preferred IDE (e.g., Visual Studio).
   - Clean and restore the solution.

3. **Apply Migrations**
   ```bash
   dotnet ef database update
   ```
   
4. **Set the Startup Project**
   - In your IDE (e.g., Visual Studio), set the `WebApi` project as the startup project.
     - This ensures the API will launch when you run the solution.

5. **Run the Project**
   - Start the project from your IDE or via CLI:
     ```bash
     dotnet run --project WebApi
     ```
   - The API will start, and you should see logs indicating that the server is running.

6. **Access API Documentation**
   - Open your browser and navigate to the Swagger documentation (default location):
     ```
     https://localhost:<port>/swagger
     ```
   - Replace `<port>` with the actual port number displayed in the console when the API starts.
   - Use the Swagger interface to explore the available endpoints and test the API.