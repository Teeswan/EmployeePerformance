using System.Data;
using EPMS.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EPMS.Infrastructure.Repositories;

public class AppraisalResponseRepository : IAppraisalResponseRepository
{
    private readonly string _connectionString;

    public AppraisalResponseRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task<IEnumerable<AppraisalResponse>> GetAllAsync()
    {
        var list = new List<AppraisalResponse>();
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_AppraisalResponses_GetAll", connection)
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

    public async Task<AppraisalResponse?> GetByIdAsync(long responseId)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_AppraisalResponses_GetById", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@ResponseID", responseId);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return MapFromReader(reader);
        }
        return null;
    }

    public async Task<IEnumerable<AppraisalResponse>> GetByEvalIdAsync(int evalId)
    {
        var list = new List<AppraisalResponse>();
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_AppraisalResponses_GetByEvalId", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@EvalID", evalId);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(MapFromReader(reader));
        }
        return list;
    }

    public async Task<AppraisalResponse> CreateAsync(AppraisalResponse entity)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_AppraisalResponses_Create", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@EvalID", (object?)entity.EvalId ?? DBNull.Value);
        command.Parameters.AddWithValue("@QuestionID", (object?)entity.QuestionId ?? DBNull.Value);
        command.Parameters.AddWithValue("@RespondentID", (object?)entity.RespondentId ?? DBNull.Value);
        command.Parameters.AddWithValue("@AnswerText", (object?)entity.AnswerText ?? DBNull.Value);
        command.Parameters.AddWithValue("@RatingValue", (object?)entity.RatingValue ?? DBNull.Value);
        command.Parameters.AddWithValue("@IsAnonymous", (object?)entity.IsAnonymous ?? DBNull.Value);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        await reader.ReadAsync();
        return MapFromReader(reader);
    }

    public async Task<AppraisalResponse?> UpdateAsync(AppraisalResponse entity)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_AppraisalResponses_Update", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@ResponseID", entity.ResponseId);
        command.Parameters.AddWithValue("@EvalID", (object?)entity.EvalId ?? DBNull.Value);
        command.Parameters.AddWithValue("@QuestionID", (object?)entity.QuestionId ?? DBNull.Value);
        command.Parameters.AddWithValue("@RespondentID", (object?)entity.RespondentId ?? DBNull.Value);
        command.Parameters.AddWithValue("@AnswerText", (object?)entity.AnswerText ?? DBNull.Value);
        command.Parameters.AddWithValue("@RatingValue", (object?)entity.RatingValue ?? DBNull.Value);
        command.Parameters.AddWithValue("@IsAnonymous", (object?)entity.IsAnonymous ?? DBNull.Value);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return MapFromReader(reader);
        }
        return null;
    }

    public async Task<bool> DeleteAsync(long responseId)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_AppraisalResponses_Delete", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@ResponseID", responseId);

        await connection.OpenAsync();
        var rowsAffected = await command.ExecuteNonQueryAsync();
        return rowsAffected > 0;
    }

    private static AppraisalResponse MapFromReader(SqlDataReader reader)
    {
        return new AppraisalResponse
        {
            ResponseId = reader.GetInt64(reader.GetOrdinal("ResponseID")),
            EvalId = reader.IsDBNull(reader.GetOrdinal("EvalID")) ? null : reader.GetInt32(reader.GetOrdinal("EvalID")),
            QuestionId = reader.IsDBNull(reader.GetOrdinal("QuestionID")) ? null : reader.GetInt32(reader.GetOrdinal("QuestionID")),
            RespondentId = reader.IsDBNull(reader.GetOrdinal("RespondentID")) ? null : reader.GetInt32(reader.GetOrdinal("RespondentID")),
            AnswerText = reader.IsDBNull(reader.GetOrdinal("AnswerText")) ? null : reader.GetString(reader.GetOrdinal("AnswerText")),
            RatingValue = reader.IsDBNull(reader.GetOrdinal("RatingValue")) ? null : reader.GetInt32(reader.GetOrdinal("RatingValue")),
            IsAnonymous = reader.IsDBNull(reader.GetOrdinal("IsAnonymous")) ? null : reader.GetBoolean(reader.GetOrdinal("IsAnonymous"))
        };
    }
}
