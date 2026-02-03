using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    // 简单合成示例：主药材决定基础数值，副药材用于调整属性比例，瓶子决定放大系数

    public Potion Craft(Item mainIngredient, Item secondaryIngredient, BottleSize bottle)
    {
        if (mainIngredient == null) return new Potion();

        // 基础值由主药材的症状决定（示例：每个匹配 symptom 增加固定值）
        float baseValue = 25f; // 基础治疗量
        float cough = mainIngredient.getCough ? baseValue : 0f;
        float headache = mainIngredient.getHeadache ? baseValue : 0f;
        float toothache = mainIngredient.getToothache ? baseValue : 0f;

        // 副药材用于微调，例如如果副药材有某 symptom，则把对应属性乘以 1.5
        if (secondaryIngredient != null)
        {
            if (secondaryIngredient.getCough) cough *= 1.5f;
            if (secondaryIngredient.getHeadache) headache *= 1.5f;
            if (secondaryIngredient.getToothache) toothache *= 1.5f;
        }

        // 瓶子放大：Small x1, Medium x1.5, Large x2
        float sizeMult = 1f;
        switch (bottle)
        {
            case BottleSize.Medium: sizeMult = 1.5f; break;
            case BottleSize.Large: sizeMult = 2f; break;
            default: sizeMult = 1f; break;
        }

        return new Potion(cough * sizeMult, headache * sizeMult, toothache * sizeMult);
    }
}
