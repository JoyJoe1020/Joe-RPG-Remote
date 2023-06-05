using RPG.Combat; // 导入战斗系统命名空间
using RPG.Movement; // 导入移动系统命名空间
using UnityEngine;
using RPG.Attributes; // 导入属性系统命名空间
using System; // 导入System命名空间
using UnityEngine.EventSystems; // 导入Unity UI事件系统命名空间
using UnityEngine.AI; // 导入Unity导航系统命名空间
using GameDevTV.Inventories; // 导入物品系统命名空间

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health; // 玩家的生命值组件
        ActionStore actionStore; // 玩家的行动存储组件

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type; // 光标类型
            public Texture2D texture; // 光标的纹理
            public Vector2 hotspot; // 光标的热点
        }

        [SerializeField] CursorMapping[] cursorMappings = null; // 光标映射数组
        [SerializeField] float maxNavMeshProjectionDistance = 1f; // 导航网格投射的最大距离
        [SerializeField] float raycastRadius = 1f; // 射线半径
        [SerializeField] int numberOfAbilities = 6; // 技能数量

        bool isDraggingUI = false; // 是否拖拽UI

        private void Awake()
        {
            health = GetComponent<Health>(); // 获取生命值组件
            actionStore = GetComponent<ActionStore>(); // 获取行动存储组件
        }

        private void Update()
        {
            if (InteractWithUI()) return; // 如果与UI交互，返回
            if (health.IsDead())
            {
                SetCursor(CursorType.None); // 如果玩家死亡，设置光标类型为无
                return;
            }

            UseAbilities(); // 使用技能

            if (InteractWithComponent()) return; // 如果与组件交互，返回
            if (InteractWithMovement()) return; // 如果与移动交互，返回

            SetCursor(CursorType.None); // 设置光标类型为无
        }

        private bool InteractWithUI()
        {
            if (Input.GetMouseButtonUp(0))
            {
                isDraggingUI = false;
            }
            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isDraggingUI = true;
                }
                SetCursor(CursorType.UI); // 设置光标类型为UI
                return true;
            }
            if (isDraggingUI)
            {
                return true;
            }
            return false;
        }

        private void UseAbilities()
        {
            for (int i = 0; i < numberOfAbilities; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    actionStore.Use(i, gameObject); // 使用技能
                }
            }
        }

        private bool InteractWithComponent()
        {
            RaycastHit[] hits = RaycastAllSorted(); // 射线投射并排序
            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>(); // 获取被射线击中物体上的所有IRaycastable组件
                foreach (IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this))
                    {
                        SetCursor(raycastable.GetCursorType()); // 设置光标类型
                        return true;
                    }
                }
            }
            return false;
        }

        RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(), raycastRadius); // 使用球形射线投射
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits); // 根据距离对射线击中的物体进行排序
            return hits;
        }

        private bool InteractWithMovement()
        {
            Vector3 target;
            bool hasHit = RaycastNavMesh(out target); // 射线投射到导航网格上
            if (hasHit)
            {
                if (!GetComponent<Mover>().CanMoveTo(target)) return false; // 如果无法移动到目标位置，返回false

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(target, 1f); // 开始移动到目标位置
                }
                SetCursor(CursorType.Movement); // 设置光标类型为移动
                return true;
            }
            return false;
        }

        private bool RaycastNavMesh(out Vector3 target)
        {
            target = new Vector3();

            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit); // 射线投射
            if (!hasHit) return false;

            NavMeshHit navMeshHit;
            bool hasCastToNavMesh = NavMesh.SamplePosition(
                hit.point, out navMeshHit, maxNavMeshProjectionDistance, NavMesh.AllAreas); // 将射线击中的点投射到导航网格上
            if (!hasCastToNavMesh) return false;

            target = navMeshHit.position;

            return true;
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto); // 设置光标纹理和热点
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }

        public static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition); // 获取鼠标射线
        }
    }
}
