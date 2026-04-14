using System.Data;
using EPMS.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EPMS.Infrastructure.Repositories;

public class PerformanceEvaluationRepository : IPerformanceEvaluationRepository
{
    private readonly string _connectionString;

    public PerformanceEvaluationRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task<IEnumerable<PerformanceEvaluation>> GetAllAsync()
    {
        var list = new List<PerformanceEvaluation>();
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_PerformanceEvaluations_GetAll", connection)
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

    public async Task<PerformanceEvaluation?> GetByIdAsync(int evalId)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_PerformanceEvaluations_GetById", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@EvalID", evalId);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return MapFromReader(reader);
        }
        return null;
    }

    public async Task<IEnumerable<PerformanceEvaluation>> GetByEmployeeIdAsync(int employeeId)
    {
        var list = new List<PerformanceEvaluation>();
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_PerformanceEvaluations_GetByEmployeeId", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@EmployeeID", employeeId);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(MapFromReader(reader));
        }
        return list;
    }

    public async Task<IEnumerable<PerformanceEvaluation>> GetByCycleIdAsync(int cycleId)
    {
        var list = new List<PerformanceEvaluation>();
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_PerformanceEvaluations_GetByCycleId", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@CycleID", cycleId);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(MapFromReader(reader));
        }
        return list;
    }

    public async Task<PerformanceEvaluation> CreateAsync(PerformanceEvaluation entity)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_PerformanceEvaluations_Create", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@EmployeeID", (object?)entity.EmployeeId ?? DBNull.Value);
        command.Parameters.AddWithValue("@CycleID", (object?)entity.CycleId ?? DBNull.Value);
        command.Parameters.AddWithValue("@SelfRating", (object?)entity.SelfRating ?? DBNull.Value);
        command.Parameters.AddWithValue("@ManagerRating", (object?)entity.ManagerRating ?? DBNull.Value);
        command.Parameters.AddWithValue("@SelfComments", (object?)entity.SelfComments ?? DBNull.Value);
        command.Parameters.AddWithValue("@ManagerComments", (object?)entity.ManagerComments ?? DBNull.Value);
        command.Parameters.AddWithValue("@FinalRatingScore", (object?)entity.FinalRatingScore ?? DBNull.Value);
        command.Parameters.AddWithValue("@IsFinalized", (object?)entity.IsFinalized ?? DBNull.Value);
        command.Parameters.AddWithValue("@FinalizedAt", (object?)entity.FinalizedAt ?? DBNull.Value);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        await reader.ReadAsync();
        return MapFromReader(reader);
    }

    public async Task<PerformanceEvaluation?> UpdateAsync(PerformanceEvaluation entity)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_PerformanceEvaluations_Update", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@EvalID", entity.EvalId);
        command.Parameters.AddWithValue("@EmployeeID", (object?)entity.EmployeeId ?? DBNull.Value);
        command.Parameters.AddWithValue("@CycleID", (object?)entity.CycleId ?? DBNull.Value);
        command.Parameters.AddWithValue("@SelfRating", (object?)entity.SelfRating ?? DBNull.Value);
        command.Parameters.AddWithValue("@ManagerRating", (object?)entity.ManagerRating ?? DBNull.Value);
        command.Parameters.AddWithValue("@SelfComments", (object?)entity.SelfComments ?? DBNull.Value);
        command.Parameters.AddWithValue("@ManagerComments", (object?)entity.ManagerComments ?? DBNull.Value);
        command.Parameters.AddWithValue("@FinalRatingScore", (object?)entity.FinalRatingScore ?? DBNull.Value);
        command.Parameters.AddWithValue("@IsFinalized", (object?)entity.IsFinalized ?? DBNull.Value);
        command.Parameters.AddWithValue("@FinalizedAt", (object?)entity.FinalizedAt ?? DBNull.Value);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return MapFromReader(reader);
        }
        return null;
    }

    public async Task<bool> DeleteAsync(int evalId)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_PerformanceEvaluations_Delete", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@EvalID", evalId);

        await connection.OpenAsync();
        var rowsAffected = await command.ExecuteNonQueryAsync();
        return rowsAffected > 0;
    }

    private static PerformanceEvaluation MapFromReader(SqlDataReader reader)
    {
        return new PerformanceEvaluation
        {
            EvalId = reader.GetInt32(reader.GetOrdinal("EvalID")),
            EmployeeId = reader.IsDBNull(reader.GetOrdinal("EmployeeID")) ? null : reader.GetInt32(reader.GetOrdinal("EmployeeID")),
            CycleId = reader.IsDBNull(reader.GetOrdinal("CycleID")) ? null : reader.GetInt32(reader.GetOrdinal("CycleID")),
            SelfRating = reader.IsDBNull(reader.GetOrdinal("SelfRating")) ? null : reader.GetInt32(reader.GetOrdinal("SelfRating")),
            ManagerRating = reader.IsDBNull(reader.GetOrdinal("ManagerRating")) ? null : reader.GetInt32(reader.GetOrdinal("ManagerRating")),
            SelfComments = reader.IsDBNull(reader.GetOrdinal("SelfComments")) ? null : reader.GetString(reader.GetOrdinal("SelfComments")),
            ManagerComments = reader.IsDBNull(reader.GetOrdinal("ManagerComments")) ? null : reader.GetString(reader.GetOrdinal("ManagerComments")),
            FinalRatingScore = reader.IsDBNull(reader.GetOrdinal("FinalRatingScore")) ? null : reader.GetDecimal(reader.GetOrdinal("FinalRatingScore")),
            IsFinalized = reader.IsDBNull(reader.GetOrdinal("IsFinalized")) ? null : reader.GetBoolean(reader.GetOrdinal("IsFinalized")),
            FinalizedAt = reader.IsDBNull(reader.GetOrdinal("FinalizedAt")) ? null : reader.GetDateTime(reader.GetOrdinal("FinalizedAt"))
        };
    }
}
