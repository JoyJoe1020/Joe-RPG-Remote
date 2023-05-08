using UnityEngine;

public class PlayerController : MonoBehaviour {

    private void Update() {
        //������������סʱ
        if(Input.GetMouseButton(0))
        {
           // ����MoveToCursor������ʹ��Ϸ�����ƶ����������λ��
            MoveToCursor();
        }
    }

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
            GetComponent<Mover>().MoveTo(hit.point);
        }
    }
}