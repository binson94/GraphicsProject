/******
각종 enum 정의
******/

public class Define
{
    /// <summary> 현재 게임 상태 </summary>
    public enum GameState
    {
        Play,
        Pause, 
        Win,
        Lose,
    }

    public enum Scene
    {
        Title,
        Ingame,
    }

    public enum Sound
    {
        SFX,
        BGM,
        MaxCount
    }

    public enum SFX
    {
        MaxCount
    }
    public enum BGM 
    {
        MaxCount
    }


}
