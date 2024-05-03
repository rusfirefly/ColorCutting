using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YandexSDK : MonoBehaviour
{
    public static YandexSDK Instance;
    public static LevelData LevelDataLoad { get; private set; }
    
    [DllImport("__Internal")]
    private static extern void GetPlatform();

    [DllImport("__Internal")]
    private static extern void SaveData(string data);

    [DllImport("__Internal")]
    public static extern void LoadData();

    private void Awake()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
       GetPlatform();
    }

    public static void SaveData(LevelData levelData)
    {
        string json = JsonUtility.ToJson(levelData);
        SaveData(json);
    }

    public static void LoadData(string json)
    {
        LevelDataLoad = JsonUtility.FromJson<LevelData>(json);
    }


    public void SetPlayerName(string name)
    {
        Debug.Log(name);
    }

    public void SetPlatform(string platform)
    {
        if (platform == "mobile")
        {
            SceneManager.LoadScene(15);        
        }else
        {
            SceneManager.LoadScene(14);
        }
    }
}
