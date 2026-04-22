namespace EPMS.Application.Interfaces;

public interface IExcelPdfService
{
    // Excel Export
    Task<byte[]> ExportAppraisalCyclesToExcelAsync();
    Task<byte[]> ExportAppraisalQuestionsToExcelAsync();
    Task<byte[]> ExportAppraisalResponsesToExcelAsync();
    Task<byte[]> ExportAppraisalFormsToExcelAsync();
    Task<byte[]> ExportFormQuestionsToExcelAsync();
    Task<byte[]> ExportPerformanceEvaluationsToExcelAsync();
    Task<byte[]> ExportPerformanceOutcomesToExcelAsync();

    // Excel Import
    Task<int> ImportAppraisalCyclesFromExcelAsync(Stream fileStream);
    Task<int> ImportAppraisalQuestionsFromExcelAsync(Stream fileStream);
    Task<int> ImportAppraisalResponsesFromExcelAsync(Stream fileStream);
    Task<int> ImportAppraisalFormsFromExcelAsync(Stream fileStream);
    Task<int> ImportFormQuestionsFromExcelAsync(Stream fileStream);
    Task<int> ImportPerformanceEvaluationsFromExcelAsync(Stream fileStream);
    Task<int> ImportPerformanceOutcomesFromExcelAsync(Stream fileStream);

    // PDF Export
    Task<byte[]> ExportAppraisalCyclesToPdfAsync();
    Task<byte[]> ExportAppraisalQuestionsToPdfAsync();
    Task<byte[]> ExportAppraisalResponsesToPdfAsync();
    Task<byte[]> ExportAppraisalFormsToPdfAsync();
    Task<byte[]> ExportFormQuestionsToPdfAsync();
    Task<byte[]> ExportPerformanceEvaluationsToPdfAsync();
    Task<byte[]> ExportPerformanceOutcomesToPdfAsync();
}
