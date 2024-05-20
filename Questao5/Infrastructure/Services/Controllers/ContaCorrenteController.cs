using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;

[ApiController]
[Route("[controller]")]
public class ContaCorrenteController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContaCorrenteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("movimentacao")]
    public async Task<IActionResult> Movimentacao([FromBody] MovimentoRequest request)
    {
        try
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        catch (HttpRequestException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet("saldo")]
    public async Task<IActionResult> Saldo([FromQuery] SaldoRequest request)
    {
        try
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        catch (HttpRequestException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}
