using GJgame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemSwitcher : MonoBehaviour
{
    // Start is called before the first frame update

    public AisleItemFilter[] Objects;

    public void SetAisle(ShopItemType itemType)
    {
        var filtered = new List<AisleItemFilter>();
        for (int i = 0; i < Objects.Length; i++)
        {
            if (Objects[i].AllowedItemTypes.HasFlag(itemType))
                filtered.Add(Objects[i]);
        }
        var active = Random.Range(0, filtered.Count);
        for (int i = 0; i < Objects.Length; i++)
        {
            Objects[i].gameObject.SetActive(false);
        }
        if (filtered.Count > 0)
            filtered[active].gameObject.SetActive(true);
        else
            Objects[Random.Range(0, Objects.Length)].gameObject.SetActive(true);
    }
}
