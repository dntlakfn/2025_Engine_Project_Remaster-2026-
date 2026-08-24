using UnityEngine;
using Work.Code.Events;

namespace Work.Code.Entities
{
    public class EntityHealth : MonoBehaviour, IModule
    {
        [SerializeField] private int maxHealth;

        private Entity _entity;
        private int _health;
        public int Health
        {
            get
            {
                return _health;
            }
            private set
            {
                if (_health != value)
                {
                    OnValueChanged(value - _health);
                }
                _health = Mathf.Clamp(value, 0, maxHealth);
                if(_health <= 0)
                {
                    _entity.OnDeadEvent?.Invoke();
                }
            }
        }

        public void Initialize(ModuleOwner owner)
        {
            _entity = owner as Entity;
            _health = maxHealth;
        }

        public void GetDamage(int damage)
        {
            Health -= damage;
        }

        private void OnValueChanged(int v)
        {
            Bus<ChangedHpEvent>.Raise(new ChangedHpEvent(Health, maxHealth));

            if (v < 0) // 데미지를 입은 경우
            {
                _entity.OnHitEvent?.Invoke();
            }
            else if (v == 0) // 체력이 변하지 않은 경우
            {

            }
            else if (v > 0) // 체력이 회복된 경우
            {

            }
        }
    }

    #region Events

    public struct ChangedHpEvent : IEvent
    {
        public int CurrentHp;
        public int MaxHp;

        public ChangedHpEvent(int currentHp, int maxHp)
        {
            CurrentHp = currentHp;
            MaxHp = maxHp;
        }

    }

    #endregion
}