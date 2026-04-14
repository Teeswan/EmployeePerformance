using System.Data;
using EPMS.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EPMS.Infrastructure.Repositories;

public class PerformanceOutcomeRepository : IPerformanceOutcomeRepository
{
    private readonly string _connectionString;

    public PerformanceOutcomeRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task<IEnumerable<PerformanceOutcome>> GetAllAsync()
    {
        var list = new List<PerformanceOutcome>();
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_PerformanceOutcomes_GetAll", connection)
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

    public async Task<PerformanceOutcome?> GetByIdAsync(int outcomeId)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_PerformanceOutcomes_GetById", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@OutcomeID", outcomeId);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return MapFromReader(reader);
        }
        return null;
    }

    public async Task<IEnumerable<PerformanceOutcome>> GetByEmployeeIdAsync(int employeeId)
    {
        var list = new List<PerformanceOutcome>();
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_PerformanceOutcomes_GetByEmployeeId", connection)
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

    public async Task<IEnumerable<PerformanceOutcome>> GetByCycleIdAsync(int cycleId)
    {
        var list = new List<PerformanceOutcome>();
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_PerformanceOutcomes_GetByCycleId", connection)
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

    public async Task<PerformanceOutcome> CreateAsync(PerformanceOutcome entity)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_PerformanceOutcomes_Create", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@EmployeeID", (object?)entity.EmployeeId ?? DBNull.Value);
        command.Parameters.AddWithValue("@CycleID", (object?)entity.CycleId ?? DBNull.Value);
        command.Parameters.AddWithValue("@RecommendationType", (object?)entity.RecommendationType ?? DBNull.Value);
        command.Parameters.AddWithValue("@OldPositionID", (object?)entity.OldPositionId ?? DBNull.Value);
        command.Parameters.AddWithValue("@NewPositionID", (object?)entity.NewPositionId ?? DBNull.Value);
        command.Parameters.AddWithValue("@OldLevelID", (object?)entity.OldLevelId ?? DBNull.Value);
        command.Parameters.AddWithValue("@NewLevelID", (object?)entity.NewLevelId ?? DBNull.Value);
        command.Parameters.AddWithValue("@ApprovalStatus", (object?)entity.ApprovalStatus ?? DBNull.Value);
        command.Parameters.AddWithValue("@EffectiveDate", (object?)entity.EffectiveDate ?? DBNull.Value);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        await reader.ReadAsync();
        return MapFromReader(reader);
    }

    public async Task<PerformanceOutcome?> UpdateAsync(PerformanceOutcome entity)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_PerformanceOutcomes_Update", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@OutcomeID", entity.OutcomeId);
        command.Parameters.AddWithValue("@EmployeeID", (object?)entity.EmployeeId ?? DBNull.Value);
        command.Parameters.AddWithValue("@CycleID", (object?)entity.CycleId ?? DBNull.Value);
        command.Parameters.AddWithValue("@RecommendationType", (object?)entity.RecommendationType ?? DBNull.Value);
        command.Parameters.AddWithValue("@OldPositionID", (object?)entity.OldPositionId ?? DBNull.Value);
        command.Parameters.AddWithValue("@NewPositionID", (object?)entity.NewPositionId ?? DBNull.Value);
        command.Parameters.AddWithValue("@OldLevelID", (object?)entity.OldLevelId ?? DBNull.Value);
        command.Parameters.AddWithValue("@NewLevelID", (object?)entity.NewLevelId ?? DBNull.Value);
        command.Parameters.AddWithValue("@ApprovalStatus", (object?)entity.ApprovalStatus ?? DBNull.Value);
        command.Parameters.AddWithValue("@EffectiveDate", (object?)entity.EffectiveDate ?? DBNull.Value);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return MapFromReader(reader);
        }
        return null;
    }

    public async Task<bool> DeleteAsync(int outcomeId)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_PerformanceOutcomes_Delete", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@OutcomeID", outcomeId);

        await connection.OpenAsync();
        var rowsAffected = await command.ExecuteNonQueryAsync();
        return rowsAffected > 0;
    }

    private static PerformanceOutcome MapFromReader(SqlDataReader reader)
    {
        return new PerformanceOutcome
        {
            OutcomeId = reader.GetInt32(reader.GetOrdinal("OutcomeID")),
            EmployeeId = reader.IsDBNull(reader.GetOrdinal("EmployeeID")) ? null : reader.GetInt32(reader.GetOrdinal("EmployeeID")),
            CycleId = reader.IsDBNull(reader.GetOrdinal("CycleID")) ? null : reader.GetInt32(reader.GetOrdinal("CycleID")),
            RecommendationType = reader.IsDBNull(reader.GetOrdinal("RecommendationType")) ? null : reader.GetString(reader.GetOrdinal("RecommendationType")),
            OldPositionId = reader.IsDBNull(reader.GetOrdinal("OldPositionID")) ? null : reader.GetInt32(reader.GetOrdinal("OldPositionID")),
            NewPositionId = reader.IsDBNull(reader.GetOrdinal("NewPositionID")) ? null : reader.GetInt32(reader.GetOrdinal("NewPositionID")),
            OldLevelId = reader.IsDBNull(reader.GetOrdinal("OldLevelID")) ? null : reader.GetString(reader.GetOrdinal("OldLevelID")),
            NewLevelId = reader.IsDBNull(reader.GetOrdinal("NewLevelID")) ? null : reader.GetString(reader.GetOrdinal("NewLevelID")),
            ApprovalStatus = reader.IsDBNull(reader.GetOrdinal("ApprovalStatus")) ? null : reader.GetString(reader.GetOrdinal("ApprovalStatus")),
            EffectiveDate = reader.IsDBNull(reader.GetOrdinal("EffectiveDate")) ? null : DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("EffectiveDate")))
        };
    }
}
