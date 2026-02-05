using System;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class InventoryManager : MonoBehaviour
{
    [Header("传入药草数据")]
    [SerializeField]private HerbSQ herbData;
    [Header("传入药物数据")]
    [SerializeField]private MedicineSQ medicineData;
    
    //还要获取当前天数来确定草药仓库中的药物数量

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
    [Header("草药仓库数据")]
    [SerializeField]List<Herb> herbInventory = new List<Herb>();
    [Header("药品仓库数据")]
    [SerializeField]List<Medicine> medicineInventory = new List<Medicine>();//创建两个仓库列表

    void Start()
    {
        int maxHerbNumb;//当前天数最大的草药数目(限制草药的种类)
        maxHerbNumb = herbData.getHerbList.Count;
        for(int i = 0 ; i < 8 ; i ++)
        {
            if( i< maxHerbNumb)
                AddHerb(herbData.getHerbList[i]);
            else
                AddHerb(herbData.getHerbList[0]);
        }



        int maxMedicineNumb = 3;//当前仓库上限



        for(int i = 0 ; i < maxMedicineNumb ; i ++)
        {
            AddMedicine(medicineData.getMedicinesList[i]);
        }
        /*应该是当前仓库上限时拒绝合成
        这里为了测试就直接把药物列表中所有东西都拿来了*/


        EventManager.CallMedicineInventoryUPdate();
        EventManager.CallHerbInventoryUpdate();
    }

    void AddHerb(Herb herb)
    {
        herbInventory.Add(herb);
    }
    void AddMedicine(Medicine medicine)
    {
        medicineInventory.Add(medicine);
    }
    
    public Medicine GetMedicine(int index)
    {
        return medicineInventory[index];
    }
    public Herb GetHerb(int index)
    {
        return herbInventory[index];
    }
}
