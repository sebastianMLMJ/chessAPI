namespace chessAPI.models.player;

public sealed class clsNewGame
{
    public clsNewGame()
    {
        firstplayerscore = 0;
        secondplayerscore = 0;
        
    }
    public int? firstplayerscore {get; set;}
    public int? secondplayerscore {get; set;}
    public int? id_firstplayer {get; set;}
    public int? id_secondplayer {get; set;}
}