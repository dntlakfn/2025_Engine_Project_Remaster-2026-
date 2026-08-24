using UnityEngine;
using Work.Code.Inventories;
using Work.Code.PlayerStats;

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
        public int damage;
        public int maxDurability;
        public int durability;
        
        public WeaponType type;
        public WeaponProficiencyLevelSO weaponProficiencyData;

        private void OnValidate()
        {
            durability = maxDurability;
        }
    }


}
