using FiapCloudGames.Domain.DTOs;
using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Enums;
using FiapCloudGames.Domain.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public abstract class BaseController : ControllerBase
{
    public readonly InfoToken _infoToken;

    public BaseController(IHttpContextAccessor httpContextAccessor, InfoToken infoToken)
    {
        _infoToken = infoToken;
    }
}
