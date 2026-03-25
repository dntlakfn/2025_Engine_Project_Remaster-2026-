using UnityEngine;
using UnityEngine.EventSystems;
using Work.Code.Inventories;

namespace Work.Code.Items
{
    public class Weapon : DragableItem, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private WeaponDataSO _weaponData;
        public void Initialize(WeaponDataSO itemData)
        {
            base.Initialize(itemData);
            _weaponData = itemData;
        }
        public void OnPointerEnter(PointerEventData eventData)
        {

        }

        public void OnPointerExit(PointerEventData eventData)
        {

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                
            }
        }
    }

    #region Events

    

    #endregion
}