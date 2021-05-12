using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Infinium.Stats
{
	public class LevelDisplay : MonoBehaviour
	{
        BaseStats baseStats;
        private void Awake()
        {
            baseStats = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
        }

        void Update()
        {
            GetComponent<TextMeshProUGUI>().text = baseStats.GetLevel().ToString();

        }
    }
}
