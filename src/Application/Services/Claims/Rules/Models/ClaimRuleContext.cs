namespace Engage.Application.Services.Claims.Rules.Models;

public class ClaimRuleContext
{
    public Claim Claim { get; private set; }
    public IAppDbContext DbContext { get; private set; }
    public IUserService User { get; private set; }
    
    public ClaimRuleContext(Claim claim, IAppDbContext dbContext, IUserService user)
    {
        Claim = claim;
        DbContext = dbContext;
        User = user;        
    }
}
