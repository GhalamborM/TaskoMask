﻿using MassTransit;
using TaskoMask.BuildingBlocks.Application.Bus;
using TaskoMask.BuildingBlocks.Application.Notifications;
using TaskoMask.BuildingBlocks.Web.MVC.Consumers;
using TaskoMask.BuildingBlocks.Contracts.Events;
using TaskoMask.Services.Owners.Read.Api.Infrastructure.DbContext;
using System.Threading.Tasks;
using TaskoMask.Services.Owners.Read.Api.Domain;

namespace TaskoMask.Services.Owners.Read.Api.Consumers
{
    public class OwnerRegisterationCompletedConsumer : BaseConsumer<OwnerRegisterationCompleted>
    {
        private readonly OwnerReadDbContext _ownerReadDbContext;


        public OwnerRegisterationCompletedConsumer(IInMemoryBus inMemoryBus, INotificationHandler notifications, OwnerReadDbContext ownerReadDbContext) : base(inMemoryBus, notifications)
        {
            _ownerReadDbContext = ownerReadDbContext;
        }


        public override async Task ConsumeMessage(ConsumeContext<OwnerRegisterationCompleted> context)
        {
            var owner = new Owner(context.Message.Id)
            {
                DisplayName= context.Message.DisplayName,
                Email= context.Message.Email,
            };
            await _ownerReadDbContext.Owners.InsertOneAsync(owner);
        }
    }
}
