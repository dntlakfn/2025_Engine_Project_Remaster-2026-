using System;
using UnityEngine;

namespace Work.Code.Inventories
{
    public class ItemDataSO : ScriptableObject
    {
        [Header("Item Data")]
        public string itemId;
        public string itemName;
        public Sprite icon;
        [TextArea] public string description;

        [Header("Grid Size")]
        public Vector2Int size = Vector2Int.one;

        [Header("Option")]
        public bool canRotate = true;

    }

    [Serializable]
    public class InventoryItemInstance
    {
        public string uniqueId;
        public ItemDataSO data;
        public Vector2Int position;
        public bool rotated;

        public Vector2Int Size
        {
            get
            {
                if (data == null) return Vector2Int.one;
                return rotated ? new Vector2Int(data.size.y, data.size.x) : data.size;
            }
        }

        public InventoryItemInstance(ItemDataSO data)
        {
            uniqueId = Guid.NewGuid().ToString();
            this.data = data;
            position = Vector2Int.zero;
            rotated = false;
        }
    }
}