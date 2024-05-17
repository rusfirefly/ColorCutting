using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeSeason : MonoBehaviour, IEndDragHandler
{
    [SerializeField] private SelectLevelHandler _selectLevelHandler;
    private float _dragThreshould;

    private void Start()
    {
        _dragThreshould = Screen.width / 15;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > _dragThreshould)
        {
            if (eventData.position.x > eventData.pressPosition.x)
                _selectLevelHandler.PreviwSeason();
            else
                _selectLevelHandler.NextSason();
        }
    }
}
