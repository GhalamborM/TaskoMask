﻿using System.Threading.Tasks;
using MassTransit;
using TaskoMask.BuildingBlocks.Application.Bus;
using TaskoMask.BuildingBlocks.Contracts.Events;

namespace TaskoMask.BuildingBlocks.Infrastructure.Bus;

/// <summary>
///
/// </summary>
public class MessageBus : IMessageBus
{
    #region Fields

    private readonly IPublishEndpoint _publishEndpoint;

    #endregion

    #region Ctors

    public MessageBus(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    #endregion

    #region Public Methods




    /// <summary>
    ///
    /// </summary>
    public async Task Publish<TEvent>(TEvent @event)
        where TEvent : IIntegrationEvent
    {
        await _publishEndpoint.Publish(@event);
    }

    #endregion

    #region Private Methods



    #endregion
}
