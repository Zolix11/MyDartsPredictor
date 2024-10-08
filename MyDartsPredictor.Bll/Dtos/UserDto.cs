﻿namespace MyDartsPredictor.Bll.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string AzureAdB2CId { get; set; } = null!;

    public List<TournamentDto> Tournaments { get; set; } = new List<TournamentDto>();

    public List<PredictionDto> Predictions { get; set; } = new List<PredictionDto>();

    public List<PointDto> Points { get; set; } = new List<PointDto>();
}
