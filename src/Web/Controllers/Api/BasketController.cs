using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BlazorShared.ViewModels;
using ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<ActionResult<BasketViewModel>> GetBasket()
        {
            var basket = await _basketService.GetOrCreateBasketForUser(User.Identity.Name);
            return Ok(basket);
        }

        [HttpPost("items")]
        public async Task<ActionResult> AddItemToBasket([FromBody] AddItemRequest request)
        {
            await _basketService.AddItemToBasket(request.BasketId, request.CatalogItemId, request.Price, request.Quantity);
            return Ok();
        }

        [HttpPut("items")]
        public async Task<ActionResult> UpdateQuantities([FromBody] UpdateQuantitiesRequest request)
        {
            await _basketService.SetQuantities(request.BasketId, request.Quantities);
            return Ok();
        }

        [HttpDelete("{basketId}")]
        public async Task<ActionResult> DeleteBasket(int basketId)
        {
            await _basketService.DeleteBasketAsync(basketId);
            return Ok();
        }
    }

    public class AddItemRequest
    {
        public int BasketId { get; set; }
        public int CatalogItemId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateQuantitiesRequest
    {
        public int BasketId { get; set; }
        public Dictionary<string, int> Quantities { get; set; } = new();
    }
}