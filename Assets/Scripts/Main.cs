using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Main : MonoBehaviour
{
    [SerializeField] private int _idSceneDesktop;
    [SerializeField] private int _idSceneMobile;

    private void Start()
    {
        LoadSceneSelectLevel();
    }

    public  void LoadSceneSelectLevel()
    {
        string platform = YandexGame.EnvironmentData.deviceType;
        int idScene = _idSceneDesktop;

        if (YandexGame.EnvironmentData.isMobile)
            idScene = _idSceneMobile;

        SceneManager.LoadScene(idScene);
    }
}
