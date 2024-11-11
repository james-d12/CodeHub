namespace CodeHub.Core.Models;

public sealed class Platform
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}


/*
 Platform Table
 * 1, Azure, Azure
 * 2, Aws, Aws,
 * 3, GitHub, GitHub
 
 Organization Table
 * 1, MyOrganization, blah, blah
 
 OrganizationPlatform Table
 * 1, organizationId, platformId
 -> Unique composite key on organizationId and platformId
 
 -- Adding Platform to Organization
 1. Find Platform with Name
 2. Find Organization to add to.
 3. Create new entry in the OrganizationPlatform Table with the ids from both.
    3.1. If already exists, return 409 conflict error.
 
 -- Removing Platform from Organization
 1. Find Platform with Name
 2. Find Organization to remove from.
 3. Remove entry where composite key is both the platformId and organizationId
  
  // Authentication
  
  
  
*/