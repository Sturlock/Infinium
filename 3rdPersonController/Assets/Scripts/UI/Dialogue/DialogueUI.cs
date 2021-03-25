using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Infinium.Dialogue;
using TMPro;

namespace Infinium.UI
{
    public class DialogueUI : MonoBehaviour
    {
        PlayerConversant playerConversant;
        [SerializeField] GameObject Panel;
        [Header("Buttons", order = 0)]
        [SerializeField] Button quitButton;
        [SerializeField] Button nextButton;
        [SerializeField] Transform choiceRoot;
        [SerializeField] GameObject choicePrefab;

        [Header("Text", order = 1)]
        [SerializeField] TextMeshProUGUI conversantName;
        [SerializeField] TextMeshProUGUI AIText;
        [SerializeField] GameObject AIResponse;
        
       // Start is called before the first frame update
        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").
                GetComponent<PlayerConversant>();
            playerConversant.OnConversationUpdate += UpdateUI;
            nextButton.onClick.AddListener(() => playerConversant.Next());
            quitButton.onClick.AddListener(() => playerConversant.Quit());
            UpdateUI();
        }

        // Update is called once per frame
        void UpdateUI()
        {
            Panel.SetActive(true);
            gameObject.SetActive(playerConversant.IsActive());
            if (!playerConversant.IsActive())
            {
                Panel.SetActive(false);
                return;
            }
            conversantName.text = playerConversant.GetCurrentConversantName();
            AIResponse.SetActive(!playerConversant.IsChoosing());
            choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());
            if (playerConversant.IsChoosing())
            {
                BuildChoiceList();
            }
            else
            {
                AIText.text = playerConversant.GetText();
                nextButton.gameObject.SetActive(playerConversant.HasNext());
            }

            
        }

        private void BuildChoiceList()
        {
            foreach (Transform item in choiceRoot)
            {
                Destroy(item.gameObject);
            }
            foreach (DialogueNode choice in playerConversant.GetChoices())
            {
                GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);
                var textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
                textComp.text = choice.GetText();
                Button button = choiceInstance.GetComponentInChildren<Button>();
                button.onClick.AddListener(() =>
                {
                    playerConversant.SelectChoice(choice);
                });
            }
        }
    }
}