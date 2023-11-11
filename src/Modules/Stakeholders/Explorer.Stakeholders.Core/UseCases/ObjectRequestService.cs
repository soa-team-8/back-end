﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ObjectRequestService : CrudService<ObjectRequestDto,ObjectRequest>, IObjectRequestService, IInternalObjectRequestService
    {
        private readonly IObjectRequestRepository _objectRequestRepository;
        private readonly INotificationRepository _notificationRepository;

        public ObjectRequestService(ICrudRepository<ObjectRequest> repository, IMapper mapper, IObjectRequestRepository objectRequestRepository, INotificationRepository notificationRepository) : base(repository, mapper)
        {
            _objectRequestRepository = objectRequestRepository;
            _notificationRepository = notificationRepository;
        }

        public Result<ObjectRequestDto> AcceptRequest(int id, string notificationComment)
        {
            var request = CrudRepository.Get(id);
            Notification notification = new Notification(notificationComment, request.AuthorId, id);
            _notificationRepository.AddNotification(notification);
            var objectRequest = _objectRequestRepository.AcceptRequest(id);
            return MapToDto(objectRequest);
        }

        public Result<List<ObjectRequestDto>> GetAll()
        {
            var objectRequests = CrudRepository.GetPaged(0, 0).Results.ToList();
            return MapToDto(objectRequests);
        }

        public Result<ObjectRequestDto> RejectRequest(int id, string notificationComment)
        {
            var request = CrudRepository.Get(id);
            Notification notification = new Notification(notificationComment, request.AuthorId, id);
            _notificationRepository.AddNotification(notification);
            var objectRequest = _objectRequestRepository.RejectRequest(id);
            return MapToDto(objectRequest);
        }
    }
}
