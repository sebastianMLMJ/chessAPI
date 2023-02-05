namespace chessAPI.models.player;

public sealed class clsNewGame
{
    public clsNewGame()
    {
        started = DateTime.Now;
        whites = 16;
        blacks = 16;
        turn = true;
        winner = 0;

    }
    public int? id { get; set; }   
    public DateTime? started { get; set; }
    public int? whites {get; set;}
    public int? blacks {get; set;}
    public bool? turn {get; set;}
    public int? winner { get; set; }

}