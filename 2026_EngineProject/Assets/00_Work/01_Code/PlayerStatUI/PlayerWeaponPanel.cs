using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Work.Code.Events;
using Work.Code.Items;

namespace Work.Code.PlayerStatUI
{
    public class PlayerWeaponPanel : MonoBehaviour
    {
        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private TextMeshProUGUI damageText;
        [SerializeField] private TextMeshProUGUI duraility;
        [SerializeField] private Image durailitySlider;
        [SerializeField] private TextMeshProUGUI descripction;

        private WeaponDataSO _currentWeapon;

        private void Awake()
        {
            Bus<EquipWeapon>.OnEvent += OnEquipWeapon;
        }
        private void OnDestroy()
        {
            Bus<EquipWeapon>.OnEvent -= OnEquipWeapon;
        }

        public void OnEquipWeapon(EquipWeapon evt)
        {
            WeaponDataSO weapon = evt.WeaponData;

            itemIcon.sprite = weapon.icon;
            itemNameText.text = weapon.name;
            damageText.text = $"Damage : {weapon.damage}";
            duraility.text = $"{weapon.durability}/{weapon.maxDurability}";
            durailitySlider.fillAmount = (float)weapon.durability / weapon.maxDurability;
            descripction.text = weapon.description;

            _currentWeapon = weapon;
        }

        public void ClearWeaponInfo()
        {
            itemIcon.sprite = null;
            itemNameText.text = "";
            damageText.text = "";
            duraility.text = "";
            durailitySlider.fillAmount = 0f;
            descripction.text = "";
        }

        public void UpdateDurailityInfo()
        {
            duraility.text = $"{_currentWeapon.durability}/{_currentWeapon.maxDurability}";
            durailitySlider.fillAmount = (float)_currentWeapon.durability / _currentWeapon.maxDurability;
        }
    }

    #region Events

    public struct EquipWeapon : IEvent
    {
        public WeaponDataSO WeaponData;
        public EquipWeapon(WeaponDataSO weaponData)
        {
            this.WeaponData = weaponData;
        }
    }

    #endregion
}