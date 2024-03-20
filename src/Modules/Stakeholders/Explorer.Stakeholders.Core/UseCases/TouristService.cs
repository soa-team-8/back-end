using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class TouristService : BaseService<TouristDto, Tourist>, ITouristService
    {
        private readonly ITouristRepository _touristRepository;
        private readonly IMapper _mapper;
        public TouristService(ITouristRepository touristRepository, IMapper mapper) : base(mapper)
        {
            _touristRepository = touristRepository;
            _mapper = mapper;
        }

        public Result<TouristDto> GetTouristById(long touristId)
        {
            return MapToDto(_touristRepository.Get(touristId));
        }

        public Result<TouristDto> UpdateTouristXpAndLevel(long touristId, int encounterXp)
        {
            Tourist tourist = _touristRepository.GetTouristByUserId(touristId);
            tourist.UpdateXpAndLevel(encounterXp);
            var result = _touristRepository.Update(tourist);
            return MapToDto(result);
        }

    }
}
