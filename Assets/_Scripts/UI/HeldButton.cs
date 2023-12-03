using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeldButton : Button, IPointerDownHandler, IPointerUpHandler
{
    public bool IsHeld { get; private set; }

    public override void OnPointerDown(PointerEventData eventData) 
    {
        IsHeld = true;
    }
    public override void OnPointerUp(PointerEventData eventData) 
    {
        IsHeld = false;
    }
}
