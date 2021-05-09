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
		Coroutine regen;

		public int GetStamina()
        {
			return currentStamina;
        }

        void Start()
        {
			currentStamina = stamaina;
        }
        public void UseStamina(bool usingStaminia, int amount)
	    {
			if (currentStamina - amount >= 0 && usingStaminia)
			{
				currentStamina -= amount;
				if (regen != null)
				{
					StopCoroutine(RegenStamina());
				}

			}
			else if(currentStamina + amount <= 100 && !usingStaminia)
			{
				regen = StartCoroutine(RegenStamina());
			}
		}

		private IEnumerator RegenStamina()
        {
			yield return new WaitForSeconds(2);

			while(currentStamina < stamaina)
            {
				currentStamina += stamaina / 100;
				yield return regenTick;
			}
			regen = null;
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
