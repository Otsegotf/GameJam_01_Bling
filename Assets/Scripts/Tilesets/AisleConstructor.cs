using GJgame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AisleConstructor : MonoBehaviour
{
    public Transform AisleTarget;

    public SingleSwitcher NonCornerVisual;

    public SingleSwitcher CornerVisual;

    public StateToggler LeftCorner;

    public StateToggler RightCorner;

    public ShopItem ItemPrefab;
    public void SetAisleState(bool isAisle, bool leftCorner, bool rightCorner, ShopItemType prefferedItemType)
    {
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

        var aisle = GetComponentInChildren<AisleItemFilter>();
        if (aisle && aisle.AllowedItemTypes.HasFlag(prefferedItemType))
        {
            aisle.FillAisle(GameManager.Instance.ItemLibrary.GetRandomItemOfType(prefferedItemType));
        }
    }
}
