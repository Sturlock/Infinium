using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infinium.Combat
{
	public class Stamina : MonoBehaviour
	{
	    // Start is called before the first frame update
	    [SerializeField] float stamaina = 100f;
		[SerializeField] float currentStamina;

		WaitForSeconds regenTick = new WaitForSeconds(.01f);
		Coroutine regen;

        void Start()
        {
			currentStamina = stamaina;
        }
        public void UseStamina(float amount)
	    {
			
			if (currentStamina - amount >= 0)
			{
				currentStamina -= amount;
				if (regen != null)
				{
					StopCoroutine(RegenStamina());
				}
				regen = StartCoroutine(RegenStamina());
			}
			else Debug.Log("No Stamina");
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
	}
}
