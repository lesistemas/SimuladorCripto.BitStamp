using BitstampSimulador.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BitstampSimulador.API.Models;

[ApiController]
[Route("[controller]")]
public class SimulacaoController : ControllerBase
{
    private readonly IServicoSimulacao _servico;

    public SimulacaoController(IServicoSimulacao servico)
    {
        _servico = servico;
    }

    [HttpPost]
    public IActionResult Post([FromBody] RequisicaoSimulacao req)
    {
        var resultado = _servico.Simular(req.Ativo, req.Tipo, req.Quantidade);
        if (resultado == null) return BadRequest("Simulação inválida.");

        return Ok(resultado);
    }
}
