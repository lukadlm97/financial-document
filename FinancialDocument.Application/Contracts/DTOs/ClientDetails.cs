using FinancialDocument.Domain.Enums;

namespace FinancialDocument.Application.Contracts.DTOs;

public record ClientDetails(string Id, string Vat, string? RegistrationNumber, string? CompanyType, CompanySize? CompanySize) : ClientMain(Id, Vat);
