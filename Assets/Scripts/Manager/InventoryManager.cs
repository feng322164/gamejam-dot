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
    [SerializeField]private HerbSQ assistHerbData;
    //还要获取当前天数来确定草药仓库中的药物数量
    int medicineIndex = 0;
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
    [Header("副药仓库数据")]
    [SerializeField]List<Herb> assistHerbInventory;
    void Start()
    {
        int maxHerbNumb;//当前天数最大的草药数目(限制草药的种类)
        maxHerbNumb = herbData.getHerbList.Count - 1;
        for(int i = 0 ; i < 8 ; i ++)
        {
            if( i< maxHerbNumb)
                AddHerb(herbData.getHerbList[i+1]);
            else
                AddHerb(herbData.getHerbList[0]);
        }



        int maxMedicineNumb = 3;//当前仓库上限



        for(int i = 0 ; i < maxMedicineNumb ; i ++)
        {
            AddMedicine(medicineData.getMedicinesList[0]);
        }
        /*应该是当前仓库上限时拒绝合成
        这里为了测试就直接把药物列表中所有东西都拿来了*/

        int maxAssistHerbNum = 6;
        for(int i = 0 ; i < maxAssistHerbNum  ; i++)
        {
            if (assistHerbData.getHerbList.Count - 1 >i)
                AddAssistHerb(assistHerbData.getHerbList[i + 1]);
            else
                AddAssistHerb(assistHerbData.getHerbList[0]);
        }//副药的列表



        EventManager.CallMedicineInventoryUPdate();
        EventManager.CallHerbInventoryUpdate();
    }

    void AddHerb(Herb herb)
    {
        herbInventory.Add(herb);
    }
    void AddAssistHerb(Herb herb)
    {
        assistHerbInventory.Add(herb);
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
    public Herb GetAssistHerb(int index)
    {
        return assistHerbInventory[index];
    }
    public void DeleteMedicine(Medicine medicine , int i)
    {
        medicineInventory[i] = medicine;
        EventManager.CallMedicineInventoryUPdate();
    }
    public void AddComebineMedicine(Medicine medicine)
    {
        for (int i = 0; i < medicineInventory.Count; i++)
        {
            if (medicineInventory[i].getMedicineName == "空")
            {
                medicineInventory [i] = medicine;
                return;
            }
        }
        medicineInventory[medicineIndex] = medicine;
        medicineIndex++;
    }
}
