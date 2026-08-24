using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Work.Code.Events;
using Work.Code.Inventories;

namespace Work.Code.Items.InfoUI
{
    /// <summary> 요약 : 아이템의 정보를 띄워주는 ui. </summary>
    /// <remarks> Bus&lt;ShowItemInfoPanel&gt;.Raise(ItemDataSO)로 나타나기, 숨기기 </remarks>
    public abstract class ItemInfoPanel : MonoBehaviour
    {
        [SerializeField] protected Image itemIcon;
        [SerializeField] protected TextMeshProUGUI itemName;
        [SerializeField] protected TextMeshProUGUI itemDescripction;

        public virtual void Awake()
        {
            Bus<ShowItemInfoPanel>.OnEvent += ShowPanel;
            Bus<HideItemInfoPanel>.OnEvent += HidePanel;
        }
        public virtual void OnDestroy()
        {
            Bus<ShowItemInfoPanel>.OnEvent -= ShowPanel;
            Bus<HideItemInfoPanel>.OnEvent -= HidePanel;
        }

        protected abstract void ShowPanel(ShowItemInfoPanel evt);
        protected abstract void HidePanel(HideItemInfoPanel evt);

    }

    #region Events

    public struct ShowItemInfoPanel : IEvent
    {
        public ItemDataSO ItemData;
        public Vector2 Position;
        public ShowItemInfoPanel(ItemDataSO itemData, Vector2 position)
        {
            this.ItemData = itemData;
            this.Position = position;
        }
    }
    public struct HideItemInfoPanel : IEvent
    {
        public ItemDataSO ItemData;
        public HideItemInfoPanel(ItemDataSO itemData)
        {
            this.ItemData = itemData;
        }
    }

    #endregion
}