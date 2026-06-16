using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform stick;
    [SerializeField] private float maxRadius = 60f;

    public Vector2 InputValue { get; private set; }

    public void OnPointerDown(PointerEventData e) => MoveStick(e);
    public void OnDrag(PointerEventData e) => MoveStick(e);

    public void OnPointerUp(PointerEventData e)
    {
        stick.anchoredPosition = Vector2.zero;
        InputValue = Vector2.zero;
    }

    private void MoveStick(PointerEventData e)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            GetComponent<RectTransform>(),
            e.position,
            e.pressEventCamera,
            out Vector2 localPos
        );

        localPos = Vector2.ClampMagnitude(localPos, maxRadius);
        stick.anchoredPosition = localPos;
        InputValue = localPos / maxRadius;
    }
}