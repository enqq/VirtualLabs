using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class LabsController : Controller
    {
        private readonly ILabRepository _labsRepository;
        private readonly IPositionRepository _positionRepository;
        public LabsController(ILabRepository labRepository, IPositionRepository positionRepository)
        {
            _labsRepository = labRepository;
            _positionRepository = positionRepository;
        }

        [HttpGet("labs")]
        public async Task<IActionResult> GetLabsAsync()
        {
            try
            {
                var result = await _labsRepository.GetLabs();
                return Ok(result);
            } catch( Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("labs")]
        public async Task<IActionResult> CreateLabAsync(LabRequest request)
        {
            try
            {
                var result = await _labsRepository.Create(request);
                return Ok(result);
            }  catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("labs")]
        public async Task<IActionResult> UpdateLabAsync(LabUpdateRequest request)
        {
            try
            {
                var result = await _labsRepository.Update(request);
                return Ok(result);
            } catch( Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("labs/position")]
        public async Task<IActionResult> InsertPosition(LabPositionRequest request)
        {
            try
            {
                var result = await _labsRepository.InsertPosition(request.Lab, request.Position);
                return Ok(result);
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("positions")]
        public async Task<IActionResult> GetPositions()
        {
            try
            {
                var result = await _positionRepository.Get();
                return Ok(result);
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("position")]
        public async Task<IActionResult> CreatePosition(PositionRequest request)
        {
            try
            {
                var result = await _positionRepository.Create(request);
                return Ok(result);
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            } 
        }

        [HttpPut("position")]
        public async Task<IActionResult> UpdatePosition(PositionUpdateRequest request)
        {
            try
            {
                var result = await _positionRepository.Update(request);
                return Ok(result);
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("position/{id}")]
        public IActionResult RemovePosition(int id)
        {
            try
            {
                 _positionRepository.Remove(id);
                return Ok();
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}

