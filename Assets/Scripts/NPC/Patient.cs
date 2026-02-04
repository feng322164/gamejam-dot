using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : MonoBehaviour
{
    [Header("Symptom Levels (0-100)")]
    public float coughLevel = 100f;
    public float headacheLevel = 100f;
    public float toothacheLevel = 100f;

    // 应用药剂

    // 各项减弱接口，值不能低于 0
    public void ReduceCough(float amount)
    {
        coughLevel = Mathf.Max(0f, coughLevel - amount);
    }

    public void ReduceHeadache(float amount)
    {
        headacheLevel = Mathf.Max(0f, headacheLevel - amount);
    }

    public void ReduceToothache(float amount)
    {
        toothacheLevel = Mathf.Max(0f, toothacheLevel - amount);
    }

    // 判断是否已痊愈（所有症状为0）
    public bool IsHealed()
    {
        return coughLevel <= 0f && headacheLevel <= 0f && toothacheLevel <= 0f;
    }
}
