using UnityEngine;
using UnityEngine.EventSystems;
using Work.Code.Events;
using Work.Code.Inventories;
using Work.Code.Items.InfoUI;

namespace Work.Code.Items
{
    public class WeaponUI : DragableItemUI
    {
        [SerializeField] private WeaponDataSO _weaponData; // 이거 테스트임 현재 아이템을 생성하는 애가 없어서
        private void Awake()
        {
            Initialize(_weaponData);
        }

        public override void Initialize(ItemDataSO itemData)
        {
            _weaponData = itemData as WeaponDataSO;
            Debug.Assert(_weaponData != null, $"{gameObject.name}의 {_weaponData}가 WeaponDataSO가 아님;;");
            base.Initialize(itemData);

        }

        public void Eqiup()
        {
            
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            Bus<ShowItemInfoPanel>.Raise(new ShowItemInfoPanel(_weaponData, transform.position));
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            Bus<HideItemInfoPanel>.Raise(new HideItemInfoPanel(_weaponData));

        }

    }

    #region Events

    

    #endregion
}