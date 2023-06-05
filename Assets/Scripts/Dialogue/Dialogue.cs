using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RPG.Dialogue
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 0)]
    public class Dialogue : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField]
        List<DialogueNode> nodes = new List<DialogueNode>(); // 对话的节点列表
        [SerializeField]
        Vector2 newNodeOffset = new Vector2(250, 0); // 新节点的偏移量

        Dictionary<string, DialogueNode> nodeLookup = new Dictionary<string, DialogueNode>(); // 节点名称与节点对象的字典

        private void OnValidate()
        {
            nodeLookup.Clear();
            foreach (DialogueNode node in GetAllNodes())
            {
                nodeLookup[node.name] = node; // 构建节点名称与节点对象的字典
            }
        }

        public IEnumerable<DialogueNode> GetAllNodes()
        {
            return nodes; // 返回所有对话节点的集合
        }

        public DialogueNode GetRootNode()
        {
            return nodes[0]; // 返回根节点
        }

        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode)
        {
            foreach (string childID in parentNode.GetChildren())
            {
                if (nodeLookup.ContainsKey(childID))
                {
                    yield return nodeLookup[childID]; // 返回指定节点的所有子节点
                }
            }
        }

        public IEnumerable<DialogueNode> GetPlayerChildren(DialogueNode currentNode)
        {
            foreach (DialogueNode node in GetAllChildren(currentNode))
            {
                if (node.IsPlayerSpeaking())
                {
                    yield return node; // 返回指定节点的玩家对白子节点
                }
            }
        }

        public IEnumerable<DialogueNode> GetAIChildren(DialogueNode currentNode)
        {
            foreach (DialogueNode node in GetAllChildren(currentNode))
            {
                if (!node.IsPlayerSpeaking())
                {
                    yield return node; // 返回指定节点的NPC对白子节点
                }
            }
        }

#if UNITY_EDITOR
        public void CreateNode(DialogueNode parent)
        {
            DialogueNode newNode = MakeNode(parent);
            Undo.RegisterCreatedObjectUndo(newNode, "Created Dialogue Node");
            Undo.RecordObject(this, "Added Dialogue Node");
            AddNode(newNode); // 创建节点
        }

        public void DeleteNode(DialogueNode nodeToDelete)
        {
            Undo.RecordObject(this, "Deleted Dialogue Node");
            nodes.Remove(nodeToDelete); // 删除节点
            OnValidate();
            CleanDanglingChildren(nodeToDelete);
            Undo.DestroyObjectImmediate(nodeToDelete);
        }

        private DialogueNode MakeNode(DialogueNode parent)
        {
            DialogueNode newNode = CreateInstance<DialogueNode>();
            newNode.name = Guid.NewGuid().ToString();
            if (parent != null)
            {
                parent.AddChild(newNode.name);
                newNode.SetPlayerSpeaking(!parent.IsPlayerSpeaking());
                newNode.SetPosition(parent.GetRect().position + newNodeOffset); // 设置新节点的位置
            }

            return newNode;
        }

        private void AddNode(DialogueNode newNode)
        {
            nodes.Add(newNode); // 添加节点到对话列表
            OnValidate();
        }

        private void CleanDanglingChildren(DialogueNode nodeToDelete)
        {
            foreach (DialogueNode node in GetAllNodes())
            {
                node.RemoveChild(nodeToDelete.name); // 清理被删除节点的所有子节点的引用
            }
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
                        AssetDatabase.AddObjectToAsset(node, this); // 将节点添加到对话对象中
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
