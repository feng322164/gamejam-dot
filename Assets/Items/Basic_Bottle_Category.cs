using UnityEngine;

[CreateAssetMenu(fileName = "BasicBottle", menuName = "Game Data/Basic/Bottle")]
public class Basic_Bottle_Category : ScriptableObject
{
    [Header("基础信息")]
    [Tooltip("显示名")]
    public string itemName;

    [Tooltip("唯一 ID")]
    public int itemId = 0; // 默认 ID

    [Header("类别标签")]
    public ItemCategory category = ItemCategory.Bottle;

    [Header("瓶子信息")]
    public string bottle_cateragy;
    public int bottle_id = 0; // 默认瓶子ID为0;

    [Header("属性（用于合成/显示）")]
    [Range(0, 100)] public float externalInjury = 0f; // 外伤
    [Range(0, 100)] public float internalInjury = 0f; // 内伤
    [Range(0, 100)] public float mental = 0f; // 精神
}
