using Backend.Api.Data;
using Backend.Api.DTOs;
using Backend.Api.Models;
using Microsoft.AspNetCore.Mvc;


namespace Backend.Api.Controllers;

[ApiController]
[Route("api/v1/products")]

public sealed class ProductController : ControllerBase
{
    private readonly InMemoryProductCatalog _catalog;

    public ProductController(InMemoryProductCatalog catalog)
    {
        _catalog=catalog;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ProductResponse>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<Product>> GetAll()
    {
        var products = _catalog.GetAll().Select(ProductResponse.FromModel).ToArray();

        return Ok(products);
    }

    // GET Product by productCode
    [HttpGet("{productCode}")]
    [ProducesResponseType(typeof(ProductResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status404NotFound)]
    public ActionResult<ProductResponse> GetByCode(
        [FromRoute] string productCode
    )
    {
        var product = _catalog.GetByCode(productCode);

        if(product is null)
        {
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: "Product not Found",
                detail: $"Product with code ({productCode}) not found"
            );
        }

        return Ok(ProductResponse.FromModel(product));
    }


    [HttpPost]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ProductResponse> PostProduct([FromBody] CreateProduct productDto)
    {
        var newProduct = new Product(productDto.ProductCode,productDto.Name,productDto.Price,productDto.Stock,productDto.Category);

        try
        {
            _catalog.Add(newProduct);
        }
        catch (ArgumentException e)
        {
            return BadRequest(new {message=e.Message});
        }
        
        return CreatedAtAction(
            actionName: nameof(GetByCode),
            routeValues: new {productCode = newProduct.ProductCode},
            value: ProductResponse.FromModel(newProduct)
        );

    }

    // TODO: Update
    
    [HttpPut("productCode")]
    [ProducesResponseType(typeof(ProductResponse),StatusCodes.Status200OK)]
    public ActionResult<ProductResponse> UpdateProduct([FromBody] Product request, [FromRoute] string productCode)
    {
        
    }


}