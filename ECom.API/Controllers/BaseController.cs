using AutoMapper;
using ECom.Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IUnitofWork UnitofWork { get; }
        public IMapper Mapper { get; }

        public BaseController(IUnitofWork unitofWork , IMapper mapper)
        {
            UnitofWork = unitofWork;
            Mapper = mapper;
        }

      
    }
}
