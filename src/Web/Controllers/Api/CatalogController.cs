using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.Services;
using BlazorShared.ViewModels;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService) => _catalogService = catalogService;

        [HttpGet]
        public async Task<ActionResult<CatalogIndexViewModel>> GetCatalogItems(int? brandFilterApplied, int? typesFilterApplied, int? page)
        {
            var itemsPage = 10;
            var catalogModel = await _catalogService.GetCatalogItems(page ?? 0, itemsPage, brandFilterApplied, typesFilterApplied);
            return Ok(catalogModel);
        }

        [HttpGet("brands")]
        public async Task<ActionResult> GetBrands()
        {
            var brands = await _catalogService.GetBrands();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult> GetTypes()
        {
            var types = await _catalogService.GetTypes();
            return Ok(types);
        }
    }
}