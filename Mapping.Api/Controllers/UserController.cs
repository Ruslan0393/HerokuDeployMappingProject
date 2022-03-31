using Mapping.Domain.Entities;
using Mapping.Service.Interfaces;
using Mapping.ViewModel.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Mapping.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _userService.GetAsync(p => p.Id.Equals(id));

            return result.Error?.Code == 404 ? NotFound(result) : Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllAsync();

            return result.Error?.Code == 404 ? NotFound(result) : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]UserCreateViewModel model)
        {
            var result = await _userService.CreateAsync(model);

            return result.Error?.Code == 404 ? NotFound(result) : Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(User model)
        {
            var result = await _userService.UpdateAsync(model);

            return result.Error?.Code == 404 ? NotFound(result) : Ok(result);
        }


        [HttpDelete]
        public async Task<bool> Delete(Guid id)
        {
            bool resalt = await _userService.DeleteAsync(p => p.Id == id);
            return resalt;
        }
    }
}
