using System;
using UnityEngine;

[Serializable]
public class Potion
{
    // 三种属性：咳嗽、头痛、牙痛，对应 Item 中的三个布尔症状
    public float coughReduce = 0f;
    public float headacheReduce = 0f;
    public float toothacheReduce = 0f;

    public Potion() { }

    public Potion(float cough, float headache, float toothache)
    {
        coughReduce = cough;
        headacheReduce = headache;
        toothacheReduce = toothache;
    }

    public void ApplyTo(Patient patient)
    {
        if (patient == null) return;
        patient.ReduceCough(coughReduce);
        patient.ReduceHeadache(headacheReduce);
        patient.ReduceToothache(toothacheReduce);
    }
}
