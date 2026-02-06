using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class HerbInventory : MonoBehaviour
{

    public InventoryManager inventoryManager;
    [SerializeField]List<Button> buttons;//传入按钮
    [SerializeField]Image detailUI;
    [SerializeField]TextMeshProUGUI herbName;
    [SerializeField]TextMeshProUGUI herbDetail;
    [SerializeField]List<TextMeshProUGUI> textMeshProUGUIs;//传入按钮的文本
    [SerializeField] List<Button> assistHerbButtons;
    [SerializeField] List<TextMeshProUGUI> assistHerbtextMeshProUGUIs;
    int lastIndex = -1,assLastIndex = -1;
    void OnEnable()
    {
        EventManager.HerbInventoryUpdateEvent += UpdateUI;
    }
    void OnDisable()
    {
        EventManager.HerbInventoryUpdateEvent -= UpdateUI;
    }

    void UpdateUI()
    {
        for(int i = 0 ; i < 8 ; i++ )
        {
            buttons[i].image.sprite = inventoryManager.GetHerb(i).getHerbSprite;
            textMeshProUGUIs[i].text = inventoryManager.GetHerb(i).getHerbName;
        }
        for (int i = 0; i < 6; i++) 
        {
            assistHerbButtons[i].image.sprite = inventoryManager.GetAssistHerb(i).getHerbSprite;
            assistHerbtextMeshProUGUIs[i].text = inventoryManager.GetAssistHerb(i).getHerbName;
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
            herbName.text = inventoryManager.GetHerb(i).getHerbName;
            herbDetail.text = inventoryManager.GetHerb(i).getHerbDetail;
            lastIndex = i;
            
        }
        else
        {
            detailUI.gameObject.SetActive(false);
            EventManager.CallSeleckedMainHerb(i);
            lastIndex = -1;
        }

    }//点击，选中的逻辑

    public void OnClickAssistHerbButton(int i)
    {
        if (assLastIndex != i)
        {
            detailUI.gameObject.SetActive(true);
            herbName.text = inventoryManager.GetAssistHerb(i).getHerbName;
            herbDetail.text = inventoryManager.GetAssistHerb(i).getHerbDetail;
            assLastIndex = i;

        }
        else
        {
            detailUI.gameObject.SetActive(false);
            if(inventoryManager.GetAssistHerb(i).getHerbName != "空")
                EventManager.CallSeleckedAssistHerb(i);
            assLastIndex = -1;
        }

    }//点击，选中的逻辑
}
