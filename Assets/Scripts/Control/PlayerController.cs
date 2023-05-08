using UnityEngine;

// ����һ����ΪPlayerController���࣬�̳���MonoBehaviour�����ڴ�����ҿ���
public class PlayerController : MonoBehaviour
{

    // Update������ÿһ֡�б�����
    private void Update()
    {
        // ��������������ʱ
        if (Input.GetMouseButton(0))
        {
            // ����MoveToCursor������ʹ��Ϸ�����ƶ����������λ��
            MoveToCursor();
        }
    }

    // ����һ��˽�з���MoveToCursor�����ڼ����������λ�ò��ƶ���Ϸ����
    private void MoveToCursor()
    {
        // �����������Ļ�ϵ�λ�ü���һ�����ߣ�������洢��ray������
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // ����һ��RaycastHit���͵ı���hit�����ڴ洢����������Ľ�����Ϣ
        RaycastHit hit;
        // ʹ��Physics.Raycast������������Ƿ��볡���е����巢����ײ����������洢��hasHit������
        bool hasHit = Physics.Raycast(ray, out hit);
        // ������������巢����ײ��hasHitΪtrue��
        if (hasHit)
        {
            // ��ȡ��ǰ��Ϸ�����Mover���
            // ����Mover�����MoveTo��������Ŀ��λ������Ϊ����������Ľ��㣨hit.point��
            GetComponent<Mover>().MoveTo(hit.point);
        }
    }
}
