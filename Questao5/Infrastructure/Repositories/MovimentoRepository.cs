using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Repositories;

public class MovimentoRepository : IMovimentoRepository
{
    public async Task<string> AddMovimentoAsync(Movimento entity)
    {
        using var connection = new SqliteConnection("Data Source=database.sqlite");

        var sql = @"INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) values (@idmovimento, @idcontacorrente, @datamovimento, @tipomovimento, @valor)";

        var idmovimento = Guid.NewGuid().ToString();

        var @params = new
        {
            idmovimento = idmovimento,
            idcontacorrente = entity.IdContaCorrente,
            datamovimento = DateTime.Now,
            tipomovimento = entity.TipoMovimento.ToUpper(),
            valor = entity.Valor
        };

        await connection.ExecuteAsync(sql, @params);

        return idmovimento;
    }

    public async Task<IEnumerable<Movimento>> GetMovimentosAsync(string idContaCorrente)
    {
        using var connection = new SqliteConnection("Data Source=database.sqlite");

        var sql = @"SELECT * FROM movimento WHERE idcontacorrente=@idcontacorrente";

        var @params = new
        {
            idcontacorrente = idContaCorrente,
        };

        return await connection.QueryAsync<Movimento>(sql, @params);
    }
}
