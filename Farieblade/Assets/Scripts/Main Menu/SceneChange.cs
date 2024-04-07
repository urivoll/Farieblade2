using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int Scene;
    public void SetSceneAr(int Scene)
    {
        PlayerData.afterFight = Scene;
    }
    public void LoadScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }
}
