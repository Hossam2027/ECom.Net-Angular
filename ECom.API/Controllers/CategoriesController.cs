using AutoMapper;
using ECom.Core.DTO;
using ECom.Core.Entites.Product;
using ECom.Core.Interface;
using ECom.infrastrucrure.Repositries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECom.API.Controllers
{
    public class CategoriesController : BaseController
    {
        public CategoriesController(Core.Interface.IUnitofWork unitofWork, IMapper mapper) : base(unitofWork, mapper)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await UnitofWork.CategoryRepository.GetAllAsync();
            if (categories is null)
                return BadRequest();
            return Ok(categories);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
          
                var category = await UnitofWork.CategoryRepository.GetByIdAsync(id);
                if (category is null)
                    return NotFound();    
                return Ok(category);      
           
        }

        [HttpPost]
        public async Task<IActionResult> addcategoryAsync(CategoryDTO categorydto)
        {
            if (categorydto == null)
                return BadRequest("Invalid data");
            var category = Mapper.Map<Category>(categorydto);
           
            await UnitofWork.CategoryRepository.AddAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPut]
        public async Task<IActionResult> Updatecategory(int id ,CategoryToUpdateDTO categoryToUpdateDTO)
        {
            var category = await UnitofWork.CategoryRepository.GetByIdAsync(id);

           Mapper.Map(categoryToUpdateDTO, category);

            await UnitofWork.CategoryRepository.UpdateAsync(category);
            return Ok("updated");
        }
        [HttpDelete]
        public async Task<IActionResult> deleteAsync(int id )

        {
            var category = await UnitofWork.CategoryRepository.GetByIdAsync(id);
           await UnitofWork.CategoryRepository.DeleteAsync(id);
            return Ok("deleted");
        }
    }
}
