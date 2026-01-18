using FiapCloudGames.Application.Services;
using FiapCloudGames.Domain.Constants;
using FiapCloudGames.Domain.DTOs;
using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Interfaces.Utils;
using LibraryApi.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.Api.Controllers;

public class UsuarioController : BaseController
{
    private readonly UsuarioService _service;
    public readonly ICryptoUtils _cryptoUtils;


    public UsuarioController(IHttpContextAccessor httpContextAccessor,
                                UsuarioService service,
                                ICryptoUtils cryptoUtils,
                                InfoToken infoToken) : base(httpContextAccessor, infoToken)
    {
        _service = service;
        _cryptoUtils = cryptoUtils;
    }

    [HttpPost(UsuarioApi.Auth)]
    [AllowAnonymous]
    public async Task<IActionResult> Auth([FromBody] LoginRequest login)
    {
        var obj = await _service.AuthAsync(login.Email, login.Senha);
        return Ok(obj);
    }

    [HttpGet(UsuarioApi.GetById)]
    [RoleAuthorize(Roles.AdminAccess)]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
        var obj = await _service.GetByIdAsync(id);
        return Ok(obj);
    }

    [HttpGet(UsuarioApi.GetMe)]
    public async Task<IActionResult> GetMe()
    {
        var obj = await _service.GetMeAsync();
        return Ok(obj);
    }

    [HttpGet(UsuarioApi.GetAll)]
    [RoleAuthorize(Roles.AdminAccess)]
    public async Task<IActionResult> GetAll()
    {
        var obj = await _service.GetAllAsync();
        return Ok(obj);
    }

    [HttpPost(UsuarioApi.Create)]
    [RoleAuthorize(Roles.AdminAccess)]
    public async Task<IActionResult> Create([FromBody] Usuario usuarioObj)
    {
        var result = await _service.CreateAsync(usuarioObj);
        return Ok(result);
    }

    [HttpPut(UsuarioApi.Update)]
    [RoleAuthorize(Roles.AdminAccess)]
    public async Task<IActionResult> Update([FromBody] Usuario usuarioObj)
    {
        var result = await _service.UpdateAsync(usuarioObj);
        return Ok(result);
    }

    [HttpDelete(UsuarioApi.DeleteById)]
    [RoleAuthorize(Roles.AdminAccess)]
    public async Task<IActionResult> DeleteById([FromQuery]int id)
    {
        var result = await _service.DeleteByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("Cryp")]
    [AllowAnonymous]
    public async Task<IActionResult> Cryp([FromQuery] string senha)
    {
        senha = _cryptoUtils.EncryptString(senha);
        return Ok(senha);
    }

}
