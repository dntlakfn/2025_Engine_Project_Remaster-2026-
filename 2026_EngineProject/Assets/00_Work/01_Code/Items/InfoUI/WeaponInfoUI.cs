using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Work.Code.Items.InfoUI
{
    
    public class WeaponInfoUI : ItemInfoPanel
    {
        [Header("Weapon Info")]
        [SerializeField] private TextMeshProUGUI weaponType;
        [SerializeField] private TextMeshProUGUI weaponDamage;
        [SerializeField] private TextMeshProUGUI weaponDurability;

        public override void Awake()
        {
            base.Awake();
            gameObject.SetActive(false);
        }

        private void Initialize(WeaponDataSO data)
        {
            itemIcon.sprite = data.icon;
            itemName.text = data.itemName;
            itemDescripction.text = data.description;

            weaponType.text = data.type.ToString();
            weaponDamage.text = data.damage.ToString();
            weaponDurability.text = data.durability.ToString();
        }

        protected override void ShowPanel(ShowItemInfoPanel evt)
        {
            WeaponDataSO data = evt.ItemData as WeaponDataSO;
            if (data == null) return;
            
            Initialize(data);
            transform.position = evt.Position + new Vector2(-200,-150);
            gameObject.SetActive(true);
        }

        protected override void HidePanel(HideItemInfoPanel evt)
        {
            WeaponDataSO data = evt.ItemData as WeaponDataSO;
            if (data == null) return;

            gameObject.SetActive(false);
        }
    }
}