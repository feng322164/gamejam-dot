using UnityEngine;

public class ExampleClass : MonoBehaviour
{
    private int patients = 2; // 当天总病人数
    private int patientnow = 3;//当天剩余病人数
    // 公开方法，用于更新一天的总病人数量并初始化这一天的剩余病人数目
    public void PatientsToday()
    {
        patients++;
        Debug.Log($"这一天总的病人数: {patients}");
        patientnow = patients;
    }
    //公开方法，用于更新这一天剩余的病人数目
    public void PatientOver()
    {
        patientnow--;
        Debug.Log($"这一天剩下的病人数: {patients}");
    }
    //公开方法
}
