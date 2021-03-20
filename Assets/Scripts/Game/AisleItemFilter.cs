using UnityEngine;
using UnityEngine.Events;

namespace GJgame
{
    public class AisleItemFilter : MonoBehaviour
    {
        public Collider[] SpawnZones;

        public int Density = 5;

        [EnumFlag]
        public ShopItemType AllowedItemTypes;

        public void FillAisle(ShopItem item)
        {
            for (int i = 0; i < SpawnZones.Length; i++)
            {
                var zone = SpawnZones[i];
                for (int z = 0; z < Density; z++)
                {
                    var pos = Vector3.Scale(Random.insideUnitSphere, zone.transform.rotation * zone.bounds.extents);
                    var newItem = GameObject.Instantiate(item, transform);
                    newItem.transform.position = zone.transform.TransformPoint(pos);
                    newItem.transform.rotation = Quaternion.AngleAxis(Random.Range(-180, 180), zone.transform.up);
                }
            }
        }
    }

}