using System;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class InventoryManager : MonoBehaviour
{
    [Header("传入所有的草药数据")]
    public List<Herb> herbs;
    [Header("传入所有的药品数据")]
    public List<Medicine> medicines;
    void OnEnable()
    {
        EventManager.AddHerbEvent += AddHerb;
        EventManager.AddMedicineEvent += AddMedicine;
    }
    void OnDisable()
    {
        EventManager.AddHerbEvent -= AddHerb;
        EventManager.AddMedicineEvent -= AddMedicine;
    }//仓库增加物品事件
    
    [SerializeField]List<Herb> herbInventory = new List<Herb>();
    [SerializeField]List<Medicine> medicineInventory = new List<Medicine>();//创建两个仓库列表

    void AddHerb(Herb herb)
    {
        herbInventory.Add(herb);
    }
    void AddMedicine(Medicine medicine)
    {
        medicineInventory.Add(medicine);
    }
}
