﻿using System.Threading.Tasks;
using MassTransit;
using MongoDB.Driver;
using TaskoMask.BuildingBlocks.Application.Bus;
using TaskoMask.BuildingBlocks.Contracts.Events;
using TaskoMask.BuildingBlocks.Web.MVC.Consumers;
using TaskoMask.Services.Owners.Read.Api.Infrastructure.DbContext;

namespace TaskoMask.Services.Owners.Read.Api.Consumers.Projects;

public class ProjectDeletedConsumer : BaseConsumer<ProjectDeleted>
{
    private readonly OwnerReadDbContext _ownerReadDbContext;

    public ProjectDeletedConsumer(IInMemoryBus inMemoryBus, OwnerReadDbContext ownerReadDbContext)
        : base(inMemoryBus)
    {
        _ownerReadDbContext = ownerReadDbContext;
    }

    public override async Task ConsumeMessage(ConsumeContext<ProjectDeleted> context)
    {
        await _ownerReadDbContext.Projects.DeleteOneAsync(p => p.Id == context.Message.Id);
    }
}
