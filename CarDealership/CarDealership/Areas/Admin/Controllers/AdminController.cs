namespace CarDealership.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static AdminConstants;

    [Authorize(Roles = AdministratorRoleName)]
    [Area(AreaName)]
    public abstract class AdminController : Controller
    {
    }
}
