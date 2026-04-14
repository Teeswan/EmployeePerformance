using System.Data;
using EPMS.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EPMS.Infrastructure.Repositories;

public class AppraisalQuestionRepository : IAppraisalQuestionRepository
{
    private readonly string _connectionString;

    public AppraisalQuestionRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task<IEnumerable<AppraisalQuestion>> GetAllAsync()
    {
        var list = new List<AppraisalQuestion>();
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_AppraisalQuestions_GetAll", connection)
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

    public async Task<AppraisalQuestion?> GetByIdAsync(int questionId)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_AppraisalQuestions_GetById", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@QuestionID", questionId);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return MapFromReader(reader);
        }
        return null;
    }

    public async Task<AppraisalQuestion> CreateAsync(AppraisalQuestion entity)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_AppraisalQuestions_Create", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@QuestionText", entity.QuestionText);
        command.Parameters.AddWithValue("@Category", (object?)entity.Category ?? DBNull.Value);
        command.Parameters.AddWithValue("@IsRequired", (object?)entity.IsRequired ?? DBNull.Value);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        await reader.ReadAsync();
        return MapFromReader(reader);
    }

    public async Task<AppraisalQuestion?> UpdateAsync(AppraisalQuestion entity)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_AppraisalQuestions_Update", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@QuestionID", entity.QuestionId);
        command.Parameters.AddWithValue("@QuestionText", entity.QuestionText);
        command.Parameters.AddWithValue("@Category", (object?)entity.Category ?? DBNull.Value);
        command.Parameters.AddWithValue("@IsRequired", (object?)entity.IsRequired ?? DBNull.Value);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return MapFromReader(reader);
        }
        return null;
    }

    public async Task<bool> DeleteAsync(int questionId)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_AppraisalQuestions_Delete", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@QuestionID", questionId);

        await connection.OpenAsync();
        var rowsAffected = await command.ExecuteNonQueryAsync();
        return rowsAffected > 0;
    }

    private static AppraisalQuestion MapFromReader(SqlDataReader reader)
    {
        return new AppraisalQuestion
        {
            QuestionId = reader.GetInt32(reader.GetOrdinal("QuestionID")),
            QuestionText = reader.GetString(reader.GetOrdinal("QuestionText")),
            Category = reader.IsDBNull(reader.GetOrdinal("Category")) ? null : reader.GetString(reader.GetOrdinal("Category")),
            IsRequired = reader.IsDBNull(reader.GetOrdinal("IsRequired")) ? null : reader.GetBoolean(reader.GetOrdinal("IsRequired"))
        };
    }
}
