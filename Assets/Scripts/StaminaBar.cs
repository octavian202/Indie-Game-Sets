using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class StaminaBar : MonoBehaviour
    {
        public Slider staminaBar;

        private float maxStamina = 100f;
        private float currentStamina;
        private Coroutine regenerate;

        private float _walkRegenRate = 8f;
        private float _idleRegenRate = 17f;

        public static StaminaBar instance;

        private FirstPersonController fpc;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            fpc = GameObject.Find("Player").GetComponent<FirstPersonController>();
            
            currentStamina = maxStamina;
            staminaBar.maxValue = maxStamina;
            staminaBar.value = maxStamina;
        }

        public void UseStamina(float amount)
        {
            if (currentStamina - amount < 0) return;

            if (regenerate != null) StopCoroutine("RegenerateStamina");

            currentStamina -= amount;
            staminaBar.value = currentStamina;

            regenerate = StartCoroutine("RegenerateStamina");
        }

        private IEnumerator RegenerateStamina()
        {
            yield return new WaitForSeconds(1f);

            while (currentStamina < maxStamina)
            {
                var regen = _idleRegenRate;
                if (fpc.isWalking) regen = _walkRegenRate;

                currentStamina += regen * Time.deltaTime;
                staminaBar.value = currentStamina;
                
                yield return null;
            }

            regenerate = null;
        }
    }
}