using System.Threading.Tasks.Sources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Work.Code.Entities;
using Work.Code.Events;

namespace Work.Code.PlayerStats
{
    public class PlayerStatPanel : MonoBehaviour
    {

        [Header("hpBar")]
        [SerializeField] private Image hpBar;
        [SerializeField] private TextMeshProUGUI hpBarText;

        [Header("ExpBar")]
        [SerializeField] private Image expBar;
        [SerializeField] private TextMeshProUGUI expBarText;
        [SerializeField] private TextMeshProUGUI weapontypeText;

        private void Awake()
        {
            Bus<ChangedHpEvent>.OnEvent += UpdateHpBar;
            Bus<ChangedExpEvent>.OnEvent += UpdateExpBar;
        }
        private void OnDestroy()
        {
            Bus<ChangedHpEvent>.OnEvent -= UpdateHpBar;
            Bus<ChangedExpEvent>.OnEvent -= UpdateExpBar;

        }


        public void UpdateHpBar(ChangedHpEvent evt)
        {
            hpBarText.text = $"{evt.CurrentHp}/{evt.MaxHp}";
            hpBar.fillAmount = (evt.CurrentHp / evt.MaxHp);
        }
        public void UpdateExpBar(ChangedExpEvent evt)
        {
            WeaponProficiencyLevelSO weaponLevelData = evt.WeaponLevelData;

            expBar.fillAmount = weaponLevelData.Exp / weaponLevelData.maxExp;
            expBarText.text = $"{weaponLevelData.Exp} / {weaponLevelData.maxExp}";
            weapontypeText.text = weaponLevelData.weaponType.ToString();
        }
    }
}