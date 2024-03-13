using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration
{
    public interface ICheckpointService
    {
        Result<PagedResult<CheckpointDto>> GetPaged(int page, int pageSize);
        Result<CheckpointDto> Create(CheckpointDto checkpoint, int userId);
        Result<CheckpointDto> Update(CheckpointDto checkpoint, int userId);
        Result Delete(int id, int userId);
        Result<PagedResult<CheckpointDto>> GetPagedByTour(int page, int pageSize, int id);
        Result<CheckpointDto> Create(CheckpointDto checkpoint, int userId, string status);
        Result<CheckpointDto> Get(int id);
        Result<CheckpointDto> CreateChechpointSecreat(CheckpointSecretDto secret, int id, int userId);
        Result<CheckpointDto> UpdateCheckpointSecret(CheckpointSecretDto secret, int id, int userId);

    }
}
