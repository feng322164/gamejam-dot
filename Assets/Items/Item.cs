using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]  // 添加可序列化特性
public class Item
{
    [Header("基础信息")]
    [SerializeField] private string itemName;  // 添加序列化字段
    [SerializeField] private string itemDetail;
    
    [Header("症状")]
    [SerializeField] private bool cough;
    [SerializeField] private bool headache;
    [SerializeField] private bool toothache;
    
    // 属性访问器（可选，用于代码访问）
    public string getItemName => itemName;
    public string getItemDetail => itemDetail;
    public bool getCough => cough;
    public bool getHeadache => headache;
    public bool getToothache => toothache;
    
    // 如果需要，可以添加构造函数
    public Item(string name, string detail)
    {
        itemName = name;
        itemDetail = detail;
    }
}