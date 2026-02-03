using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

[CreateAssetMenu(fileName = "BasicHerbal", menuName = "Game Data/Basic/Herbal")]
public class Basic_Herbal_Category : ScriptableObject
{
    [Header("基础信息")]
    [Tooltip("显示名")]
    public string itemName;

    [Tooltip("唯一 ID")]
    public int itemId = 0;

    [Header("类别标签")]
    public ItemCategory category = ItemCategory.Herbal;

    [Header("属性（用于配方/影响）")]
    [Range(0, 100)] public float externalInjury = 0f; // 外伤
    [Range(0, 100)] public float internalInjury = 0f; // 内伤
    [Range(0, 100)] public float mental = 0f; // 精神
}
