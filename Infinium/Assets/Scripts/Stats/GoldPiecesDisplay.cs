using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Infinium.Stats
{
	public class GoldPiecesDisplay : MonoBehaviour
	{
		GoldPieces GP;
	    private void Awake()
	    {
	        GP = GameObject.FindGameObjectWithTag("Player").GetComponent<GoldPieces>();
	    }
	
	    void Update()
	    {
	        GetComponent<TextMeshProUGUI>().text = GP.GetGoldPieces().ToString();
	
	    }
	}
}
