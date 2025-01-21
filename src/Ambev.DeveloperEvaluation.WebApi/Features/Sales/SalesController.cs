using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;
using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using Ambev.DeveloperEvaluation.Common.Models;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

/// <summary>
/// Controller for managing sale operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of SaleController.
    /// </summary>
    /// <param name="mediator">The mediator instance.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public SalesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new sale.
    /// </summary>
    /// <param name="request">The sale creation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created sale details.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithDataModel<CreateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponseModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        // Validate the request
        var validator = new CreateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        // Map request to command
        var command = _mapper.Map<CreateSaleCommand>(request);

        // Handle the command
        var result = await _mediator.Send(command, cancellationToken);

        // Map result to response
        var response = _mapper.Map<CreateSaleResponse>(result);

        return Created(string.Empty, new ApiResponseWithDataModel<CreateSaleResponse>
        {
            Success = true,
            Message = "Sale created successfully.",
            Data = response
        });
    }

    /// <summary>
    /// Retrieves a sale by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the sale.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The sale details if found.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithDataModel<GetSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseModel), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSaleById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<GetSaleQuery>(id);

        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithDataModel<GetSaleResponse>
        {
            Success = true,
            Message = "Sale retrieved successfully.",
            Data = _mapper.Map<GetSaleResponse>(response)
        });
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithDataModel<PaginatedListModel<ListSalesResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseModel), StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<IActionResult> ListSale([FromQuery] ListSalesRequest request, CancellationToken cancellationToken)
    {
        // Map API request to application query
        var query = _mapper.Map<ListSalesQuery>(request);

        // Send the query to the application layer
        var result = await _mediator.Send(query, cancellationToken);

        // Map application result to API response
        var response = _mapper.Map<ListSalesResponse>(result);

        return Ok(new ApiResponseWithDataModel<ListSalesResponse>
        {
            Success = true,
            Message = "Sales retrieved successfully",
            Data = response
        });
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithDataModel<UpdateSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseModel), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSale([FromRoute] Guid id, [FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        request.Id = id;
        var command = _mapper.Map<UpdateSaleCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithDataModel<UpdateSaleResponse>
        {
            Success = true,
            Message = "Sale updated successfully.",
            Data = _mapper.Map<UpdateSaleResponse>(result)
        });
    }
}
