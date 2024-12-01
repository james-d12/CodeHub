# CodeHub Backend
This folder contains all the backend code for CodeHub. It includes the platform specific code for connecting with 
and querying from third parties. As well as exposing the API that is used by the frontend to get these resources.

## Building
This project uses docker files and docker compose to allow you to spin up the backend quickly. 
You can find the docker file within this root folder called 'Backend.Dockerfile' 

**Steps to run locally**
1. Clone this repository.
2. Run 'docker compose up backend' in the root folder of the CodeHub project to spin up the backend project.

## Platforms
Each platform is given its own class library project. This is so that all the internal code can be neatly separated 
and closed off from other code mistakenly using it through the use of the internal keyword. 

Below is the current list of platforms that are supported:
- Azure 
- Azure DevOps
