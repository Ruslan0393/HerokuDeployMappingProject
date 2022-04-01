using AutoMapper;
using Mapping.Data.IRepositories;
using Mapping.Domain.Common;
using Mapping.Domain.Entities;
using Mapping.Service.Interfaces;
using Mapping.ViewModel.User;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mapping.Service.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private IConfiguration _configuration;
        private IWebHostEnvironment _env;

        public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration, IWebHostEnvironment env)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
            _env = env;
        }

        public async Task<BaseResponse<User>> CreateAsync(UserCreateViewModel model)
        {
            var resalt = new BaseResponse<User>();

            var user = _mapper.Map<User>(model);

            user.Image = await UploadFileAsync(model.Image.OpenReadStream(), model.Image.FileName);

            var retuenUser = await _userRepository.CreateAsync(user);

            if (retuenUser == null)
            {
                resalt.Error = new ErrorModel(404, "User can't create");
                return resalt;
            }

            resalt.Data = retuenUser;
            return resalt;
        }

        public async Task<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            return await _userRepository.DeleteAsync(expression);
        }

        public async Task<BaseResponse<IEnumerable<User>>> GetAllAsync(Expression<Func<User, bool>> expression = null)
        {
            var resalt = new BaseResponse<IEnumerable<User>>();
            var user = await _userRepository.GetAllAsync(expression);

            if (user == null)
            {
                resalt.Error = new ErrorModel(404, "Users don't found");
                return resalt;
            }

            resalt.Data = user;
            return resalt;
        }

        public async Task<BaseResponse<User>> GetAsync(Expression<Func<User, bool>> expression)
        {
            var resalt = new BaseResponse<User>();
            var user = await _userRepository.GetAsync(expression);

            if (user == null)
            {
                resalt.Error = new ErrorModel(404, "User doesn't found");
                return resalt;
            }

            resalt.Data = user;
            return resalt;
        }

        public async Task<BaseResponse<User>> UpdateAsync(User user)
        {
            var resalt = new BaseResponse<User>();
            var returnUser = await _userRepository.UpdateAsync(user);

            if (returnUser == null)
            {
                resalt.Error = new ErrorModel(404, "Something getting wrong");
                return resalt;
            }

            resalt.Data = returnUser;
            return resalt;
        }

        public async Task<string> UploadFileAsync(Stream file, string fileName)
        {
            fileName = Guid.NewGuid().ToString() + "_" + fileName;
            string storagePath = _configuration.GetSection("Storage:ImageUrl").Value;
            string filePath = Path.Combine(_env.WebRootPath, $"{storagePath}/{fileName}");
            FileStream newFile = File.Create(filePath);
            await file.CopyToAsync(newFile);
            newFile.Close();

            return fileName;
        }
    }
}
