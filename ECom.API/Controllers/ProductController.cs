using AutoMapper;
using ECom.Core.DTO;
using ECom.Core.Entites.Product;
using ECom.Core.Interface;
using ECom.Core.Services;
using ECom.Core.Sharing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace ECom.API.Controllers
{
   
    public class ProductController : BaseController
    {
        public IImageManegmentService ImageService { get; }

        public ProductController(IUnitofWork unitofWork, IMapper mapper, IImageManegmentService imageService) : base(unitofWork, mapper)
        {
            ImageService = imageService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> Get([FromQuery]ProductParams productParams)
        {
            var products = await UnitofWork.ProductRepository.GetAllAsync(productParams);

            //var result = Mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var Product = await UnitofWork.ProductRepository.GetByIdAsync(id, p => p.Category,
                p => p.Photos);
            if (Product == null)
                return NotFound($"Product with id {id} not found.");
            var result = Mapper.Map<ProductDTO>(Product);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] addProductDTO productDTO)
        {

            var images = await ImageService.AddImageAsync(productDTO.Images, "wwwroot/images/products");

            var product = new Product
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                NewPrice = productDTO.NewPrice,
                OldPrice = productDTO.OldPrice,
                CategoryId = productDTO.CategoryId,
                Photos = images.Select(image => new Photo { ImageName = image }).ToList()
            };
            await UnitofWork.ProductRepository.AddAsync(product);

            var productWithCategory = await UnitofWork.ProductRepository.GetByIdAsync(product.Id,
                p => p.Category, p => p.Photos);

            var productDTOResponse = Mapper.Map<ProductDTO>(productWithCategory);

            return Ok(productDTOResponse);



        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] addProductDTO addProductDTO)
        {
            var product = await UnitofWork.ProductRepository
      .GetByIdAsync(id, p => p.Category, p => p.Photos);

            product.Name = addProductDTO.Name;
            product.Description = addProductDTO.Description;
            product.NewPrice = addProductDTO.NewPrice;
            product.OldPrice = addProductDTO.OldPrice;
            product.CategoryId = addProductDTO.CategoryId;

            //foreach (var photo in product.Photos)
            //{
            //    await ImageService.DeleteImageAsync(photo.ImageName);
            //}
            if (addProductDTO.Images != null && addProductDTO.Images.Count > 0)
            {
                foreach (var photo in product.Photos)
                {
                    await ImageService.DeleteImageAsync(photo.ImageName);
                }

               
            }

            var imageNames = await ImageService.AddImageAsync(addProductDTO.Images, "wwwroot/images/products");
            product.Photos = imageNames.Select(img => new Photo { ImageName = img }).ToList();
            await UnitofWork.ProductRepository.UpdateAsync(product);

            var updatedProduct = await UnitofWork.ProductRepository
       .GetByIdAsync(product.Id, p => p.Category, p => p.Photos);

            var productDTOResponse = Mapper.Map<ProductDTO>(updatedProduct);
            return Ok(productDTOResponse);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await UnitofWork.ProductRepository.GetByIdAsync(id , x=>x.Category , x=>x.Photos);
            if (product == null)
                return NotFound($"Product with id {id} not found.");
            foreach (var photo in product.Photos)
            {
              await  ImageService.DeleteImageAsync(photo.ImageName);
            }
           await UnitofWork.ProductRepository.DeleteAsync(id);
            return Ok("deleted");
        }
    }
}
