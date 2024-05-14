using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Main : MonoBehaviour
{
    [SerializeField] private int _idSceneDesktop;
    [SerializeField] private int _idSceneMobile;
    [field:SerializeField] public int MaxLevels { get; private set; }

    public static Main Instance;
    
    public int IdLoadScene { get; private set; }

    public string Platform { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            gameObject.name = "Main";
            Platform = YandexGame.EnvironmentData.deviceType;

            GetIdScene();
            LoadSceneSelectLevel();
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }

        
    }

    private int GetIdScene()
    {
        IdLoadScene = _idSceneDesktop;
        if (YandexGame.EnvironmentData.isMobile)
            IdLoadScene = _idSceneMobile;

        return IdLoadScene;
    }

    public void LoadSceneSelectLevel()
    {
        SceneManager.LoadScene(IdLoadScene);
    }


}
