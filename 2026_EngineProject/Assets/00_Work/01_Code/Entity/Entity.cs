using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Work.Code.Entities
{
    public abstract class Entity : ModuleOwner
    {
        public Transform Transform => transform;

        public bool IsDead { get; set; }
        public Action<Entity> OnAttack;
        public UnityEvent OnHitEvent;
        public UnityEvent OnDeadEvent;

        protected Dictionary<Type, IModule> _components;

        protected override void Awake()
        {
            base.Awake();
            _components = new Dictionary<Type, IModule>();
            AddComponents();
        }

        protected virtual void AddComponents()
        {
            GetComponentsInChildren<IModule>().ToList()
                .ForEach(component => _components.Add(component.GetType(), component));
        }


        public void DestroyEntity()
        {
            Die();
            Destroy(gameObject);
        }

        protected virtual void Die()
        {
        }

    }
}
