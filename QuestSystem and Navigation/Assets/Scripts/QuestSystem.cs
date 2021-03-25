using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSystem : MonoBehaviour
{
    public bool near = false;
    public bool QuestComplete = false;

    public GameObject Nearme;
    public GameObject DialougeBox;
    public GameObject Dialougeone;
    public GameObject Dialougetwo;
    public GameObject Dialougethree;

    private int ECounter = 0;

    public QuestMarker one;
    //public QuestMarker middle;
    //public QuestMarker end;
    public Compass com;

    // Start is called before the first frame update
    void Start()
    {
        Nearme.SetActive(false);
        DialougeBox.SetActive(false);
        Dialougeone.SetActive(false);
        Dialougetwo.SetActive(false);
        Dialougethree.SetActive(false);
        com = GetComponent<Compass>();
        com.AddQuestMarker(one);

    }

    // Update is called once per frame
    void Update()
    {
        if (near == true)
        {
            Nearme.SetActive(true);
            if (Input.GetKeyDown("e"))
            {
                Nearme.SetActive(false);
                //Debug.Log("Glad to see you");
                DialougeBox.SetActive(true);
                ECounter += 1;
                if (ECounter == 1 && QuestComplete == false)
                {
                    Dialougeone.SetActive(true);
                }
                if (ECounter>1 && QuestComplete == false)
                {
                    Dialougeone.SetActive(false);
                    //com.RemoveQuestMarker(start);
                    Dialougetwo.SetActive(true);
                    //com.AddQuestMarker(middle);
                }
                if (QuestComplete == true)
                {
                    //com.RemoveQuestMarker(middle);
                    //com.AddQuestMarker(end);
                }
                if (ECounter > 1 && QuestComplete == true)
                {
                    Dialougethree.SetActive(true);
                    //com.RemoveQuestMarker(end);
                }


            }
        }
        else
        {
            Nearme.SetActive(false);
            DialougeBox.SetActive(false);
            Dialougeone.SetActive(false);
            Dialougetwo.SetActive(false);
            Dialougethree.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            //Press e to enter
            //Debug.Log("You've entered the Zone");
            near = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            //Debug.Log("You've left the Zone");
            near = false;
        }
    }
}
