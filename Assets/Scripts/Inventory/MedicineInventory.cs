
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class MedicineInventory : MonoBehaviour
{

    public InventoryManager inventoryManager;
    [SerializeField]List<Button> buttons;//传入按钮
    [SerializeField]Image detailUI;
    [SerializeField]TextMeshProUGUI medicineName;
    [SerializeField]TextMeshProUGUI medicineDetail;
    [SerializeField]List<TextMeshProUGUI> textMeshProUGUIs;//传入按钮的文本
    int lastIndex = -1;
    void OnEnable()
    {
        EventManager.MedicineInventoryUpdateEvent += UpdateUI;
    }
    void OnDisable()
    {
        EventManager.MedicineInventoryUpdateEvent -= UpdateUI;
    }

    void UpdateUI()
    {
        for(int i = 0 ; i < 3 ; i++ )
        {
            buttons[i].image.sprite = inventoryManager.GetMedicine(i).getMedicineSprite;
            textMeshProUGUIs[i].text = inventoryManager.GetMedicine(i).getMedicineName;
        }
    }//仓库更新的时候更新UI


    void Start()
    {
        detailUI.gameObject.SetActive(false);
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            detailUI.gameObject.SetActive(false);
        }
    }//当Z键按下后

    public void OnClickButton(int i)
    {
        if(lastIndex != i)
        {
            detailUI.gameObject.SetActive(true);
            medicineName.text = inventoryManager.GetMedicine(i).getMedicineName;
            medicineDetail.text = inventoryManager.GetMedicine(i).getMedicineDetail;
            lastIndex = i;
        }
        else
        {
            detailUI.gameObject.SetActive(false);
            if (inventoryManager.GetMedicine(i).getMedicineName != "空")
                EventManager.CallSeleckedMedicine(i);
            lastIndex = -1;
        }

    }//点击，选中的逻辑

}
