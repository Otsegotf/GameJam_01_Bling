using GJgame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingListUI : Singleton<ShoppingListUI>
{
    public Transform ListTransform;

    public TMPro.TMP_Text ShoppingListItem;

    public void UpdateList()
    {
        for (int i = 1; i < ListTransform.childCount; i++)
        {
            GameObject.Destroy(ListTransform.GetChild(i).gameObject);
        }
        var items = BuyListManager.Instance.CurrentList;
        foreach (var item in items)
        {
            var newItem = GameObject.Instantiate(ShoppingListItem, ListTransform);
            newItem.text = $"{item.Key} x {item.Value.Count}";
        }
    }
}
