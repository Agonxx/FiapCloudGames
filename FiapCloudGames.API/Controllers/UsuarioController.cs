using FiapCloudGames.Application.Services;
using FiapCloudGames.Domain.DTOs;
using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.Api.Controllers;

public class UsuarioController : BaseController
{
    private readonly UsuarioService _service;
    public readonly CryptoUtils _cryptoUtils;


    public UsuarioController(IHttpContextAccessor httpContextAccessor,
                                UsuarioService service,
                                CryptoUtils cryptoUtils,
                                InfoToken infoToken) : base(httpContextAccessor, infoToken)
    {
        _service = service;
        _cryptoUtils = cryptoUtils;
    }

    [HttpGet(UsuarioApi.Autenticar)]
    [AllowAnonymous]
    public async Task<IActionResult> Autenticar([FromQuery] string user, string senha)
    {
        var obj = await _service.Autenticar(user, senha);
        return Ok(obj);
    }

    [HttpPost(UsuarioApi.Cadastrar)]
    public async Task<IActionResult> Cadastrar([FromBody] Usuario usuario)
    {
        var result = await _service.CadastrarAsync(usuario);
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
