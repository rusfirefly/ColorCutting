using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class YandexSDK : MonoBehaviour
{
    [SerializeField] private bool _isMain;
    [DllImport("__Internal")]
    private static extern void GetPlatform();


    private void Start()
    {
        if(_isMain)
            GetPlatform();
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
