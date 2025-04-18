﻿
using System.ComponentModel.DataAnnotations;
using TaskoMask.Domain.Share.Resources;

namespace TaskoMask.Application.Workspace.Projects.Commands.Models
{
    public class CreateProjectCommand : ProjectBaseCommand
    {
        public CreateProjectCommand(string name, string description, string organizationId)
            : base(name, description)

        {
            OrganizationId = organizationId;

        }

        [Required(ErrorMessageResourceName = nameof(DomainMessages.Required), ErrorMessageResourceType = typeof(DomainMessages))]
        public string OrganizationId { get; }
    }
}
