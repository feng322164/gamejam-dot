using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Herb
{
    [Header("基础信息")]
    [SerializeField] string herbName;
    [SerializeField] string herbDetail;
    [SerializeField] Sprite herbSprite;
    [Header("精神")]
    [SerializeField] int mindWound;
    [Header("外伤")]
    [SerializeField] int outsideWound;
    [Header("内伤")]
    [SerializeField] int internalWound;
    public string getHerbName => herbName;
    public string getHerbDetail => herbDetail;
    public int getMindWound => mindWound;
    public int getOutsideWound => outsideWound;
    public int getInternalWound => internalWound;
    public Sprite getHerbSprite => herbSprite;
}
