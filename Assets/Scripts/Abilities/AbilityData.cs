using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class AbilityData : IAction
{
    GameObject user; // ʹ�ü��ܵ���Ϸ����
    Vector3 targetedPoint; // Ŀ���
    IEnumerable<GameObject> targets; // Ŀ���б�
    bool cancelled = false; // �Ƿ�ȡ������ʹ��

    // ���캯��������ʹ�ü��ܵ���Ϸ����
    public AbilityData(GameObject user)
    {
        this.user = user;
    }

    // ��ȡĿ���б�
    public IEnumerable<GameObject> GetTargets()
    {
        return targets;
    }

    // ����Ŀ���б�
    public void SetTargets(IEnumerable<GameObject> targets)
    {
        this.targets = targets;
    }

    // ��ȡĿ���
    public Vector3 GetTargetedPoint()
    {
        return targetedPoint;
    }

    // ����Ŀ���
    public void SetTargetedPoint(Vector3 targetedPoint)
    {
        this.targetedPoint = targetedPoint;
    }

    // ��ȡʹ�ü��ܵ���Ϸ����
    public GameObject GetUser()
    {
        return user;
    }

    // ��ʼЭ��
    public void StartCoroutine(IEnumerator coroutine)
    {
        user.GetComponent<MonoBehaviour>().StartCoroutine(coroutine);
    }

    // ȡ������ʹ��
    public void Cancel()
    {
        cancelled = true;
    }

    // �жϼ����Ƿ�ȡ��
    public bool IsCancelled()
    {
        return cancelled;
    }
}