using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using RPG.Control;
using UnityEngine;

namespace RPG.Dialogue
{
    public class AIConversant : MonoBehaviour, IRaycastable
    {
        [SerializeField] Dialogue dialogue = null; // 对话的数据对象
        [SerializeField] string conversantName; // 对话者的名称

        public CursorType GetCursorType()
        {
            return CursorType.Dialogue; // 返回光标类型为对话
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (dialogue == null)
            {
                return false;
            }

            Health health = GetComponent<Health>();
            if (health && health.IsDead()) return false; // 如果对话者已死亡，则无法对话

            if (Input.GetMouseButtonDown(0))
            {
                callingController.GetComponent<PlayerConversant>().StartDialogue(this, dialogue); // 开始与对话者进行对话
            }
            return true; // 成功处理射线交互
        }

        public string GetName()
        {
            return conversantName; // 返回对话者的名称
        }
    }
}
