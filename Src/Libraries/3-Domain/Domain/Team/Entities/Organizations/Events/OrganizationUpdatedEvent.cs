﻿using TaskoMask.Domain.Core.Events;

namespace TaskoMask.Domain.Team.Entities.Organizations.Events
{
    public class OrganizationUpdatedEvent : DomainEvent
    {
        public OrganizationUpdatedEvent(string id, string name, string description) : base(entityId: id, entityType: nameof(Organization))
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public string Id { get; }
        public string Name { get;  }
        public string Description { get; }
    }
}
