using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Lost_Found.Controllers
{
    public abstract class ApiControllerBase : ControllerBase
    {
        protected int CurrentKorisnikId =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        protected bool IsAdmin => User.IsInRole("Admin");
    }
}
