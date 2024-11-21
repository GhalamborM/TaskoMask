﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskoMask.BuildingBlocks.Application.Bus;

namespace TaskoMask.BuildingBlocks.Web.MVC.Pages;

public class BasePageModel : PageModel
{
    protected readonly IRequestDispatcher _requestDispatcher;

    public BasePageModel(IRequestDispatcher requestDispatcher)
    {
        _requestDispatcher = requestDispatcher;
    }
}
