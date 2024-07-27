using FinancialDocument.Domain.Enums;

namespace FinancialDocument.Application.Contracts.DTOs;

public record PropertySettings(PropertyRepresentationType Type, string Name);
