using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class EventManager
{
    public static event Action<Herb> AddHerbEvent;
    public static event Action<Medicine> AddMedicineEvent;//仓库增加物品事件
    public static event Action MedicineInventoryUpdateEvent;//药品仓库物品更新事件
    public static event Action HerbInventoryUpdateEvent;//草药仓库物品更新事件

    public static void CallAddHerbEvent(Herb herb)
    {
        AddHerbEvent?.Invoke(herb);
    }
    public static void CallAddMedicineEvent(Medicine medicine)
    {
        AddMedicineEvent?.Invoke(medicine);
    }//仓库增加物品事件激活函数
    public static void CallMedicineInventoryUPdate()
    {
        MedicineInventoryUpdateEvent?.Invoke();
    }//仓库物品更新的激活函数
    public static void CallHerbInventoryUpdate()
    {
        HerbInventoryUpdateEvent?.Invoke();
    }//仓库药草更新激活函数
}
