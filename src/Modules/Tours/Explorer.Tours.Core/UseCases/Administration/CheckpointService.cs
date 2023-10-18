﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class CheckpointService : CrudService<CheckpointDto, Checkpoint>, ICheckpointService
    {
        public CheckpointService(ICrudRepository<Checkpoint> repository, IMapper mapper) : base(repository, mapper) { }
        public Result<List<CheckpointDto>> GetPagedByTour(int page, int pageSize, int id)
        {
            var allTours = CrudRepository.GetPaged(page, pageSize);
            List<Checkpoint> checkpoints = allTours.Results.Where(n => n.TourID == id).ToList();
            return MapToDto(checkpoints);
        }
    }
}
