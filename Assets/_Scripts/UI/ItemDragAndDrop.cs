using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragAndDrop : Selectable, IDragHandler
{
    [SerializeField]
    private GameObject prefab;

    private GameObject item;
    private static float maxDistance = 15f;
    private static float nearDistance = 5f;// maxDistance / 3;
    private CanvasGroup canvasGroup;

    protected override void Awake()
    {
        base.Awake();
        canvasGroup = GetComponentInParent<CanvasGroup>();
        if (canvasGroup == null)
            throw new System.Exception("Couldn't find the required component in the parents!");
    }

    public override void OnPointerDown(PointerEventData eventData) 
    {
        canvasGroup.blocksRaycasts = false;
        if (item == null)
        {
            item = Instantiate(prefab);
        }

        OnDrag(eventData);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (item == null)
            return;

        if (eventData.pointerCurrentRaycast.gameObject?.layer == LayerMask.NameToLayer("ShopUI"))
        {
            item.SetActive(false);
            return;
        }

        if(!item.activeSelf)
            item.SetActive(true);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isHit = Physics.Raycast(
                ray, 
                out RaycastHit raycastHit, 
                maxDistance, 
                layerMask: LayerMask.GetMask("Default", "ImmovableObjects", "CollisionObjects")
                );

        if(isHit)
        {
            item.transform.position = raycastHit.point;
        }
        else
        {
            item.transform.position = ray.origin + ray.direction * nearDistance;
        }
    }
    public override void OnPointerUp(PointerEventData eventData) 
    {
        canvasGroup.blocksRaycasts = true;

        if (item == null)
            return;

        if (eventData.pointerCurrentRaycast.gameObject?.layer == LayerMask.NameToLayer("ShopUI"))
        {
            item.SetActive(false);
            return;
        }

        //Release item
        var start = item.GetComponent<SetObjectToStart>();
        start.enabled = true;
        item = null;
    }

}
