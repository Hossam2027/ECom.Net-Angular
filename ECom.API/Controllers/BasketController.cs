using AutoMapper;
using ECom.Core.Entites;
using ECom.Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECom.API.Controllers
{
    public class BasketController : BaseController
    {
        public BasketController(IUnitofWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBasket(string id)
        {
            var basket = await UnitofWork.CustomerBasketRepository.GetBasketAsync(id);
            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateBasket([FromBody] customerbasket basket)
        {
            var updatebasket = await UnitofWork.CustomerBasketRepository.updatebasketAsync(basket);
            return Ok(updatebasket);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBasket(string id)
        {
            var deletebasket = await UnitofWork.CustomerBasketRepository.deletebasketAsync(id);
            return Ok(deletebasket);
        }
    }


}
