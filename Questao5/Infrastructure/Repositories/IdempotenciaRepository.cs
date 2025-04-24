using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Repositories;

public class IdempotenciaRepository : IIdempotenciaRepository
{
    public async Task<Idempotencia> GetIdempotenciaByIdAsync(string id)
    {
        await using var connection = new SqliteConnection("Data Source=database.sqlite");

        var sql = @"SELECT * FROM idempotencia where chave_idempotencia=@id";

        var @params = new
        {
            id
        };

        return await connection.QueryFirstOrDefaultAsync<Idempotencia>(sql, @params);
    }

    public async Task AddIdempotenciaAsync(Idempotencia entity)
    {
        using var connection = new SqliteConnection("Data Source=database.sqlite");

        var sql = @"INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) VALUES (@chave_idempotencia, @requisicao, @resultado);";

        var @params = new
        {
            chave_idempotencia = entity.ChaveIdempotencia,
            requisicao = entity.Requisicao,
            resultado = entity.Resultado,
        };

        await connection.ExecuteAsync(sql, @params);
    }
}