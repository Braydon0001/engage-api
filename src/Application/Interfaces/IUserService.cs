namespace Engage.Application.Interfaces
{
    public interface IUserService
    {
        string UserName { get; }
        bool IsHostOrganization { get; }
        string ConnectionString { get; }
        int SupplierId { get; }
        bool IsHostSupplier { get; }
        bool HasClaimProcessClaim { get; }
        bool HasClaimProcessAfterCutOffClaim { get; }
        bool HasClaimEditAfterCutOffClaim { get; }
        bool HasTrainingAdminClaim { get; }
        Task<int> GetUserIdAsync();
    }
}
