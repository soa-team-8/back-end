﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class MapObjectService : CrudService<MapObjectDto, MapObject>, IMapObjectService
    {
        private readonly IInternalObjectRequestService _internalObjectRequestService;
        public MapObjectService(ICrudRepository<MapObject> repository, IMapper mapper, IInternalObjectRequestService internalObjectRequestService) : base(repository, mapper)
        {
            _internalObjectRequestService = internalObjectRequestService;
        }

        public Result<MapObjectDto> Create(MapObjectDto mapObject, int userId, string status)
        {
            var result = Create(mapObject);
            if (status.Equals("Public"))
            {
                _internalObjectRequestService.Create(result.Value.Id, userId, "OnHold");
            }
            return result;
        }
    }
}
