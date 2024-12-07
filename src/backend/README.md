# CodeHub Backend

This folder contains all the backend code for CodeHub. It includes the platform specific code for connecting with
and querying from third parties. As well as exposing the API that is used by the frontend to get these resources.

## Platforms

Each platform is given its own class library project. This is so that all the internal code can be neatly separated
and closed off from other platforms. This helps to prevent using classes / code in unintended ways. It also ensures each
platform acts as a ``module`` of sorts, with its own public API of how you interact with it.

Below is the current list of platforms that are supported:

- Azure
- Azure DevOps
- GitHub

### Structure

Each module implements 2 shared interfaces that are used in the CodeHub.API to expose the data fetched from each
platform:

- **IDiscoveryService**: This is responsible for kicking off background tasks for each registered platform to fetch all
  associated data
- **IQueryService**: This is responsible for querying the data back for a given platform. E.g. GetRepositories will
  return
  all cached repositories for that platform implementation

### Configuration

Each platform can be configured and enabled through the ```appsettings.json``` file. Each platform has its own required
way of of authenticating. For example, with DevOps you can use a Personal Access Token.

#### Azure DevOps

For Azure DevOps below is an example. This example will enable the Azure DevOps platform and read the provided 
'Organization' and 'PersonalAccessToken' fields. 

```json
{
  "AzureDevOpsSettings": {
    "Organization": "<INSERT YOUR ORGANIZATION NAME HERE>",
    "PersonalAccessToken": "<INSERT YOUR PERSONAL ACCESS TOKEN HERE>",
    "IsEnabled": true
  }
}
```

## Building

This project uses docker files and docker compose to allow you to spin up the backend quickly.
You can find the docker file within this root folder called 'Backend.Dockerfile'

**Steps to run locally**

1. Clone this repository.
2. Create an ```appsettings.development.json``` file in the root of the CodeHub.API project. It must contain the
   credentials
   and an enabled flag for each platform you want turned on.
3. Run ```docker compose up backend``` in the root folder of the CodeHub project to spin up the backend project.
