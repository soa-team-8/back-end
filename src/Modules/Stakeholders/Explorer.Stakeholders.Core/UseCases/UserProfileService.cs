﻿using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserProfileService : ISocialProfileService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISocialProfileRepository _socialProfileRepository;

        public UserProfileService(IUserRepository userRepository, ISocialProfileRepository socialProfileRepository)
        {
            _userRepository = userRepository;
            _socialProfileRepository = socialProfileRepository;
        }

        public Result<SocialProfileDto> Follow(int followerId, int followedId)
        {
            if(followerId == followedId) { throw new InvalidOperationException("You cannot follow yourself."); }

            var socialProfile = _socialProfileRepository.Get(followerId);
            var followedUser = _userRepository.GetUserById(followedId);

            socialProfile.Follow(followedUser);

            var result = _socialProfileRepository.Update(socialProfile);

            return null;
        }

        public Result<SocialProfileDto> Get(int userId)
        {
            //var user = _userRepository.GetUserById(userId);
            //return _mapper.Map<SocialProfileDto>(user);

            var socialProfile = _socialProfileRepository.Get(userId);

            return null;
        }
    }
}
