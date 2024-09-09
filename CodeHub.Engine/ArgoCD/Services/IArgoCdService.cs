using CodeHub.Engine.ArgoCD.Models.Application;

namespace CodeHub.Engine.ArgoCD.Services;

internal interface IArgoCdService
{
    Task<Application?> GetApplication(string cookie);
}