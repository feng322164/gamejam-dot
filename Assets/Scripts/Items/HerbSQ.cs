using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HerbSQ", menuName = "Game Data/Herbs")]
public class HerbSQ : ScriptableObject
{
    [SerializeField] private List<Herb> herbList = new List<Herb>();
    
    // 通过属性公开列表，以便在其他脚本中访问
    public List<Herb> getHerbList => herbList;
}
