using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Work.Code.Events;
using Work.Code.Inventories;

namespace Work.Code.Items
{
    public class DragableItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private Image weaponBg;

        private ItemDataSO _itemData;

        private RectTransform _rectTrm;
        private InventoryItemInstance _itemInstance;
        private CanvasGroup _canvasGroup;

        private Transform _prevParent;
        private Transform _nextParent;
        private Vector2 _prevPos;
        
        

        protected void Initialize(ItemDataSO itemData)
        {
            _itemData = itemData;
            _rectTrm = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _itemInstance = new InventoryItemInstance(itemData);
            _prevParent = transform.parent;
            _prevPos = _rectTrm.anchoredPosition;
        } 

        public void OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = false;
            _prevParent = transform.parent;
            _prevPos = _rectTrm.anchoredPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
            Bus<ItemDraggingEvent>.Raise(new ItemDraggingEvent(eventData.pointerEnter, this, _rectTrm.anchoredPosition));

            if (eventData.pointerEnter != null && eventData.pointerEnter.TryGetComponent(out Inventory inventory))
            {
                Transform nextParent = eventData.pointerEnter.transform;
                _nextParent = nextParent;
                transform.SetParent(_nextParent);
                Debug.Log(_rectTrm.anchoredPosition);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;

            if(eventData.pointerEnter != null && eventData.pointerEnter.TryGetComponent(out Inventory inventory))
            {
                _nextParent.SetParent(inventory.transform);
                _prevParent = _nextParent;
                Bus<ItemDraggingEvent>.Raise(new ItemDraggingEvent(null, this, _rectTrm.anchoredPosition));


            }
            else
            {
                
                ReturnPrevPos();

            }
        }

        public void ReturnPrevPos()
        {
            if (_prevParent == null) return;

            Debug.Log($"{gameObject.name} was returned at prev Pos In {_prevParent}");
            transform.SetParent(_prevParent);
            _rectTrm.anchoredPosition = _prevPos;
        }


        #region Getter

        public RectTransform GetRectTransform() => _rectTrm;
        public InventoryItemInstance GetItemInstance() => _itemInstance;

        #endregion

        #region Events

        public struct ItemDraggingEvent : IEvent
        {
            public GameObject PointerEnter;
            public DragableItem Item;
            public Vector2 Position;

            public ItemDraggingEvent(GameObject pointerEnter, DragableItem item, Vector2 position)
            {
                PointerEnter = pointerEnter;
                Item = item;
                Position = position;
            }
        }

        #endregion
    }
}
