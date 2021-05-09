using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Infinium.Saving;

namespace Infinium.Core
{
	public class Stamina : MonoBehaviour, ISaveable
	{
	    // Start is called before the first frame update
	    [SerializeField] int stamaina = 100;
		[SerializeField] int currentStamina;

		WaitForSeconds regenTick = new WaitForSeconds(.01f);
		public bool regen = false;

		public int GetStamina()
        {
			return currentStamina;
        }

        void Start()
        {
			currentStamina = stamaina;
			regen = false;
		}
        public void UseStamina(bool usingStaminia, int amount)
	    {
			if (currentStamina - amount >= 0 && usingStaminia)
			{
                if (regen)
                {
                    StopCoroutine(RegenStamina());
					regen = false;
                }
                currentStamina -= amount;
				
			}
		}

		public IEnumerator RegenStamina()
        {
			yield return new WaitForSeconds(2);
			
			if (regen)
			{
				while(currentStamina < stamaina)
	            {
					currentStamina += stamaina / 100;
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
            stamaina = (int)state;
			StartCoroutine(RegenStamina());
        }
    }
}
