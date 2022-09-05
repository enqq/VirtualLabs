using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/log")]
    public class MeasurementLogsController : Controller
    {
        private readonly IMeasurementLogsRepository _mesLogsRepository;
        public MeasurementLogsController(IMeasurementLogsRepository mesLogsRepository)
        {
            _mesLogsRepository = mesLogsRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLogAsync([FromBody] MeasurementLogsCreateRequest request)
        {
            try
            {
                var response = await _mesLogsRepository.AddAsync(request);
                return Ok(response);

            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditLogAsync([FromBody] MeasurementLogsUpdateRequest request)
        {
            try
            {
                var response = await _mesLogsRepository.Edit(request);
                return Ok(response);

            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetByUser()
        {
            try
            {
                var response = await _mesLogsRepository.Get();
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var response = await _mesLogsRepository.GetByIdAsync(id);
                return Ok(response);
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id:int}/{idValue:int}")]
        public async Task<IActionResult> GetValueById(int id, int idValue)
        {
            try
            {
                var response = await _mesLogsRepository.GetValueById(id, idValue);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("value")]
        public async Task<IActionResult> GetValueByName(MeasurementLogsGetRequest request)
        {
            try
            {
                var response = await _mesLogsRepository.GetValueByNameAsync(request);
                return Ok(response);
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("insert/user")]
        public async Task<IActionResult> InserUser([FromBody] MeasurmentLogsSharedRequest request)
        {
            try
            {
                var response = await _mesLogsRepository.InsertUser(request);
                return Ok(response);
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("remove/user")]
        public async Task<IActionResult> RemoveUser([FromBody] MeasurmentLogsSharedRequest request)
        {
            try
            {
                var response = await _mesLogsRepository.RemoveUser(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("insert/group")]
        public async Task<IActionResult> InsertGroup([FromBody] MeasurmentLogsSharedRequest request)
        {
            try
            {
                var response = await _mesLogsRepository.InsertGroup(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("remove/group")]
        public async Task<IActionResult> RemoveGroup([FromBody] MeasurmentLogsSharedRequest request)
        {
            try
            {
                var response = await _mesLogsRepository.RemoveGroup(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

