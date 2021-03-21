using UnityEngine;
using UnityEngine.Events;

namespace GJgame
{
    public class AisleLabel : MonoBehaviour
    {
        public TMPro.TMP_Text Label;

        public void SetItemLabel(ShopItem item)
        {
            Label.text = item.GetSpriteText;
        }
    }

}