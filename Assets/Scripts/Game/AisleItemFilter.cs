using UnityEngine;
using UnityEngine.Events;

namespace GJgame
{
    public class AisleItemFilter : MonoBehaviour
    {
        public Collider[] SpawnZones;

        public AisleLabel[] Labels;

        public int Density = 5;

        [EnumFlag]
        public ShopItemType AllowedItemTypes;

        public void FillAisle(ShopItem item)
        {
            for (int i = 0; i < Labels.Length; i++)
            {
                Labels[i].SetItemLabel(item);
            }
            for (int i = 0; i < SpawnZones.Length; i++)
            {
                var zone = SpawnZones[i];
                for (int z = 0; z < Density;)
                {
                    var newItem = GameObject.Instantiate(item, transform);


                    var pos = Vector3.Scale(Random.insideUnitSphere, zone.bounds.extents / newItem.Size);
                    pos = zone.transform.rotation * pos;
                    pos.y = 0;

                    newItem.transform.position = zone.transform.TransformPoint(pos);
                    var randomForward = Quaternion.AngleAxis(Random.Range(0, 360), zone.transform.up) * zone.transform.forward;
                    newItem.transform.rotation = Quaternion.LookRotation(randomForward);
                    z += newItem.Size;
                }
            }
        }
    }

}