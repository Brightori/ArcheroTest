
using UnityEngine;

namespace Components
{
    class HealthComponent : MonoBehaviour, IHealthComponent
    {
        [SerializeField] private float health = 10;
        [SerializeField] private float maxHealth = 10;

        public float Health => health;
        public float MaxHealth => maxHealth;
        public bool IsDead => health <= 0;

        public void AddHealth(float amount, bool additive = true)
        {
            if (additive)
                health += amount;
            else
                health = amount;
        }

        public void RemoveHealth(float amount)
        {
            health -= amount;
        }
    }

    public interface IHealthComponent : IComponent
    {
        float Health { get; }
        float MaxHealth { get; }
        bool IsDead { get; }

        void AddHealth(float amount, bool additive = true);
        void RemoveHealth(float amount);
    }
}
