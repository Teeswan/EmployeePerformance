using System.Data;
using EPMS.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EPMS.Infrastructure.Repositories;

public class FormQuestionRepository : IFormQuestionRepository
{
    private readonly string _connectionString;

    public FormQuestionRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task<IEnumerable<FormQuestion>> GetAllAsync()
    {
        var list = new List<FormQuestion>();
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_FormQuestions_GetAll", connection)
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

    public async Task<IEnumerable<FormQuestion>> GetByFormIdAsync(int formId)
    {
        var list = new List<FormQuestion>();
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_FormQuestions_GetByFormId", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@FormID", formId);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(MapFromReader(reader));
        }
        return list;
    }

    public async Task<FormQuestion?> GetByFormAndQuestionIdAsync(int formId, int questionId)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_FormQuestions_GetByFormAndQuestionId", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@FormID", formId);
        command.Parameters.AddWithValue("@QuestionID", questionId);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return MapFromReader(reader);
        }
        return null;
    }

    public async Task<FormQuestion> CreateAsync(FormQuestion entity)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_FormQuestions_Create", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@FormID", (object?)entity.FormId ?? DBNull.Value);
        command.Parameters.AddWithValue("@QuestionID", (object?)entity.QuestionId ?? DBNull.Value);
        command.Parameters.AddWithValue("@SortOrder", (object?)entity.SortOrder ?? DBNull.Value);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        await reader.ReadAsync();
        return MapFromReader(reader);
    }

    public async Task<FormQuestion?> UpdateAsync(FormQuestion entity)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_FormQuestions_Update", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@FormID", (object?)entity.FormId ?? DBNull.Value);
        command.Parameters.AddWithValue("@QuestionID", (object?)entity.QuestionId ?? DBNull.Value);
        command.Parameters.AddWithValue("@SortOrder", (object?)entity.SortOrder ?? DBNull.Value);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return MapFromReader(reader);
        }
        return null;
    }

    public async Task<bool> DeleteAsync(int formId, int questionId)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_FormQuestions_Delete", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@FormID", formId);
        command.Parameters.AddWithValue("@QuestionID", questionId);

        await connection.OpenAsync();
        var rowsAffected = await command.ExecuteNonQueryAsync();
        return rowsAffected > 0;
    }

    private static FormQuestion MapFromReader(SqlDataReader reader)
    {
        return new FormQuestion
        {
            FormId = reader.IsDBNull(reader.GetOrdinal("FormID")) ? null : reader.GetInt32(reader.GetOrdinal("FormID")),
            QuestionId = reader.IsDBNull(reader.GetOrdinal("QuestionID")) ? null : reader.GetInt32(reader.GetOrdinal("QuestionID")),
            SortOrder = reader.IsDBNull(reader.GetOrdinal("SortOrder")) ? null : reader.GetInt32(reader.GetOrdinal("SortOrder"))
        };
    }
}
