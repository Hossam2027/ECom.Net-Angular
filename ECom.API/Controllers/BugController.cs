using AutoMapper;
using ECom.Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECom.API.Controllers
{
  
    public class BugController : BaseController
    {
        public BugController(IUnitofWork unitofWork, IMapper mapper) : base(unitofWork, mapper)
        {
        }
    }
}
