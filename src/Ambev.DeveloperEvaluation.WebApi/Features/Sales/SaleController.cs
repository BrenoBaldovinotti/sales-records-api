using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

/// <summary>
/// Controller for managing sale operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SaleController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of SaleController.
    /// </summary>
    /// <param name="mediator">The mediator instance.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public SaleController(IMediator mediator, IMapper mapper)
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
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
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

        return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
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
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSaleById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<GetSaleQuery>(id);

        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<GetSaleResponse>
        {
            Success = true,
            Message = "Sale retrieved successfully.",
            Data = _mapper.Map<GetSaleResponse>(response)
        });
    }
}
