using AutoMapper;
using Mapping.Data.IRepositories;
using Mapping.Domain.Common;
using Mapping.Domain.Entities;
using Mapping.Service.Interfaces;
using Mapping.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mapping.Service.Services
{
    public class UserService : IUserService
    {

        private IUserRepository _userRepository;
        private IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<User>> CreateAsync(UserCreateViewModel model)
        {
            var resalt = new BaseResponse<User>();

            var user = _mapper.Map<User>(model);

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
    }
}
