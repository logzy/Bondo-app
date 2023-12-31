﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Bondo.Domain.Common.Interfaces;
using System.Reflection;
using Bondo.Domain.Common;
using Bondo.Domain.Entities;

namespace Bondo.Persistence.Contexts;
public class AppSqlDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
     private readonly IDomainEventDispatcher _dispatcher;
 
    public AppSqlDbContext(DbContextOptions<AppSqlDbContext> options,
      IDomainEventDispatcher dispatcher)
        : base(options)
    {
        _dispatcher = dispatcher;
    }
    public AppSqlDbContext(DbContextOptions<AppSqlDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<Contract> Contracts { get; set; }
    public DbSet<ContractApplication> ContractApplications { get; set; }
 
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
 
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
         
        if (_dispatcher == null) return result;
 
        var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToArray();
 
        await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);
 
        return result;
    }
 
    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }
}
