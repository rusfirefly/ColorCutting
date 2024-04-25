using UnityEngine;
using UnityEngine.UI;

public class HudHandler : MonoBehaviour
{
    [SerializeField] private GameObject _complete;
    [SerializeField] private Text _levelName;
    [SerializeField] private Text _cutText;

    public void SetLavelNunber(int levelNumvber) => _levelName.text = $"Level {levelNumvber}";

    public void ShowCompleteLevel()
    {
        _complete.gameObject.SetActive(true);
    }

    public void SetCutText(int countCut) => _cutText.text = $"{countCut}";
}
