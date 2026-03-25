using UnityEngine;
using Work.Code.Inventories;

namespace Work.Code.Items
{
    public enum WeaponType
    {
        NONE = 0, SWORD = 1,
    }

    [CreateAssetMenu(menuName = "SO/ItemData/Weapon")]
    public class WeaponDataSO : ItemDataSO
    {
        [Header("Weapon Data")]
        public int damege;
        public int durability;
        public WeaponType type;


    }
}
