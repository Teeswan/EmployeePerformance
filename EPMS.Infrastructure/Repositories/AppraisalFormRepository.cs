using System.Data;
using EPMS.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EPMS.Infrastructure.Repositories;

public class AppraisalFormRepository : IAppraisalFormRepository
{
    private readonly string _connectionString;

    public AppraisalFormRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task<IEnumerable<ApplicationForm>> GetAllAsync()
    {
        var list = new List<ApplicationForm>();
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_ApplicationForms_GetAll", connection)
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

    public async Task<ApplicationForm?> GetByIdAsync(int formId)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_ApplicationForms_GetById", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@FormID", formId);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return MapFromReader(reader);
        }
        return null;
    }

    public async Task<ApplicationForm> CreateAsync(ApplicationForm entity)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_ApplicationForms_Create", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@FormName", (object?)entity.FormName ?? DBNull.Value);
        command.Parameters.AddWithValue("@IsActive", (object?)entity.IsActive ?? DBNull.Value);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        await reader.ReadAsync();
        return MapFromReader(reader);
    }

    public async Task<ApplicationForm?> UpdateAsync(ApplicationForm entity)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_ApplicationForms_Update", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@FormID", entity.FormId);
        command.Parameters.AddWithValue("@FormName", (object?)entity.FormName ?? DBNull.Value);
        command.Parameters.AddWithValue("@IsActive", (object?)entity.IsActive ?? DBNull.Value);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return MapFromReader(reader);
        }
        return null;
    }

    public async Task<bool> DeleteAsync(int formId)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_ApplicationForms_Delete", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@FormID", formId);

        await connection.OpenAsync();
        var rowsAffected = await command.ExecuteNonQueryAsync();
        return rowsAffected > 0;
    }

    private static ApplicationForm MapFromReader(SqlDataReader reader)
    {
        return new ApplicationForm
        {
            FormId = reader.GetInt32(reader.GetOrdinal("FormID")),
            FormName = reader.IsDBNull(reader.GetOrdinal("FormName")) ? null : reader.GetString(reader.GetOrdinal("FormName")),
            IsActive = reader.IsDBNull(reader.GetOrdinal("IsActive")) ? null : reader.GetBoolean(reader.GetOrdinal("IsActive"))
        };
    }
}
