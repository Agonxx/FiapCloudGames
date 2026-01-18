using FiapCloudGames.Application.Services;
using FiapCloudGames.Domain.Constants;
using FiapCloudGames.Domain.DTOs;
using FiapCloudGames.Domain.Entities;
using LibraryApi.Bases;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.Api.Controllers;

public class JogoController : BaseController
{
    private readonly JogoService _service;


    public JogoController(IHttpContextAccessor httpContextAccessor,
                                JogoService jogoService,
                                InfoToken infoToken) : base(httpContextAccessor, infoToken)
    {
        _service = jogoService;
    }

    [HttpGet(JogoApi.GetAll)]
    [RoleAuthorize(Roles.UsuarioAccess)]
    public async Task<IActionResult> GetAll()
    {
        var obj = await _service.GetAllAsync();
        return Ok(obj);
    }

    [HttpGet(JogoApi.GetById)]
    [RoleAuthorize(Roles.UsuarioAccess)]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
        var obj = await _service.GetByIdAsync(id);
        return Ok(obj);
    }

    [HttpPost(JogoApi.Create)]
    [RoleAuthorize(Roles.AdminAccess)]
    public async Task<IActionResult> Create([FromBody] Jogo jogoObj)
    {
        var obj = await _service.CreateAsync(jogoObj);
        return Ok(obj);
    }

    [HttpPut(JogoApi.Update)]
    [RoleAuthorize(Roles.AdminAccess)]
    public async Task<IActionResult> Update([FromBody] Jogo jogoObj)
    {
        var obj = await _service.UpdateAsync(jogoObj);
        return Ok(obj);
    }

    [HttpDelete(JogoApi.DeleteById)]
    [RoleAuthorize(Roles.AdminAccess)]
    public async Task<IActionResult> DeleteById([FromQuery] int id)
    {
        var obj = await _service.DeleteByIdAsync(id);
        return Ok(obj);
    }
}
