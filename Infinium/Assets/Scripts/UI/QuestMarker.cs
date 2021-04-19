using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMarker : MonoBehaviour
{
    public Sprite icon;
    public Image image;

    // Start is called before the first frame update
    public Vector2 position {  get { return new Vector2(transform.position.x, transform.position.z); } }

}
