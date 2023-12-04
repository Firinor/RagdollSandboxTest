using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private IControllable controllable;

    [SerializeField]
    private Joint configurableJoint;
    private float targetDistance;

    private Camera cam;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float sensitivity;
    [SerializeField]
    private Joystick joystick;
    [SerializeField]
    private HeldButton upButton;
    [SerializeField]
    private HeldButton downButton;

    private void Awake()
    {
        cam = FindAnyObjectByType<Camera>();
    }

    private void MoveUp()
    {
        if (controllable != null)
            controllable.Action1();//Jump up or aim
        else
            cam.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void MoveDown()
    {
        if (controllable != null)
            controllable.Action2();//Sit down or shoot
        else
            cam.transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
    private void Update()
    {
        if (joystick.Direction != Vector2.zero)
            MoveByJoystick();

        if (upButton.IsHeld)
            MoveUp();

        if (downButton.IsHeld)
            MoveDown();
    }

    private void MoveByJoystick()
    {
        if (controllable != null)
            controllable.MoveByVector(joystick.Direction * Time.deltaTime);
        else
            cam.transform.Translate(
                new Vector3(joystick.Direction.x, joystick.Direction.y, 0) * speed * Time.deltaTime);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (configurableJoint.connectedBody != null)
        {
            MoveObject();
            return;
        }

        RotateCamera(eventData.delta);
    }

    private void MoveObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        configurableJoint.transform.position = ray.origin + ray.direction * targetDistance;
    }

    private void RotateCamera(Vector2 delta)
    {
        Vector3 angles = cam.transform.rotation.eulerAngles;
        float x = -delta.y * sensitivity + angles.x;//Vertical
        float y = delta.x * sensitivity + angles.y;//Horizontal

        //Debug.Log(x);
        if (x < 0) x += 360;
        if (x > 360) x -= 360;
        if (x > 180) x = Math.Clamp(x, 271, 360);
        if (x < 180) x = Math.Clamp(x, 0, 89);
        if (y < -360) y += 360;
        if (y > 360) y -= 360;

        cam.transform.rotation = Quaternion.Euler(x, y, 0f);

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        bool isHit = Physics.Raycast(
                ray,
                out RaycastHit raycastHit,
                maxDistance: float.MaxValue,
                layerMask: LayerMask.GetMask("ImmovableObjects", "CollisionObjects")
                );

        if (isHit)
        {
            Rigidbody rb = raycastHit.collider.attachedRigidbody;
            if(rb != null)
            {
                configurableJoint.transform.position = raycastHit.point;
                configurableJoint.connectedBody = rb;
                targetDistance = raycastHit.distance;
            }
        }
        else
        {
            configurableJoint.connectedBody = null;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        configurableJoint.connectedBody = null;
    }
}