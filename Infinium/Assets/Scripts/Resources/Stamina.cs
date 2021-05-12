using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Infinium.Saving;

namespace Infinium.Resources
{
	public class Stamina : MonoBehaviour, ISaveable
	{
	    // Start is called before the first frame update
	    float stamaina = 100;
		[SerializeField] float currentStamina;

		WaitForSeconds regenTick = new WaitForSeconds(.01f);
		public bool regen = false;

		public float GetStamina()
        {
			return currentStamina;
        }

        void Start()
        {
			currentStamina = stamaina;
			regen = false;
		}
        public void UseStamina(bool usingStaminia, float amount)
	    {
			if (currentStamina - amount >= 0f && usingStaminia)
			{
                if (regen)
                {
                    StopCoroutine(RegenStamina());
					regen = false;
                }
                currentStamina -= amount;
				
			}
		}
		public float GetPercentage()
        {
			return 100 * (currentStamina / stamaina);
        }

		public IEnumerator RegenStamina()
        {
			yield return new WaitForSeconds(2);
			
			if (regen)
			{
				while(currentStamina < stamaina)
	            {
					currentStamina += stamaina / 100f;
					yield return regenTick;

				}
				regen = false;

			}
			

		}

        public object CaptureState()
        {
            return stamaina;
        }
        public void RestoreState(object state)
        {
            stamaina = (float)state;
			StartCoroutine(RegenStamina());
        }
    }
}
