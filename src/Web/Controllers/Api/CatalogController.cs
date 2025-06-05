using Microsoft.eShopWeb.Services;
using Microsoft.AspNetCore.Mvc;
using BlazorShared.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Microsoft.eShopWeb.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly ICatalogService _catalogService;

    public CatalogController(ICatalogService catalogService) => _catalogService = catalogService;

    [HttpGet]
    public async Task<ActionResult<CatalogIndexDto>> GetCatalogItems(
        int? brandFilterApplied = null, 
        int? typesFilterApplied = null, 
        int? page = null)
    {
        var itemsPage = 10;
        var catalogModel = await _catalogService.GetCatalogItems(page ?? 0, itemsPage, brandFilterApplied, typesFilterApplied);
        
        // Convert to DTO
        var dto = new CatalogIndexDto
        {
            CatalogItems = catalogModel.CatalogItems.Select(item => new CatalogItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Name, // Use name as description since Description is not available
                Price = item.Price,
                PictureUri = item.PictureUri,
                CatalogTypeId = 0, // Not available in ViewModel
                CatalogBrandId = 0, // Not available in ViewModel  
                CatalogTypeName = null,
                CatalogBrandName = null
            }),
            Brands = catalogModel.Brands.Select(b => new SelectListItemDto
            {
                Value = b.Value,
                Text = b.Text,
                Selected = b.Selected
            }),
            Types = catalogModel.Types.Select(t => new SelectListItemDto
            {
                Value = t.Value,
                Text = t.Text,
                Selected = t.Selected
            }),
            BrandFilterApplied = catalogModel.BrandFilterApplied,
            TypesFilterApplied = catalogModel.TypesFilterApplied,
            PaginationInfo = new PaginationInfoDto
            {
                ActualPage = catalogModel.PaginationInfo.ActualPage,
                ItemsPerPage = catalogModel.PaginationInfo.ItemsPerPage,
                TotalItems = catalogModel.PaginationInfo.TotalItems,
                TotalPages = catalogModel.PaginationInfo.TotalPages,
                Previous = catalogModel.PaginationInfo.Previous,
                Next = catalogModel.PaginationInfo.Next
            }
        };

        return Ok(dto);
    }
}