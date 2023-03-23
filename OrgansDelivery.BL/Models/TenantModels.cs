namespace OrganStorage.BL.Models;

public class CreateTenantModel
{
    public string Url { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public class UpdateTenantModel
{
    public string Url { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
