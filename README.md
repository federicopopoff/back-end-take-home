# Summary
* This project is in .Net Framework 4.7.2.
* Projects names: WebApiGuestLogix , WebApiGuestLogix.Tests
* I managed to use the CSV files locally. But, for default, I used those from the github account from GuestLogix. The reason is regarding of the security in Azure against of use files within the project.
* You can run the project locally or use the published site in Azure.

# Running WebAPI - Locally
1. In the solution go the project with name WebApiGuestLogix, set the project to default.
2. Please open Manage Nuget Package and download all missing packages.
3. Run the project.
4. Open the Browser.
6. Write in the URL: https://localhost:44376/api/GetShortestRoute?origin=LAX&destination=EZE

# Running Test
1. You can see the test-project in WebApiGuestLogix.Test
2. Rull All Test.

# Running WebAPI - Azure
1. Open the Browser.
2. Write in the URL: http://webapiguestlogix.azurewebsites.net/api/GetShortestRoute?origin=LAX&destination=EZE
