namespace Sample_Identity_jwt.Services
{
    public interface IPermissionService
    {
        Task<HashSet<string>> GetPermissionsAsync(Guid userId);
    }
}
