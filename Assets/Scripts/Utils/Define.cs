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

    /// <summary> 씬 종류 </summary>
    public enum Scene
    {
        Title,
        Ingame,
    }

    /// <summary> 효과음 </summary>
    public enum SFX
    {
        MonsterFound
    }
}
