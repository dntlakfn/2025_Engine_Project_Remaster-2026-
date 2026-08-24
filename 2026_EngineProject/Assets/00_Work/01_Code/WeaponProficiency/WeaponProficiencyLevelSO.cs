using System;
using System.Runtime.ExceptionServices;
using UnityEngine;
using Work.Code.Events;
using Work.Code.Items;

namespace Work.Code.PlayerStats
{
    [CreateAssetMenu(fileName = "_ProficiencyLevel", menuName = "SO/Weapon/ProficiencyLevel")]
    public class WeaponProficiencyLevelSO : ScriptableObject
    {
        public event Action OnLevelUp;
        public WeaponType weaponType;
        public int level = 1;
        public int firstMaxExp = 100;
        [HideInInspector] public int maxExp;

        [TextArea(1, 2)]
        [Header("경험치 공식 (에디터에서 수정 ㄴㄴ)")]
        public string _ = "maxExp = (firstMaxExp + (2 * level) * level/2)";

        private int _exp;
        public int Exp 
        {
            get
            {
                return _exp;
            }
            set
            {
                if (_exp != value)
                {
                    Bus<ChangedExpEvent>.Raise(new ChangedExpEvent(this));
                }
                _exp = Mathf.Clamp(value, 0, int.MaxValue);
                if(_exp >= maxExp)
                {
                    OnLevelUp?.Invoke();
                    level++;
                    maxExp = (maxExp + (2 * level) * level/2);
                }
                
            }
        }

        private void Awake()
        {
            maxExp = firstMaxExp;
        }


    }

    #region Events

    public struct ChangedExpEvent : IEvent
    {
        public WeaponProficiencyLevelSO WeaponLevelData;
        public ChangedExpEvent(WeaponProficiencyLevelSO weaponLevelData)
        {
            WeaponLevelData = weaponLevelData;
        }
    }

    #endregion
}