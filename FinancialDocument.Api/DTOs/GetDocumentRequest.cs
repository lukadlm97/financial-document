namespace FinancialDocument.Api.DTOs;

public record GetDocumentRequest(string ProductCode, string TenantId, string DocumentId);
