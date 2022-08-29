using Application.Contracts;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValueLogsController : Controller
    {
        private readonly IValueLogsRepository _vRepository;
        public ValueLogsController(IValueLogsRepository vRepository)
        {
            _vRepository = vRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(ValuesLogsCreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid) throw new Exception("Fileds don't cane be empty");
                var response = await _vRepository.CreateAsync(request);
                return Ok(response);
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditAsync(ValuesLogsUpdateRequest request)
        {
            try
            {
                if (!ModelState.IsValid) throw new Exception("Fileds don't cane be empty");
                var response = await _vRepository.EditAsync(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

