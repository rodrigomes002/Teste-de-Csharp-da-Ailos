using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Questao5.Application.Commands;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Services.Controllers;

namespace Questoes.Test;
public class ContaControllerTests
{
    private readonly ILogger<ContaController> _logger;
    private readonly IMediator _mediator;
    private readonly ContaController _controller;

    public ContaControllerTests()
    {
        _logger = Substitute.For<ILogger<ContaController>>();
        _mediator = Substitute.For<IMediator>();
        _controller = new ContaController(_logger, _mediator);
    }

    [Theory]
    [MemberData(nameof(Post_Success))]
    public async Task Post_Success_Test(MovimentoRequest model, Idempotencia? idempotencia, ContaCorrente? contaCorrente)
    {
        _mediator.Send(Arg.Any<GetIdempotenciaByIdQuery>())
          .Returns(idempotencia);

        _mediator.Send(Arg.Any<GetContaCorrenteByIdQuery>())
            .Returns(contaCorrente);

        _mediator.Send(Arg.Any<CreateIdempotenciaCommand>())
            .Returns(Unit.Value);

        _mediator.Send(Arg.Any<CreateMovimentoCommand>())
            .Returns("A21SDGFASE1-2182172376-0AASSDA");

        var result = await _controller.Post(model);

        Assert.IsType<OkObjectResult>(result);
    }

    public static IEnumerable<object[]> Post_Success()
    {
        yield return new object[]
        {
            new MovimentoRequest
            {
                IdContaCorrente = "123",
                TipoMovimento = "C",
                Valor = 100,
                ChaveIdempotencia = Guid.NewGuid().ToString()
            },
             null,
             new ContaCorrente
             {
                 IdContaCorrente = "123",
                 Ativo = 1
             },
       };

        yield return new object[]
        {
            new MovimentoRequest
            {
                IdContaCorrente = "123",
                TipoMovimento = "C",
                Valor = 100,
                ChaveIdempotencia = Guid.NewGuid().ToString()
            },
             new Idempotencia()
             {
                 ChaveIdempotencia = "123",
             },
             new ContaCorrente
             {
                 IdContaCorrente = "123",
                 Ativo = 1
             },
       };

    }

    [Theory]
    [MemberData(nameof(Post_BadRequest))]
    public async Task Post_BadRequest_Test(MovimentoRequest model, Idempotencia? idempotencia, ContaCorrente? contaCorrente)
    {
        _mediator.Send(Arg.Any<GetIdempotenciaByIdQuery>())
          .Returns(idempotencia);

        _mediator.Send(Arg.Any<GetContaCorrenteByIdQuery>())
            .Returns(contaCorrente);

        _mediator.Send(Arg.Any<CreateIdempotenciaCommand>())
            .Returns(Unit.Value);

        _mediator.Send(Arg.Any<CreateMovimentoCommand>())
            .Returns("A21SDGFASE1-2182172376-0AASSDA");

        var result = await _controller.Post(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    public static IEnumerable<object[]> Post_BadRequest()
    {
        yield return new object[]
        {
             new MovimentoRequest
            {
                IdContaCorrente = "123",
                TipoMovimento = "C",
                Valor = -100,
                ChaveIdempotencia = Guid.NewGuid().ToString()
            },
             null,
             null
        };

        yield return new object[]
        {
             new MovimentoRequest
            {
                IdContaCorrente = "123",
                TipoMovimento = "X",
                Valor = 100,
                ChaveIdempotencia = Guid.NewGuid().ToString()
            },
             null,
             null
        };

        yield return new object[]
        {
             new MovimentoRequest
            {
                IdContaCorrente = "123",
                TipoMovimento = "C",
                Valor = 100,
                ChaveIdempotencia = Guid.NewGuid().ToString()
            },
             null,
             null
        };

        yield return new object[]
        {
             new MovimentoRequest
            {
                IdContaCorrente = "123",
                TipoMovimento = "C",
                Valor = 100,
                ChaveIdempotencia = Guid.NewGuid().ToString()
            },
             null,
             new ContaCorrente
             {
                 IdContaCorrente = "123",
                 Ativo = 10
             },
        };
    }


    [Theory]
    [MemberData(nameof(Get_Success))]
    public async Task Get_Success_Test(string idContaCorrente, ContaCorrente? contaCorrente, IEnumerable<Movimento> movimentos)
    {
        _mediator.Send(Arg.Any<GetContaCorrenteByIdQuery>())
            .Returns(contaCorrente);

        _mediator.Send(Arg.Any<GetMovimentosQuery>())
            .Returns(movimentos);

        var result = await _controller.Get(idContaCorrente);

        Assert.IsType<OkObjectResult>(result);
    }

    public static IEnumerable<object[]> Get_Success()
    {
        yield return new object[]
        {
           "123",
           new ContaCorrente
           {
              IdContaCorrente = "123",
              Ativo = 1
           },
           new List<Movimento>()
           {
               new Movimento { Valor = 10, TipoMovimento = "C" },
               new Movimento { Valor = 2, TipoMovimento = "C" },
               new Movimento { Valor = 103 , TipoMovimento = "D" }, 
           }
       };
    }

    [Theory]
    [MemberData(nameof(Get_BadRequest))]
    public async Task Get_BadRequest_Test(string idContaCorrente, ContaCorrente? contaCorrente, IEnumerable<Movimento> movimentos)
    {
        _mediator.Send(Arg.Any<GetContaCorrenteByIdQuery>())
            .Returns(contaCorrente);

        _mediator.Send(Arg.Any<GetMovimentosQuery>())
            .Returns(movimentos);

        var result = await _controller.Get(idContaCorrente);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    public static IEnumerable<object[]> Get_BadRequest()
    {
        yield return new object[]
        {
           "123",
           null,
           new List<Movimento>()
           {
               new Movimento { Valor = 10, TipoMovimento = "C" },
               new Movimento { Valor = 2, TipoMovimento = "C" },
               new Movimento { Valor = 103 , TipoMovimento = "D" },
           }
       };

        yield return new object[]
        {
           "123",
           new ContaCorrente
           {
              IdContaCorrente = "123",
              Ativo = 11
           },
           new List<Movimento>()
           {
               new Movimento { Valor = 10, TipoMovimento = "C" },
               new Movimento { Valor = 2, TipoMovimento = "C" },
               new Movimento { Valor = 103 , TipoMovimento = "D" },
           }
       };
    }
}