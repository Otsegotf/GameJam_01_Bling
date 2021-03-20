using GJgame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AisleConstructor : MonoBehaviour
{
    public Transform AisleTarget;

    public ShopItemSwitcher NonCornerVisual;

    public SingleSwitcher CornerVisual;

    public StateToggler LeftCorner;

    public StateToggler RightCorner;

    public ShopItem ItemPrefab;

    public ShopItemType CurrentPreferedType;
    public void SetAisleState(bool isAisle, bool leftCorner, bool rightCorner, ShopItemType prefferedItemType)
    {
        CurrentPreferedType = prefferedItemType;
        gameObject.SetActive(isAisle);
        var isCorner = leftCorner || rightCorner;
        NonCornerVisual.gameObject.SetActive(!isCorner);
        CornerVisual.gameObject.SetActive(isCorner);
        LeftCorner.gameObject.SetActive(isCorner);
        RightCorner.gameObject.SetActive(isCorner);
        if (isCorner)
        {
            LeftCorner.SetState(leftCorner);
            RightCorner.SetState(rightCorner);
        }
        else
        {
            NonCornerVisual.SetAisle(prefferedItemType);
        }

        var aisle = GetComponentsInChildren<AisleItemFilter>();
        ItemPrefab = GameManager.Instance.ItemLibrary.GetRandomItemOfType(prefferedItemType);
        if (aisle.Length > 0 && aisle[0].AllowedItemTypes.HasFlag(prefferedItemType))
        {
            for (int i = 0; i < aisle.Length; i++)
            {
                aisle[i].FillAisle(ItemPrefab);
            }
        }
    }
}
