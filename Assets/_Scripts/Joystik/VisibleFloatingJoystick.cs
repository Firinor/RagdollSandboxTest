using UnityEngine;
using UnityEngine.EventSystems;

public class VisibleFloatingJoystick : Joystick
{
    [SerializeField]
    private Vector2 defaultPosition;

    protected override void Start()
    {
        defaultPosition = background.anchoredPosition;
        base.Start();

        //There is bug in ScreenPointToAnchoredPosition(eventData.position),
        //Method RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, screenPosition, cam, out localPoint)
        //Returns the value of localPoint incorrectly on the first call.
        //On the second call, everything starts working correctly. I didn't figure out how to fix this error, so I made the first call here.
        SomeFirstCallDebug();
    }
    #region BadCode
    private void SomeFirstCallDebug()
    {
        var eventData = new PointerEventData(FindObjectOfType<EventSystem>());
        OnPointerDown(eventData);
        OnPointerUp(eventData);
    }
    #endregion

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.anchoredPosition = defaultPosition;
        base.OnPointerUp(eventData);
    }
}
