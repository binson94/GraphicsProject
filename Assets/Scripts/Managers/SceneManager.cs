/******
씬 전환 관리 클래스
******/

public class SceneManager
{
    /// <summary> enum을 이용한 씬 전환 </summary>
    public static void LoadScene(Define.Scene scene)
    {
        GameManager.Instance.Clear();
        UnityEngine.SceneManagement.SceneManager.LoadScene((int)scene);
    }
}
