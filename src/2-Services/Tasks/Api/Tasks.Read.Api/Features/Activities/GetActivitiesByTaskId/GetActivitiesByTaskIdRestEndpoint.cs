using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskoMask.BuildingBlocks.Application.Bus;
using TaskoMask.BuildingBlocks.Application.Services;
using TaskoMask.BuildingBlocks.Contracts.Dtos.Activities;
using TaskoMask.BuildingBlocks.Contracts.Helpers;
using TaskoMask.BuildingBlocks.Web.MVC.Controllers;

namespace TaskoMask.Services.Tasks.Read.Api.Features.Activities.GetActivitiesByTaskId;

[Authorize("user-read-access")]
[Tags("Activities")]
public class GetActivitiesByTaskIdRestEndpoint : BaseApiController
{
    public GetActivitiesByTaskIdRestEndpoint(IAuthenticatedUserService authenticatedUserService, IRequestDispatcher requestDispatcher)
        : base(authenticatedUserService, requestDispatcher) { }

    /// <summary>
    /// get activities for a task
    /// </summary>
    [HttpGet]
    [Route("tasks/{taskId}/activities")]
    public async Task<Result<IEnumerable<GetActivityDto>>> Get(string taskId)
    {
        return await _requestDispatcher.SendQuery(new GetActivitiesByTaskIdRequest(taskId));
    }
}
