using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

[CreateAssetMenu(fileName = "BasicPotion", menuName = "Game Data/Basic/Potion")]
public class Basic_Potion_Cateragy : ScriptableObject
{
    [Header("基础信息")]
    [Tooltip("显示名")]
    public string itemName;

    [Tooltip("唯一 ID")]
    public int itemId = 0;

    [Header("类别标签")]
    public ItemCategory category = ItemCategory.Potion;

    [Header("药剂属性（合成后结果，可作为模版）")]
    [Range(0, 100)] public float externalInjury = 0f; // 外伤
    [Range(0, 100)] public float internalInjury = 0f; // 内伤
    [Range(0, 100)] public float mental = 0f; // 精神
}
