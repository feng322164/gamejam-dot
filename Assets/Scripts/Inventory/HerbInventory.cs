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
    int lastIndex = -1;
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
    public void OnSeleckedButton()
    {
        
    }
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

            lastIndex = -1;
        }

    }//点击，选中的逻辑

    private void OnButtonHoverEnter(int buttonIndex)
    {
        Debug.Log("jru");
    }
    private void OnButtonHoverExit(int buttonIndex)
    {

    }
}
