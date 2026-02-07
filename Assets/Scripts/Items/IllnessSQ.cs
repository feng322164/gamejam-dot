using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IllnessSQ", menuName = "Game Data/Illnesses")]
public class IllnessSQ : ScriptableObject
{
    [SerializeField] private List<Illness> illnessList = new List<Illness>();
    
    // 通过属性公开列表，以便在其他脚本中访问
    public List<Illness> getIllnessList => illnessList;

    public IllnessSQ()
    {
        illnessList.Add(new Illness("感冒","症状：咳嗽、流鼻涕、头痛",null,1,2,3));
        illnessList.Add(new Illness("发烧","症状：咳嗽、流鼻涕、头痛",null,2,3,4));
        illnessList.Add(new Illness("崴脚","症状：",null,5,5,5));
        illnessList.Add(new Illness("骨折","症状：",null,6,6,6));
        illnessList.Add(new Illness("中毒","症状：",null,2,2,2));
        illnessList.Add(new Illness("擦伤","症状：",null,3,3,3));
    }
}
