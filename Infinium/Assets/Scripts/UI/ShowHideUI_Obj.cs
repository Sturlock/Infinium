using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideUI_Obj : MonoBehaviour
{
    [SerializeField] KeyCode toggleKey = KeyCode.Escape;
    [SerializeField] GameObject uiContainer = null;
    // Start is called before the first frame update
    void Start()
    {
        uiContainer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        uiContainer.SetActive(!uiContainer.activeSelf);
    }

    public void ToggleCanvas()
    {
        if (uiContainer.GetComponent<CanvasGroup>().alpha > 0f)
        {
            uiContainer.GetComponent<CanvasGroup>().alpha = 1f;
        }
        else
        {
            uiContainer.GetComponent<CanvasGroup>().alpha = 0f;
        }
    }
}
