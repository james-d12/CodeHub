# CodeHub

[![CI Workflow](https://github.com/james-d12/CodeHub/actions/workflows/ci.yml/badge.svg)](https://github.com/james-d12/CodeHub/actions/workflows/ci.yml)
[![Dependabot Updates](https://github.com/james-d12/CodeHub/actions/workflows/dependabot/dependabot-updates/badge.svg)](https://github.com/james-d12/CodeHub/actions/workflows/dependabot/dependabot-updates)

A service catalog hub for viewing all of your software infrastructure from various third party providers all in one
place!

## Why?

The idea for this project came to me at work, where I often struggled to get a clear view of all the infrastructure we
had. It was frustrating to have to juggle 5+ different websites just to get the information I needed. CodeHub aims to
solve this by consolidating everything into a single, consistent UI.

## How It Works

At the moment, CodeHub works by spinning up background tasks called 'Discovery Services' that will fetch all relevant
data from your connected third parties. It then presents these via a unified portal that you can use to browse them.
It’s designed to be deployed and run locally, such as in a Kubernetes cluster or on a local machine.

## What Does The Portal Look Like?

At the moment it is in its infancy, so the UI is very basic, but below are some screenshots taken (07-12-2024) of its
current state.

![img.png](./docs/Portal-Screenshot-GitHub.png)

## Documentation

<details>

<summary>CodeHub Portal</summary>

# CodeHub Portal

This folder contains the projects used for the Frontend part of CodeHub. It is currently written using Blazor and
MudBlazor.

</details>


<details>

<summary>CodeHub Api</summary>

# CodeHub Api

The CodeHub.Api folder is the other executable application in this project. It is the Api that is called from the
portal.

## Modules

Each platform is in its own folder in the CodeHub.Module project. Below is the current list of platforms that are
supported:

- Azure
- Azure DevOps
- GitHub
- GitLab

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

#### Azure

For Azure you just need to tell it to be enabled. It uses the ```DefaultArmCredential``` to authenticate.
See more information about what sources it gets authentication information from
here: https://learn.microsoft.com/en-us/dotnet/api/azure.identity.defaultazurecredential?view=azure-dotnet

```json
{
  "AzureSettings": {
    "IsEnabled": true,
    "SubscriptionFilters": []
  }
}
```

#### Azure DevOps

For Azure DevOps below is an example. This example will enable the Azure DevOps platform and read the provided
'Organization' and 'PersonalAccessToken' fields.

```json
{
  "AzureDevOpsSettings": {
    "Organization": "<INSERT YOUR ORGANIZATION NAME HERE>",
    "PersonalAccessToken": "<INSERT YOUR PERSONAL ACCESS TOKEN HERE>",
    "IsEnabled": true,
    "ProjectFilters": []
  }
}
```

#### GitHub

For GitHub you need to provide the AgentName (Name used in the Header for identifying request) and the Token.

```json
{
  "GitHubSettings": {
    "AgentName": "<INSERT YOUR AGENT NAME HERE>",
    "Token": "<INSERT YOUR TOKEN HERE>",
    "IsEnabled": true
  }
}
```

#### GitLab

For GitLab you need to provide the Host Url and the Token.

```json
{
  "GitLabSettings": {
    "HostUrl": "<INSERT YOUR HOST URL HERE>",
    "Token": "<INSERT YOUR TOKEN HERE>",
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

</details>

## Future Plans

- GitHub, GitLab & Bitbucket for Git providers.
- Azure / AWS for cloud providers
- SonarCloud integration for security and static analysis
- Kubernetes API integration for monitoring your orchestration stack.
