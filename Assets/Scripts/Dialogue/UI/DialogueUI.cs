using UnityEngine;
using RPG.Dialogue;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using RPG.Stats;

namespace RPG.UI
{
    public class DialogueUI : MonoBehaviour
    {
        PlayerConversant playerConversant;
        [SerializeField]
        TextMeshProUGUI AIText;
        [SerializeField]
        Button nextButton;
        [SerializeField]
        Button quitButton;
        [SerializeField]
        GameObject AIResponse;
        [SerializeField]
        Transform choiceRoot;
        [SerializeField]
        GameObject choicePrefab;
        [SerializeField]
        TextMeshProUGUI conversantName;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            playerConversant.onConversationUpdated += UpdateUI;
            nextButton.onClick.AddListener(() => playerConversant.Next());
            quitButton.onClick.AddListener(() => playerConversant.Quit());

            UpdateUI();
        }

        void UpdateUI()
        {
            gameObject.SetActive(playerConversant.IsActive());
            if (!playerConversant.IsActive())
            {
                return;
            }

            conversantName.gameObject.SetActive(playerConversant.GetCurrentConversantName() != "Player");
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
                quitButton.gameObject.SetActive(!playerConversant.HasNext());
            }
        }

        private void BuildChoiceList()
        {
            //Limpia todos los botones de eleccion
            foreach (Transform item in choiceRoot)
            {
                Destroy(item.gameObject);
            }

            //Pone nuevos botones de eleccion
            foreach (DialogueNode choice in playerConversant.GetChoices())
            {
                GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);
                var textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();

                //Añade el checko delante del texto
                if (choice.isCheck)
                {
                    textComp.text = "[" + choice.statToCheck + "] " + choice.GetText();
                }
                else
                {
                    textComp.text = choice.GetText();
                }

                Button button = choiceInstance.GetComponentInChildren<Button>();
                if (choice.GetChildren().Count >= 1 && !choice.isCheck)
                {
                    button.onClick.AddListener(() =>
                    {
                        playerConversant.SelectChoice(choice);
                        AudioManager.instance.PlaySFX("Choice");
                    });
                } else if (choice.GetChildren().Count >= 1 && choice.isCheck)
                {
                    button.onClick.AddListener(() =>
                    {                       
                        playerConversant.SkillCheck(choice, choice.statToCheck, choice.skillCheck);                       
                    });
                }
                else
                {
                    button.onClick.AddListener(() =>
                    {
                        playerConversant.SelectChoice(choice);
                        AudioManager.instance.PlaySFX("Choice");
                        playerConversant.Quit();
                    });
                }
            }
        }
    }
}
