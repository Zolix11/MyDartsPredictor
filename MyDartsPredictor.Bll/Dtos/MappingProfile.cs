using AutoMapper;
using MyDartsPredictor.Dal.Entities;

namespace MyDartsPredictor.Bll.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Users, UserDto>()
                .ForMember(dest => dest.Tournaments, opt => opt.MapFrom(src => src.UsersInTournaments.Select(uit => uit.Tournament)))
                .ForMember(dest => dest.Predictions, opt => opt.MapFrom(src => src.Predictions))
                .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.Points));

            CreateMap<Users, TournamentDto>()
                .ForMember(dest => dest.FounderUser, opt => opt.MapFrom(src => src));

            CreateMap<Tournament, TournamentDto>()
                .ForMember(dest => dest.FounderUser, opt => opt.MapFrom(src => src.FounderUser))
                .ForMember(dest => dest.Games, opt => opt.MapFrom(src => src.Games))
                .ForMember(dest => dest.UsersWithPoints, opt => opt.MapFrom(src => src.UsersInTournament.Select(uit => new UserWithPointsDto
                {
                    UserId = uit.User.Id,
                    UserName = uit.User.Name,
                    EarnedPoints = uit.EarnedPoints
                })))
                .IncludeMembers(src => src.FounderUser);


            CreateMap<Games, GameDto>()
               .ForMember(dest => dest.Tournament, opt => opt.MapFrom(src => src.Tournament))
               .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.Result))
               .ForMember(dest => dest.Predictions, opt => opt.MapFrom(src => src.Predictions))
               .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.Points));

            CreateMap<Point, PointDto>()
               .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
               .ForMember(dest => dest.Game, opt => opt.MapFrom(src => src.Game))
               .ForMember(dest => dest.Prediction, opt => opt.MapFrom(src => src.Prediction))
               .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.Result));


            CreateMap<Prediction, PredictionDto>()
              .ForMember(dest => dest.Game, opt => opt.MapFrom(src => src.Game))
              .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

            CreateMap<Result, ResultDto>()
              .ForMember(dest => dest.Game, opt => opt.MapFrom(src => src.Game));

        }
    }
}
