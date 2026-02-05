using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class MedicineInventory : MonoBehaviour
{
    public InventoryManager inventoryManager;
    [SerializeField]List<Button> buttons;

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
        }
    }//仓库更新的时候更新UI

    public void OnSeleckedButton()
    {
        
    }
    public void OnClickButton()
    {
        
    }//点击，选中的逻辑
}
