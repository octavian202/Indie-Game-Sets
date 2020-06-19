using System;
using UnityEngine;

namespace Scripts
{
    public class PlayerHealth : MonoBehaviour, IDamageable, IHealable
    {
        public float maxHealth  = 100f;
        public float currentHealth;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float amount)
        {
            currentHealth -= amount;
        }

        public void Die()
        {
            print("you died");
        }

        public void Heal(float amount)
        {
            currentHealth += amount;
        }
    }
}