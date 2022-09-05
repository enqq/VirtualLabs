using System;
using Application.Contracts;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using static Domain.Entities.Enums;

namespace Infrastructure.Repository
{
    public class UserGroupsRepository: IUserGroupsRepository
    {
        private readonly IUserGroupsManager<UserGroup> _userGroupsManager;
        private readonly IUserManager<User> _userManager;
        private readonly IMapper _mapper;
        public UserGroupsRepository(IUserGroupsManager<UserGroup> userGroupManager, IUserManager<User> userManager, IMapper mapper)
        {
            _userGroupsManager = userGroupManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<UserGroupsResponse>> GetGroups()
        {
            var groups = _userGroupsManager.Get();
            var mapper = _mapper.Map<List<UserGroupsResponse>>(groups);
            return await Task.FromResult(mapper);
        }

        public async Task<UserGroupsResponse> GetGroupById(int id)
        {
            var group = await _userGroupsManager.GetByIdAsync(id);
            var mapper = _mapper.Map<UserGroupsResponse>(group);
            return await Task.FromResult(mapper);
        }

        public async Task<UserResponse> GetUsersAsync(int id)
        {
            var group = await _userGroupsManager.GetByIdAsync(id);
            var user = group.Users;
            return _mapper.Map<UserResponse>(user);
        }

        public async Task<UserGroupsResponse> InserUserToGroup(UserGroupsUserUpdateRequest request)
        {
            var user = await _userManager.GetByIdAsync(request.UserId);
            if (user == null) throw new Exception("Can't find user");
            var group = await _userGroupsManager.GetByIdAsync(request.Id);
            if (group == null) throw new Exception("Can't find user group");
            var response = await _userGroupsManager.InsertUser(group.ID, user);
            return _mapper.Map<UserGroupsResponse>(response);
        }

        public async Task<UserGroupsResponse> RemoveUserToGroup(UserGroupsUserUpdateRequest request)
        {
            var user = await _userManager.GetByIdAsync(request.UserId);
            if (user == null) throw new Exception("Can't find user");
            var group = await _userGroupsManager.GetByIdAsync(request.Id);
            if (group == null) throw new Exception("Can't find user group");
            var response = await _userGroupsManager.RemoveUser(group.ID, user);
            return _mapper.Map<UserGroupsResponse>(response);
        }

        public async Task<UserGroupsResponse> UpdateName(UserGroupsUpdateRequest request)
        {
            var userAccess = new UserRoles[] {
                UserRoles.admin,
                UserRoles.teacher
            };

            if (!_userGroupsManager.CheckPersmission(userAccess)) throw new Exception("User deosn't have persmission");
            UserGroup userGroup = await _userGroupsManager.GetByIdAsync(request.Id);
            if (userGroup == null) throw new Exception("Group doesn't exist");
            userGroup.Name = request.Name;
            userGroup.Modified = DateTime.Now;
            var update = await _userGroupsManager.Update(userGroup);
            return _mapper.Map<UserGroupsResponse>(update);
        }

        public async Task<UserGroupsResponse> Create(UserGroupsRequest request)
        {
            var group = new UserGroup()
            {
                Name = request.Name,
                Created = DateTime.Now
            };

            await _userGroupsManager.Create(group);
            var mapper = _mapper.Map<UserGroupsResponse>(group);
            return await Task.FromResult(mapper);

        }
    }
}

