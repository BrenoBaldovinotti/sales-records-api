﻿using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Command to cancel a sale.
/// </summary>
public record CancelSaleCommand(Guid Id) : IRequest<CancelSaleResult>;
