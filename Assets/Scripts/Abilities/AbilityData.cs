using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class AbilityData : IAction
{
    GameObject user; // 使用技能的游戏对象
    Vector3 targetedPoint; // 目标点
    IEnumerable<GameObject> targets; // 目标列表
    bool cancelled = false; // 是否取消技能使用

    // 构造函数，传入使用技能的游戏对象
    public AbilityData(GameObject user)
    {
        this.user = user;
    }

    // 获取目标列表
    public IEnumerable<GameObject> GetTargets()
    {
        return targets;
    }

    // 设置目标列表
    public void SetTargets(IEnumerable<GameObject> targets)
    {
        this.targets = targets;
    }

    // 获取目标点
    public Vector3 GetTargetedPoint()
    {
        return targetedPoint;
    }

    // 设置目标点
    public void SetTargetedPoint(Vector3 targetedPoint)
    {
        this.targetedPoint = targetedPoint;
    }

    // 获取使用技能的游戏对象
    public GameObject GetUser()
    {
        return user;
    }

    // 开始协程
    public void StartCoroutine(IEnumerator coroutine)
    {
        user.GetComponent<MonoBehaviour>().StartCoroutine(coroutine);
    }

    // 取消技能使用
    public void Cancel()
    {
        cancelled = true;
    }

    // 判断技能是否被取消
    public bool IsCancelled()
    {
        return cancelled;
    }
}