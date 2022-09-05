using System;
using Application.Models;

namespace Application.Contracts
{
    public interface IUserGroupsRepository
    {
        Task<List<UserGroupsResponse>> GetGroups();
        Task<UserResponse> GetUsersAsync(int id);
        Task<UserGroupsResponse> GetGroupById(int id);
        Task<UserGroupsResponse> Create(UserGroupsRequest request);
        Task<UserGroupsResponse> UpdateName(UserGroupsUpdateRequest request);
        Task<UserGroupsResponse> InserUserToGroup(UserGroupsUserUpdateRequest request);
        Task<UserGroupsResponse> RemoveUserToGroup(UserGroupsUserUpdateRequest request);
    }
}

