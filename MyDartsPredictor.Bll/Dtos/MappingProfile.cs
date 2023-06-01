using AutoMapper;
using MyDartsPredictor.Dal.Entities;

namespace MyDartsPredictor.Bll.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Users, UserDto>()
                  .ForMember(dest => dest.Predictions, opt => opt.MapFrom(src => src.Predictions))
                  .ForMember(dest => dest.Tournaments, opt => opt.MapFrom(src => src.UsersInTournaments.Select(uit => uit.Tournament)
             ));

            CreateMap<Tournament, TournamentDto>()
              .ForMember(dest => dest.FounderUser, opt => opt.MapFrom(src => src.FounderUser))
              .ForMember(dest => dest.Games, opt => opt.MapFrom(src => src.Games))
              .ForMember(dest => dest.UsersWithPoints, opt => opt.MapFrom(src => src.UsersInTournament
                  .Where(uit => uit.TournamentId == src.Id)
                  .Select(uit => new UserWithPointsDto
                  {
                      UserId = uit.UserId,
                      UserName = uit.User.Name,
                      EarnedPoints = uit.EarnedPoints
                  }).ToList()));


            CreateMap<UserWithPointsDto, UsersInTournament>()
                 .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                 .ForMember(dest => dest.EarnedPoints, opt => opt.MapFrom(src => src.EarnedPoints))
                 .ReverseMap();

            CreateMap<Games, GameDto>()
               .ForMember(dest => dest.Player2Name, opt => opt.MapFrom(src => src.Player2Name))
               .ForMember(dest => dest.Player1Name, opt => opt.MapFrom(src => src.Player1Name))
               .ForMember(dest => dest.MatchDate, opt => opt.MapFrom(src => src.MatchDate))
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ReverseMap();


            CreateMap<Prediction, PredictionDto>()
              .ForMember(dest => dest.Game, opt => opt.MapFrom(src => src.Game))
              .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
              .ForMember(dest => dest.PredictionWinner, opt => opt.MapFrom(src => src.PredictionWinner))
              .ForMember(dest => dest.PredictionScore, opt => opt.MapFrom(src => src.PredictionScore))
              .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.GameId))
              .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
              .ReverseMap();


            CreateMap<Result, ResultDto>()
              .ForMember(dest => dest.WinnerPlayer, opt => opt.MapFrom(src => src.WinnerPlayer))
              .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Score))
              .ReverseMap();
            ;

        }
    }
}
