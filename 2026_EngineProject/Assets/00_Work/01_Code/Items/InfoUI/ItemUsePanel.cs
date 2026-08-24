using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Work.Code.Events;
using Work.Code.Inventories;
using Work.Code.PlayerStatUI;

namespace Work.Code.Items.InfoUI
{
    public class ItemUsePanel : MonoBehaviour
    {
        [SerializeField] private Button buttonPrefab;
        private Inventory _inventory;
        private TextMeshProUGUI _buttonText;

        private void Awake()
        {
            Bus<ShowItemUsePanel>.OnEvent += ShowUseOptions;
            Bus<HideItemUsePanel>.OnEvent += HideUseOptions;
        }
        private void OnDestroy()
        {
            Bus<ShowItemUsePanel>.OnEvent -= ShowUseOptions;
            Bus<HideItemUsePanel>.OnEvent -= HideUseOptions;
        }

        public void ShowUseOptions(ShowItemUsePanel evt)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            DragableItemUI item = evt.item;
            _inventory = item.GetComponentInParent<Inventory>();
            Debug.Assert(_inventory != null, $"{item.gameObject.name}는 인벤토리를 벗어나 있음.");

            transform.position = item.transform.position;

            Button deleteButton = Instantiate(buttonPrefab, transform);
            deleteButton.GetComponentInChildren<TextMeshProUGUI>().text = "버리기";
            deleteButton.onClick.AddListener(() =>
            {
                _inventory.UnequipItem(item);
                Destroy(item.gameObject);
            });

            ItemDataSO itemData = item.GetItemInstance().data;
            Button useButton = Instantiate(buttonPrefab, transform);
            _buttonText = useButton.GetComponentInChildren<TextMeshProUGUI>();
            useButton.onClick.AddListener(() => 
            {
                UseItem(itemData);
                _inventory.UnequipItem(item);
                Destroy(item.gameObject);
            });
            switch(itemData) // 마음에 안들어;;
            {
                    case WeaponDataSO weaponData:
                        _buttonText.text = "장착하기";
                        break;
    
                    default:
                        _buttonText.text = "사용하기";
                        break;
            }
        }

        public void HideUseOptions(HideItemUsePanel evt)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            gameObject.SetActive(false);
        }

        private void UseItem(ItemDataSO itemData)
        {
            switch(itemData)
            {
                case WeaponDataSO weaponData:
                    {
                        Bus<EquipWeapon>.Raise(new EquipWeapon(weaponData));
                        
                    }
                    break;

                default:
                    Debug.Log($"Using item: {itemData.itemName}");
                    break;
            }

            Bus<HideItemUsePanel>.Raise(new HideItemUsePanel());

        }
    }

    #region Events

    public struct ShowItemUsePanel : IEvent
    {
        public DragableItemUI item;
        public ShowItemUsePanel(DragableItemUI item)
        {
            this.item = item;
        }
    }

    public struct HideItemUsePanel : IEvent
    {
        
    }

    #endregion
}