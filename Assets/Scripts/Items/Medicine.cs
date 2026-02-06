using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Medicine
{
    [Header("基础信息")]
    [SerializeField] string medicineName;
    [SerializeField] string medicineDetail;
    [SerializeField] Sprite medicineSprite;
    [Header("精神")]
    [SerializeField] int mindWound;
    [Header("外伤")]
    [SerializeField] int outsideWound;
    [Header("内伤")]
    [SerializeField] int internalWound;
    public string getMedicineName => medicineName;
    public string getMedicineDetail => medicineDetail;
    public int getMindWound => mindWound;
    public int getOutsideWound => outsideWound;
    public int getInternalWound => internalWound;
    public Sprite getMedicineSprite => medicineSprite;
    public void ChangeInternalWound(int wound)
    { internalWound = wound; }
    public void ChangeOutsideWound(int wound) { outsideWound = wound; }
    public void ChangeName(string name) {  medicineName = name; }
    public void ChangeDetail() { medicineDetail = "外伤" + outsideWound + "\n" + "内伤" + internalWound + "\n"; }
    public void ChangeSprite(Sprite sprite) { medicineSprite = sprite; }
}
