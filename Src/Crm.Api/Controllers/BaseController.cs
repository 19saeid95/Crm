using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Api.Controllers;

[ApiController]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseController : ControllerBase
{
}