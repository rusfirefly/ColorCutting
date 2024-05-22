using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Image _hand1;

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Hide();
        }else
        {
            if(Input.GetMouseButtonDown(0))
            {
                Hide();
            }
        }
    }

    private void Hide()
    {
        _hand1.gameObject.SetActive(false);
    }

}
