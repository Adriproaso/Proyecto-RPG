using RPG.Core;
using RPG.Stats;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RPG.Dialogue
{
    public class DialogueNode : ScriptableObject
    {
        enum speaker
        {
            player,
            companion,
            npcs
        }

        [SerializeField]
        speaker currentSpeaker;
        [SerializeField] 
        string text;
        [SerializeField]
        public bool isRootNode;
        [SerializeField]
        List<string> children = new List<string>();
        [SerializeField]
        Rect rect = new Rect(0, 0, 200, 100);
        [SerializeField]
        public bool isCheck;
        [SerializeField]
        public StatSheet.Stats statToCheck;
        [SerializeField]
        public int skillCheck;
        [SerializeField]
        string onEnterAction;
        [SerializeField]
        string onExitAction;
        [SerializeField]
        Condition condition;
        

        public Rect GetRect()
        {
            return rect;
        }

        public string GetText()
        {
            return text;          
        }

        public List<string> GetChildren()
        {
            return children;
        }

        public string WhoIsSpeaking()
        {
            return currentSpeaker.ToString();
        }

        public string GetOnEnterAction()
        {
            return onEnterAction;
        }

        public string GetOnExitAction()
        {
            return onExitAction;
        }

        public bool CheckCondition(IEnumerable<IPredicateEvaluate> evaluators)
        {
            return condition.Check(evaluators);
        }

#if UNITY_EDITOR
        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "Move Dialogue Node");

            rect.position = newPosition;

            EditorUtility.SetDirty(this);
        }

        public void SetText(string newText)
        {
            if (newText != text && isCheck)
            {
                Undo.RecordObject(this, "Update Dialogue Text");

                text = "[" + statToCheck +"]" + newText;
                EditorUtility.SetDirty(this);
            }
            else if (newText != text)
            {
                Undo.RecordObject(this, "Update Dialogue Text");

                text = newText;
                EditorUtility.SetDirty(this);
            }
        }

        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Linked Dialogue Node");

            children.Add(childID);
            EditorUtility.SetDirty(this);
        }

        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Unlinked Dialogue Node");

            children.Remove(childID);
            EditorUtility.SetDirty(this);
        }

        public void SetPlayerSpeaking(string v)
        {
            Undo.RecordObject(this, "Change Dialogue Speaker");
            switch (v)
            {
                case "player":
                    currentSpeaker = speaker.player;
                    break;
                case "companion":
                    currentSpeaker = speaker.companion;
                    break;
                case "npcs":
                    currentSpeaker = speaker.npcs;
                    break;
            }
            EditorUtility.SetDirty(this);
        }
#endif
    }
}