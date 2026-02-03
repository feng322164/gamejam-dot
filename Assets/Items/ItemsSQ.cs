using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsSQ", menuName = "Game Data/Items")]
public class ItemsSQ : ScriptableObject
{
    [SerializeField] private List<Item> itemSQ = new List<Item>();
    
    // 通过属性公开列表，以便在其他脚本中访问
    public List<Item> getItemsList => itemSQ;
}