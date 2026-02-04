using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class EventManager
{
    public static event Action<Herb> AddHerbEvent;
    public static event Action<Medicine> AddMedicineEvent;//仓库增加物品事件



    public static void CallAddHerbEvent(Herb herb)
    {
        AddHerbEvent?.Invoke(herb);
    }
    public static void CallAddMedicineEvent(Medicine medicine)
    {
        AddMedicineEvent?.Invoke(medicine);
    }//仓库增加物品事件激活函数
}
