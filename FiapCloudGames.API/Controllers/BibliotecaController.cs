using FiapCloudGames.Application.Services;
using FiapCloudGames.Domain.Constants;
using FiapCloudGames.Domain.DTOs;
using FiapCloudGames.Domain.Entities;
using LibraryApi.Bases;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.Api.Controllers;

public class BibliotecaController : BaseController
{
    private readonly BibliotecaService _service;


    public BibliotecaController(IHttpContextAccessor httpContextAccessor,
                                BibliotecaService bibliotecaService,
                                InfoToken infoToken) : base(httpContextAccessor, infoToken)
    {
        _service = bibliotecaService;
    }

    [HttpGet(ItemBibliotecaApi.GetMyGames)]
    [RoleAuthorize(Roles.UsuarioAccess)]
    public async Task<IActionResult> GetMyGames()
    {
        var obj = await _service.GetMyGamesAsync();
        return Ok(obj);
    }

    [HttpGet(ItemBibliotecaApi.GetUserGamesById)]
    [RoleAuthorize(Roles.AdminAccess)]
    public async Task<IActionResult> GetUserGamesById([FromQuery] int id)
    {
        var obj = await _service.GetUserGamesByIdAsync(id);
        return Ok(obj);
    }

    [HttpPost(ItemBibliotecaApi.BuyGame)]
    [RoleAuthorize(Roles.UsuarioAccess)]
    public async Task<IActionResult> BuyGame([FromQuery] int id)
    {
        var obj = await _service.BuyGameAsync(id);
        return Ok(obj);
    }
}
