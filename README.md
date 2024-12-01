# CodeHub
A service catalog hub for viewing all of your software infrastructure from various third party providers all in one place!

## Why?
I thought of this idea whilst at work. I always encountered issues with trying to see how much infrastructure we actually had. Additionally 
having to have 5+ different websites open at once to view what we had became a big problem. CodeHub attempts to consolidate all of that into
one consistent UI. 

It's been designed in mind to be deployed and run locally. For example deployed to a Kubernetes cluster and spun up locally.

## How It Works
At the moment, CodeHub works by spinning up background tasks called 'Discovery Services' that will fetch all relevant data
from your connected third parties. It then presents these via a unified portal that you can use to browse them.

## Future Plans
- GitHub, GitLab & Bitbucket for Git providers.
- Azure / AWS for cloud providers
- SonarCloud integration for security and static analysis
- SOOS integration for security analysis.
- Kubernetes API integration for monitoring your orchestration stack.

[![CI Workflow](https://github.com/james-d12/CodeHub/actions/workflows/ci.yml/badge.svg)](https://github.com/james-d12/CodeHub/actions/workflows/ci.yml)
