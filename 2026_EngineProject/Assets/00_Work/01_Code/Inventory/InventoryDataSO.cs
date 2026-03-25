using UnityEngine;

namespace Work.Code.Inventories
{
    [CreateAssetMenu(fileName = "InventoryDataSO", menuName = "SO/Inventory/InventoryData")]
    public class InventoryDataSO : ScriptableObject
    {
        public int width;
        public int height;


        public GameObject blockPrefab;

    }
}