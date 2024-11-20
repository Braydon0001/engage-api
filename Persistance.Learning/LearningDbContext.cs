using Domain.Learning.Common;
using Domain.Learning.Entities;
using Engage.Application.Models;
using System.Linq.Expressions;
using System.Reflection;

namespace Persistence.Learning;

public class LearningDbContext : DbContext, ILearningDbContext
{
    private readonly IUserService _user;
    private readonly IDateTimeService _dateTime;

    public LearningDbContext(DbContextOptions<LearningDbContext> options, IUserService user, IDateTimeService dateTime) : base(options)
    {
        _user = user;
        _dateTime = dateTime;
    }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Designation> Designations { get; set; }
    public DbSet<Domain.Learning.Entities.Store> Stores { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<StaffLearningPath> StaffLearningPaths { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseLearningAuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _user.UserName ?? entry.Entity.CreatedBy;
                    entry.Entity.Created = _dateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _user.UserName;
                    entry.Entity.LastModified = _dateTime.Now;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    async Task<OperationStatus> ILearningDbContext.SaveChangesAsync(CancellationToken cancellationToken)
    {
        var opStatus = new OperationStatus();

        try
        {
            opStatus.RecordsAffected = await SaveChangesAsync(cancellationToken);
            opStatus.Status = opStatus.RecordsAffected > 0;
        }
        catch (DbUpdateException ex)
        {
            return OperationStatus.CreateFromException("An error occured while saving.", ex);
        }

        return opStatus;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Apply all IEntityTypeConfiguration configurations
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Configure base entities
        foreach (var entityType in builder.Model.GetEntityTypes()
                                                .Where(e => e.ClrType.IsSubclassOf(typeof(BaseLearningEntity))))
        {
            // Soft delete. Automatically exclude deleted entities
            builder.Entity(entityType.Name, b =>
            {
                b.HasQueryFilter(DeletedLambdaExpression(entityType.ClrType));
            });
        }

        #region Add indexes to code and name columns

        builder.Entity<Store>()
           .HasIndex(e => e.StoreName);

        builder.Entity<Store>()
            .HasIndex(e => e.Code).IsUnique();

        builder.Entity<Staff>()
           .HasIndex(e => e.Name);

        builder.Entity<Staff>()
           .HasIndex(e => e.Surname);
        
        #endregion

        base.OnModelCreating(builder);
    }

    private static LambdaExpression DeletedLambdaExpression(Type type)
    {
        var parameter = Expression.Parameter(type);

        var falseConstant = Expression.Constant(false);
        var deletedProperty = Expression.Property(parameter, "Deleted");
        var body = Expression.Equal(deletedProperty, falseConstant);

        return Expression.Lambda(body, parameter);
    }
}
