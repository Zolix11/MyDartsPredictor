﻿namespace MyDartsPredictor.Dal.Entities;

public class Prediction
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int UserId { get; set; }
    public int PredictionWinner { get; set; }
    public int PredictionScore { get; set; }

    // Navigation properties
    public Games Game { get; set; } = null!;
    public Users User { get; set; } = null!;
}
