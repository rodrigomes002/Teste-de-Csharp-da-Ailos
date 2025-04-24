using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Repositories;

public class ContaCorrenteRepository : IContaCorrenteRepository
{
    public async Task<ContaCorrente> GetContaCorrenteByIdAsync(string id)
    {
        await using var connection = new SqliteConnection("Data Source=database.sqlite");

        var sql = @"SELECT * FROM contacorrente where idcontacorrente=@id";

        var @params = new
        {
            id
        };

        return await connection.QueryFirstOrDefaultAsync<ContaCorrente>(sql, @params);
    }
}
