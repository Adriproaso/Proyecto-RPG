using RPG.Core;
using RPG.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField]
        string playerName;

        Dialogue currentDialogue;
        DialogueNode currentNode = null;
        AIConversant currentConversant = null;
        bool isChoosing = false;
        StatSheet stats;

        public event Action onConversationUpdated;

        private void Awake()
        {
            stats = GetComponent<StatSheet>();
        }

        public void StartDialogue(AIConversant newConversant, Dialogue newDialogue)
        {
            Debug.Log("Dialogo Empezado: " + newDialogue);
            currentConversant = newConversant;
            currentDialogue = newDialogue;

            //Busca rootNodes y entra por uno aleatorio que cumpla los requisitos
            DialogueNode[] rootNodes = FilterOnCondition(currentDialogue.GetRootNode()).ToArray();
            int randomIndex = UnityEngine.Random.Range(0, rootNodes.Count());
            currentNode = rootNodes[randomIndex];

            TriggerEnterAction();
            onConversationUpdated();
        }

        public void Quit()
        {
            Debug.Log("Dialogo Terminado: " + currentDialogue);
            currentDialogue = null;
            TriggerExitAction();
            currentConversant = null;
            currentNode = null;
            isChoosing = false;
            onConversationUpdated();
        }

        public bool IsChoosing()
        {
            return isChoosing;
        }

        public string GetText()
        {
            if (currentDialogue == null)
            {
                return "";
            }

            return currentNode.GetText();
        }

        public IEnumerable<DialogueNode> GetChoices()
        {
            return FilterOnCondition(currentDialogue.GetPlayerChildren(currentNode));
        }

        public string GetCurrentConversantName()
        {
            if (isChoosing)
            {
                return playerName;
            }
            else
            {
                return currentConversant.GetName();
            }
        }

        public void SelectChoice(DialogueNode chosenNode)
        {
            currentNode = chosenNode;
            TriggerEnterAction();
            isChoosing = false;
            Next();
        }

        public void SkillCheck(DialogueNode checkNode, StatSheet.Stats statToCheck, int checkDifficulty)
        {
            Debug.Log("Haciendo Skillcheck de: " + statToCheck);
            //Saca valor random del d12 y lo junta con el stat
            int random = UnityEngine.Random.Range(1, 13);
            Debug.Log("Random: " + random);
            Debug.Log("Juntos: " + (random + stats.GetValue(statToCheck.ToString())));
            random += stats.GetValue(statToCheck.ToString());

            //Quita la eleccion
            isChoosing = false;
            currentNode = checkNode;

            //Si pasa el check entra en el primer hijo del dialogo
            if (random >= checkDifficulty)
            {
                DialogueNode[] children = FilterOnCondition(currentDialogue.GetAIChildren(checkNode)).ToArray();
                int randomIndex = UnityEngine.Random.Range(0, children.Count());
                //Añade experiencia segun lo dificil que sea la tirada
                stats.AddXP(checkDifficulty/3, statToCheck);
                AudioManager.instance.PlaySFX("CheckPass");
                TriggerExitAction();
                currentNode = children[0];
                TriggerEnterAction();
                onConversationUpdated();
            } 
            //Si lo falla entra en el segundo
            else
            {
                DialogueNode[] children = FilterOnCondition(currentDialogue.GetAIChildren(checkNode)).ToArray();
                int randomIndex = UnityEngine.Random.Range(0, children.Count());
                AudioManager.instance.PlaySFX("CheckFail");
                TriggerExitAction();
                currentNode = children[1];
                TriggerEnterAction();
                onConversationUpdated();
            }
        }

        public bool IsActive()
        {
            return currentDialogue != null;
        }

        public void Next()
        {
            int numberPlayerResponses = FilterOnCondition(currentDialogue.GetPlayerChildren(currentNode)).Count();
            if (numberPlayerResponses > 0)
            {
                isChoosing = true;
                TriggerExitAction();
                onConversationUpdated();
                return;
            }
           
            if (HasNext())
            {
                DialogueNode[] children = FilterOnCondition(currentDialogue.GetAIChildren(currentNode)).ToArray();
                int randomIndex = UnityEngine.Random.Range(0, children.Count());
                TriggerExitAction();
                currentNode = children[randomIndex];
                TriggerEnterAction();
                onConversationUpdated();

                if (currentNode.GetText() == "")
                {
                    Next();
                }
            }           
        }

        public bool HasNext()
        {
            return FilterOnCondition(currentDialogue.GetAllChildren(currentNode)).Count() > 0;
        }

        private IEnumerable<DialogueNode> FilterOnCondition(IEnumerable<DialogueNode> inputNode)
        {
            foreach (var node in inputNode)
            {
                if (node.CheckCondition(GetEvaluators()))
                {
                    yield return node;
                }
            }
        }

        private IEnumerable<IPredicateEvaluate> GetEvaluators()
        {
            return GetComponents<IPredicateEvaluate>();
        }

        private void TriggerEnterAction()
        {
            if (currentNode != null && currentNode.GetOnEnterAction() != "")
            {
                TriggerAction(currentNode.GetOnEnterAction());
            }
        }

        private void TriggerExitAction()
        {
            if (currentNode != null && currentNode.GetOnExitAction() != "")
            {
                TriggerAction(currentNode.GetOnExitAction());
            }
        }

        private void TriggerAction(string action)
        {
            if (action == "") return;

            foreach (DialogueTrigger trigger in currentConversant.GetComponents<DialogueTrigger>())
            {
                trigger.Trigger(action);
            }
        }
    }
}
