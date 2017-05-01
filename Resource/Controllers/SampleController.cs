using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Resource.Models;

namespace Resource.Controllers
{
    [Route("samples")]
    [Authorize]
    public class SampleController : Controller
    {
        public IActionResult Get()
        {
            return Ok(new SampleResponse
            {
                Id = 1,
                Name = "Sample Name",
                Date = DateTime.Now
            });
        }
    }
}