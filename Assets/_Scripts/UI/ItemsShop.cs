using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsShop : MonoBehaviour
{
    [SerializeField]
    private GameObject shop;
    public void ItemShopButton()
    {
        shop.SetActive(!shop.activeSelf);
    }
}
