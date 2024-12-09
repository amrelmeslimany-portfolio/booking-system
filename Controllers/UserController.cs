using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [Authorize(Roles = "Any")]
    public class UserController : ControllerBase { }
}
