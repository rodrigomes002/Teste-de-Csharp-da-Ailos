using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Questao5.Application.Commands;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("v1/conta")]
    public class ContaController : ControllerBase
    {
        private readonly ILogger<ContaController> _logger;
        private readonly IMediator _mediator;

        public ContaController(ILogger<ContaController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("movimento")]
        public async Task<IActionResult> Post([FromBody] MovimentoRequest model)
        {
            if (model.Valor < 0)
                return BadRequest(new Result { Mensagem = "Apenas valores positivos podem ser recebidos", Tipo = "INVALID_VALUE" });

            if ((model.TipoMovimento.ToUpper() != "C") && (model.TipoMovimento.ToUpper() != "D"))
                return BadRequest(new Result { Mensagem = "Apenas os tipos “débito” ou “crédito” podem ser aceitos", Tipo = "INVALID_TYPE" });

            var idempotencia = await _mediator.Send(new GetIdempotenciaByIdQuery(model.ChaveIdempotencia));

            if (idempotencia is not null)
                return Ok(new Result { Mensagem = "Requisição já processada.", Tipo = string.Empty });

            var contaCorrente = await _mediator.Send(new GetContaCorrenteByIdQuery(model.IdContaCorrente));

            if (contaCorrente is null)
                return BadRequest(new Result { Mensagem = "Apenas contas correntes cadastradas podem receber movimentação", Tipo = "INVALID_ACCOUNT" });

            if (contaCorrente.Ativo != 1)
                return BadRequest(new Result { Mensagem = "Apenas contas correntes ativas podem receber movimentação", Tipo = "INACTIVE_ACCOUNT" });

            var idempotenciaModel = new IdempotenciaRequest()
            {
                ChaveIdempotencia = Guid.NewGuid().ToString(),
                Requisicao = JsonConvert.SerializeObject(model),
                Resultado = "200"
            };

            await _mediator.Send(new CreateIdempotenciaCommand(idempotenciaModel.ChaveIdempotencia, idempotenciaModel.Requisicao, idempotenciaModel.Resultado));

            return Ok(await _mediator.Send(new CreateMovimentoCommand(model.IdContaCorrente, model.TipoMovimento, model.Valor)));

        }

        [HttpGet]
        [Route("saldo")]
        public async Task<IActionResult> Get([FromQuery] string idContaCorrente)
        {
            var contaCorrente = await _mediator.Send(new GetContaCorrenteByIdQuery(idContaCorrente));

            if (contaCorrente is null)
                return BadRequest(new Result { Mensagem = "Apenas contas correntes cadastradas podem receber movimentação", Tipo = "INVALID_ACCOUNT" });

            if (contaCorrente.Ativo != 1)
                return BadRequest(new Result { Mensagem = "Apenas contas correntes ativas podem receber movimentação", Tipo = "INACTIVE_ACCOUNT" });

            var movimentos = await _mediator.Send(new GetMovimentosQuery(idContaCorrente));

            double saldo = 0;

            var creditos = movimentos
                .Where(m => m.TipoMovimento == "C")
                .Sum(m => m.Valor);

            var debitos = movimentos
                .Where(m => m.TipoMovimento == "D")
                .Sum(m => m.Valor);

            saldo = (creditos - debitos);

            return Ok(new { contaCorrente.Numero, contaCorrente.Nome, DateTime.Now, saldo });
        }
    }
}