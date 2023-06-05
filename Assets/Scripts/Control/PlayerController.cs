using RPG.Combat; // ����ս��ϵͳ�����ռ�
using RPG.Movement; // �����ƶ�ϵͳ�����ռ�
using UnityEngine;
using RPG.Attributes; // ��������ϵͳ�����ռ�
using System; // ����System�����ռ�
using UnityEngine.EventSystems; // ����Unity UI�¼�ϵͳ�����ռ�
using UnityEngine.AI; // ����Unity����ϵͳ�����ռ�
using GameDevTV.Inventories; // ������Ʒϵͳ�����ռ�

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health; // ��ҵ�����ֵ���
        ActionStore actionStore; // ��ҵ��ж��洢���

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type; // �������
            public Texture2D texture; // ��������
            public Vector2 hotspot; // �����ȵ�
        }

        [SerializeField] CursorMapping[] cursorMappings = null; // ���ӳ������
        [SerializeField] float maxNavMeshProjectionDistance = 1f; // ��������Ͷ���������
        [SerializeField] float raycastRadius = 1f; // ���߰뾶
        [SerializeField] int numberOfAbilities = 6; // ��������

        bool isDraggingUI = false; // �Ƿ���קUI

        private void Awake()
        {
            health = GetComponent<Health>(); // ��ȡ����ֵ���
            actionStore = GetComponent<ActionStore>(); // ��ȡ�ж��洢���
        }

        private void Update()
        {
            if (InteractWithUI()) return; // �����UI����������
            if (health.IsDead())
            {
                SetCursor(CursorType.None); // ���������������ù������Ϊ��
                return;
            }

            UseAbilities(); // ʹ�ü���

            if (InteractWithComponent()) return; // ������������������
            if (InteractWithMovement()) return; // ������ƶ�����������

            SetCursor(CursorType.None); // ���ù������Ϊ��
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
                SetCursor(CursorType.UI); // ���ù������ΪUI
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
                    actionStore.Use(i, gameObject); // ʹ�ü���
                }
            }
        }

        private bool InteractWithComponent()
        {
            RaycastHit[] hits = RaycastAllSorted(); // ����Ͷ�䲢����
            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>(); // ��ȡ�����߻��������ϵ�����IRaycastable���
                foreach (IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this))
                    {
                        SetCursor(raycastable.GetCursorType()); // ���ù������
                        return true;
                    }
                }
            }
            return false;
        }

        RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(), raycastRadius); // ʹ����������Ͷ��
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits); // ���ݾ�������߻��е������������
            return hits;
        }

        private bool InteractWithMovement()
        {
            Vector3 target;
            bool hasHit = RaycastNavMesh(out target); // ����Ͷ�䵽����������
            if (hasHit)
            {
                if (!GetComponent<Mover>().CanMoveTo(target)) return false; // ����޷��ƶ���Ŀ��λ�ã�����false

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(target, 1f); // ��ʼ�ƶ���Ŀ��λ��
                }
                SetCursor(CursorType.Movement); // ���ù������Ϊ�ƶ�
                return true;
            }
            return false;
        }

        private bool RaycastNavMesh(out Vector3 target)
        {
            target = new Vector3();

            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit); // ����Ͷ��
            if (!hasHit) return false;

            NavMeshHit navMeshHit;
            bool hasCastToNavMesh = NavMesh.SamplePosition(
                hit.point, out navMeshHit, maxNavMeshProjectionDistance, NavMesh.AllAreas); // �����߻��еĵ�Ͷ�䵽����������
            if (!hasCastToNavMesh) return false;

            target = navMeshHit.position;

            return true;
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto); // ���ù��������ȵ�
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
            return Camera.main.ScreenPointToRay(Input.mousePosition); // ��ȡ�������
        }
    }
}
