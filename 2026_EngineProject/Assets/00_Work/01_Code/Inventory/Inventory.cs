using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Work.Code.Events;
using Work.Code.Items;
using static Work.Code.Items.DragableItem;

namespace Work.Code.Inventories
{
    public class Inventory : MonoBehaviour, IDropHandler
    {
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private int cellSize;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Transform blocks;

        private InventoryItemInstance[,] _inventoryArray;
        private Image[] _blockArray; 
        private RectTransform _rectTrm;
        
        private void Awake()
        {
            CreateInventory();
            Bus<ItemDraggingEvent>.OnEvent += ItemDraggingHandle;

        }

        private void OnDestroy()
        {
            Bus<ItemDraggingEvent>.OnEvent -= ItemDraggingHandle;

        }
        public void CreateInventory()
        {
            
            _blockArray = blocks.GetComponentsInChildren<Image>();
            
            _inventoryArray = new InventoryItemInstance[width, height];

            _rectTrm = GetComponent<RectTransform>();
        }
        public void TogglePanel()
        {
            transform.parent.gameObject.SetActive(!transform.parent.gameObject.activeSelf);
            canvasGroup.interactable = !canvasGroup.interactable;
            canvasGroup.blocksRaycasts = !canvasGroup.blocksRaycasts;
        }

        public bool CanEquipItem(DragableItem item, Vector2 position, out Vector2Int currentCanEquipPoint)
        {
            InventoryItemInstance itemInst = item.GetItemInstance();
            currentCanEquipPoint = new Vector2Int(-1, -1);

            int x = Mathf.FloorToInt(position.x / cellSize);
            int y = Mathf.FloorToInt(-position.y / cellSize);
            if (position.x % cellSize >= (cellSize / 2)) x++;
            if (-position.y % cellSize >= (cellSize / 2)) y++;

            int gridWidth = (int)itemInst.Size.x;
            int gridHeight = (int)itemInst.Size.y;

            Debug.Log($"mousePos : ({x}, {y}), gridSize : ({gridWidth}, {gridHeight})");

            if (x + gridWidth < 0 || y + gridHeight < 0 || x + gridWidth > width || y + gridHeight > height) 
                return false;

            currentCanEquipPoint = new Vector2Int(x * cellSize, -y * cellSize);


            for (int i = y; i < y+gridHeight; i++)
            {
                for(int j  = x; j < x + gridWidth - 1; j++)
                {
                    if (_inventoryArray[i, j] != null)
                        return false;
                }
            }

            return true;
        }

        public void EquipItem(DragableItem item, Vector2Int position)
        {

            InventoryItemInstance itemInst = item.GetItemInstance();

            int x = position.x;
            int y = position.y;

            int gridWidth = (int)itemInst.Size.x;
            int gridHeight = (int)itemInst.Size.y;

            for (int i = y; i < y + gridHeight; i++)
            {
                for (int j = x; j < x + gridWidth - 1; j++)
                {
                    _inventoryArray[i, j] = itemInst;
                }
            }

            item.GetRectTransform().anchoredPosition = position;

        }

        public void OnDrop(PointerEventData eventData)
        {
            DragableItem item = eventData.pointerDrag?.GetComponent<DragableItem>();
            Debug.Assert(item != null, "드롭을 했는데 왜 집고있는게 없냐");

            for (int i = 0; i < _blockArray.Length; i++)
            {
                _blockArray[i].color = Color.white;
            }

            if (CanEquipItem(item, item.GetRectTransform().anchoredPosition, out Vector2Int currentItemEquipPoint))
            {
                EquipItem(item, currentItemEquipPoint);
            }
            else
            {
                item.ReturnPrevPos();
            }
        }

        public void ItemDraggingHandle(ItemDraggingEvent evt)
        {

            for (int i = 0; i < _blockArray.Length; i++)
            {
                _blockArray[i].color = Color.white;
            }

            Inventory CurrentInv = evt.PointerEnter?.GetComponent<Inventory>();

            if (CurrentInv == null || CurrentInv.gameObject != gameObject) return;

            bool can = CanEquipItem(evt.Item, evt.Position, out Vector2Int currentCanEquipPoint);

            Debug.Log(currentCanEquipPoint);
            InventoryItemInstance item = evt.Item.GetItemInstance();

            if(currentCanEquipPoint == new Vector2Int(-1, -1))
            {
                return;
            }

            int x = currentCanEquipPoint.x != 0 ? currentCanEquipPoint.x / cellSize : 0;
            int y = -currentCanEquipPoint.y != 0 ? -currentCanEquipPoint.y / cellSize : 0;

            int itemWidth = item.Size.x;
            int itemHeight = item.Size.y;

            Debug.Log($"x, y : ({x}, {y})");



            for (int i = y; i < y + itemHeight; i++)
            {
                for (int j = x; j < x + itemWidth; j++)
                {
                    Debug.Log($"x : {j}, y : {i}, itemSize : ({itemWidth}, {itemHeight}), firstIdx : {(i * width) + j}");

                    if (can)
                        _blockArray[(i * width) + j].color = Color.green;
                    else
                        _blockArray[(i * width) + j].color = Color.red;

                }
            }
        }
        
    }
}