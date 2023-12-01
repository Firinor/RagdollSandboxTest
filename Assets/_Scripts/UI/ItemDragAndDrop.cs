using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ItemDragAndDrop : Selectable, IDragHandler
{
    [SerializeField]
    private GameObject prefab;

    private GameObject item;
    private static float maxDistance = 5f;
    //private Image image;

    //protected override void Awake()
    //{
    //    image = GetComponent<Image>();
    //}


    public override void OnPointerDown(PointerEventData eventData) 
    {
        image.raycastTarget = false;
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
                layerMask: LayerMask.GetMask("Default", "TransparentFX", "CollisionObjects")
                );

        if(isHit)
        {
            item.transform.position = raycastHit.point;
        }
        else
        {
            item.transform.position = ray.origin + ray.direction * maxDistance;
        }
    }
    public override void OnPointerUp(PointerEventData eventData) 
    {
        image.raycastTarget = true;

        if (item == null)
            return;

        if (eventData.pointerCurrentRaycast.gameObject?.layer == LayerMask.NameToLayer("ShopUI"))
        {
            item.SetActive(false);
            return;
        }

        //Release item
        item = null;
    }

}
