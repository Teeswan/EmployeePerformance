using System.Data;
using EPMS.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EPMS.Infrastructure.Repositories;

public class AppraisalCycleRepository : IAppraisalCycleRepository
{
    private readonly string _connectionString;

    public AppraisalCycleRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task<IEnumerable<AppraisalCycle>> GetAllAsync()
    {
        var list = new List<AppraisalCycle>();
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_AppraisalCycles_GetAll", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(MapFromReader(reader));
        }
        return list;
    }

    public async Task<AppraisalCycle?> GetByIdAsync(int cycleId)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_AppraisalCycles_GetById", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@CycleID", cycleId);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return MapFromReader(reader);
        }
        return null;
    }

    public async Task<AppraisalCycle> CreateAsync(AppraisalCycle entity)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_AppraisalCycles_Create", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@CycleName", entity.CycleName);
        command.Parameters.AddWithValue("@StartDate", entity.StartDate);
        command.Parameters.AddWithValue("@EndDate", entity.EndDate);
        command.Parameters.AddWithValue("@EvaluationPeriod", (object?)entity.EvaluationPeriod ?? DBNull.Value);
        command.Parameters.AddWithValue("@CycleStatus", (object?)entity.CycleStatus ?? DBNull.Value);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        await reader.ReadAsync();
        return MapFromReader(reader);
    }

    public async Task<AppraisalCycle?> UpdateAsync(AppraisalCycle entity)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_AppraisalCycles_Update", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@CycleID", entity.CycleId);
        command.Parameters.AddWithValue("@CycleName", entity.CycleName);
        command.Parameters.AddWithValue("@StartDate", entity.StartDate);
        command.Parameters.AddWithValue("@EndDate", entity.EndDate);
        command.Parameters.AddWithValue("@EvaluationPeriod", (object?)entity.EvaluationPeriod ?? DBNull.Value);
        command.Parameters.AddWithValue("@CycleStatus", (object?)entity.CycleStatus ?? DBNull.Value);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return MapFromReader(reader);
        }
        return null;
    }

    public async Task<bool> DeleteAsync(int cycleId)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_AppraisalCycles_Delete", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@CycleID", cycleId);

        await connection.OpenAsync();
        var rowsAffected = await command.ExecuteNonQueryAsync();
        return rowsAffected > 0;
    }

    private static AppraisalCycle MapFromReader(SqlDataReader reader)
    {
        return new AppraisalCycle
        {
            CycleId = reader.GetInt32(reader.GetOrdinal("CycleID")),
            CycleName = reader.GetString(reader.GetOrdinal("CycleName")),
            StartDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("StartDate"))),
            EndDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("EndDate"))),
            EvaluationPeriod = reader.IsDBNull(reader.GetOrdinal("EvaluationPeriod")) ? null : reader.GetString(reader.GetOrdinal("EvaluationPeriod")),
            CycleStatus = reader.IsDBNull(reader.GetOrdinal("CycleStatus")) ? null : reader.GetString(reader.GetOrdinal("CycleStatus"))
        };
    }
}
