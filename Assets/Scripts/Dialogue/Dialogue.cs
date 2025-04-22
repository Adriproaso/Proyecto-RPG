using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RPG.Dialogue
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "RPG/Dialogue", order = 0)]
    public class Dialogue : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField]
        List<DialogueNode> nodes = new List<DialogueNode>();

        [SerializeField]
        Vector2 newNodeOffset = new Vector2(250, 0);

        [SerializeField]
        Dictionary<string, DialogueNode> nodeLookup = new Dictionary<string, DialogueNode>();

        private void OnValidate()
        {
            nodeLookup.Clear();
            foreach (DialogueNode node in GetAllNodes())
            {
                nodeLookup[node.name] = node;
            }
        }

        /// <summary>
        /// Recoge todos los nodos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DialogueNode> GetAllNodes()
        {
            return nodes;
        }

        /// <summary>
        /// Recoge todos los nodos marcados como Root
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DialogueNode> GetRootNode()
        {
            foreach (DialogueNode node in nodes)
            {
                if (node.isRootNode)
                {
                    yield return node;
                }
            }
        }

        /// <summary>
        /// Recoge todos los nodos emparentados a un nodo
        /// </summary>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode)
        {
            /*
            List<DialogueNode> result = new List<DialogueNode>();
            foreach (string childID in parentNode.GetChildren())
            {
                if (nodeLookup.ContainsKey(childID))
                {
                    Debug.Log("ALL CHILDREN RESULTS FIND " + childID);
                    yield return nodeLookup[childID];
                }       
            }
            */

            List<DialogueNode> result = new List<DialogueNode>();
            foreach (string childID in parentNode.GetChildren())
            {
                foreach (DialogueNode node in GetAllNodes())
                {
                    if (node.name == childID)
                    {
                        result.Add(node);
                    }
                }
            }
            return result;

        }

        /// <summary>
        /// Recoge todos los nodos del jugador emparentados a un nodo
        /// </summary>
        /// <param name="currentNode"></param>
        /// <returns></returns>
        public IEnumerable<DialogueNode> GetPlayerChildren(DialogueNode currentNode)
        {
            foreach (DialogueNode node in GetAllChildren(currentNode))
            {
                if (node.WhoIsSpeaking() == "player")
                {
                    yield return node;
                }
            }
        }

        /// <summary>
        /// Recoge todos los nodos de IA emparentados a un nodo
        /// </summary>
        /// <param name="currentNode"></param>
        /// <returns></returns>
        public IEnumerable<DialogueNode> GetAIChildren(DialogueNode currentNode)
        {
            foreach (DialogueNode node in GetAllChildren(currentNode))
            {
                if (node.WhoIsSpeaking() != "player")
                {
                    yield return node;
                }
            }
        }

#if UNITY_EDITOR

        public void CreateNode(DialogueNode parent)
        {
            DialogueNode newNode = MakeNode(parent);
            Undo.RegisterCreatedObjectUndo(newNode, "Created Dialogue Node");
            Undo.RecordObject(this, "Create Dialogue Node");
            AddNode(newNode);
        }

        public void DeleteNode(DialogueNode nodeToDelete)
        {
            Undo.RecordObject(this, "Deleted Dialogue Node");
            nodes.Remove(nodeToDelete);
            OnValidate();
            CleanDanglingChildren(nodeToDelete);
            Undo.DestroyObjectImmediate(nodeToDelete);
        }

        private void CleanDanglingChildren(DialogueNode nodeToDelete)
        {
            foreach (DialogueNode node in GetAllNodes())
            {
                node.RemoveChild(nodeToDelete.name);
            }
        }

        private DialogueNode MakeNode(DialogueNode parent)
        {
            DialogueNode newNode = CreateInstance<DialogueNode>();
            newNode.name = Guid.NewGuid().ToString();
            if (parent != null)
            {
                parent.AddChild(newNode.name);
                newNode.SetPlayerSpeaking(parent.WhoIsSpeaking());
                newNode.SetPosition(parent.GetRect().position + newNodeOffset);
            }

            return newNode;
        }

        private void AddNode(DialogueNode newNode)
        {
            nodes.Add(newNode);
            OnValidate();
        }

#endif
        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (nodes.Count == 0)
            {
                DialogueNode newNode = MakeNode(null);
                AddNode(newNode);
            }

            if (AssetDatabase.GetAssetPath(this) != "")
            {
                foreach (DialogueNode node in GetAllNodes())
                {
                    if (AssetDatabase.GetAssetPath(node) == "")
                    {
                        AssetDatabase.AddObjectToAsset(node, this);
                    }
                }
            }
#endif
        }

        public void OnAfterDeserialize()
        {

        }
    }
}