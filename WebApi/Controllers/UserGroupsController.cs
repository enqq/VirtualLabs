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
    [Route("api/usergroups")]
    public class UserGroupsController : Controller
    {
        private readonly IUserGroupsRepository _userGroupsRepository;
        public UserGroupsController(IUserGroupsRepository userGroupsRepository)
        {
            _userGroupsRepository = userGroupsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            try
            {
                var response = await _userGroupsRepository.GetGroups();
                return Ok(response);

            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserGroupsRequest request)
        {
            try
            {
                var response = await _userGroupsRepository.Create(request);
                return Ok(response);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserGroupsUpdateRequest request)
        {
            try
            {
                var response = await _userGroupsRepository.UpdateName(request);
                return Ok(response);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var response = await _userGroupsRepository.GetGroupById(id);
                return Ok(response);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id:int}/users")]
        public async Task<IActionResult> GetUsersGrup(int id)
        {
            try
            {
                var response = await _userGroupsRepository.GetUsersAsync(id);
                return Ok(response);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("insert/user/")]
        public async Task<IActionResult> InsertUserToGroup([FromBody]UserGroupsUserUpdateRequest request)
        {
            try
            {
                var response = await _userGroupsRepository.InserUserToGroup(request);
                return Ok(response);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("remove/user/")]
        public async Task<IActionResult> RemoveUserToGroup([FromBody] UserGroupsUserUpdateRequest request)
        {
            try
            {
                var response = await _userGroupsRepository.RemoveUserToGroup(request);
                return Ok(response);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

