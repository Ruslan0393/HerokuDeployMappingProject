using Mapping.Domain.Common;
using Mapping.Domain.Entities;
using Mapping.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mapping.Service.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<User>> GetAsync(Expression<Func<User, bool>> expression);

        Task<BaseResponse<IEnumerable<User>>> GetAllAsync(Expression<Func<User, bool>> expression = null);

        Task<BaseResponse<User>> UpdateAsync(User user);

        Task<BaseResponse<User>> CreateAsync(UserCreateViewModel user);

        Task<bool> DeleteAsync(Expression<Func<User, bool>> expression);
    }
}
