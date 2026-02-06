using System.Collections.Generic;
using UnityEngine;

public class PatientDataManager : MonoBehaviour
{
    // 字符串列表，包含所有数据字段名称
    public List<string> dataFields = new List<string>()
    {
        "Date",
        "PatientsToday",
        "PatientsNow",
        "MedicineNumber",
        "PatientHurt1",
        "PatientHurt2",
        "PatientHurt3",
        "PatientName"
    };

    // 对应的整数列表（除了PatientName）
    public List<int> dateData = new List<int>();
    public List<int> patientsTodayData = new List<int>();
    public List<int> patientsNowData = new List<int>();
    public List<int> medicineNumberData = new List<int>();
    public List<int> patientHurt1Data = new List<int>();
    public List<int> patientHurt2Data = new List<int>();
    public List<int> patientHurt3Data = new List<int>();

    // PatientName使用字符串列表
    public List<string> patientNameData = new List<string>();

    // 通过字段名称获取对应的数据列表（返回object类型）
    public object GetDataListByField(string fieldName)
    {
        switch (fieldName)
        {
            case "Date": return dateData;
            case "PatientsToday": return patientsTodayData;
            case "PatientsNow": return patientsNowData;
            case "MedicineNumber": return medicineNumberData;
            case "PatientHurt1": return patientHurt1Data;
            case "PatientHurt2": return patientHurt2Data;
            case "PatientHurt3": return patientHurt3Data;
            case "PatientName": return patientNameData;
            default: return null;
        }
    }

    // 添加整数数据到指定字段
    public void AddIntData(string fieldName, int data)
    {
        if (fieldName == "PatientName")
        {
            Debug.LogWarning("PatientName字段需要使用AddStringData方法");
            return;
        }

        List<int> targetList = GetDataListByField(fieldName) as List<int>;
        if (targetList != null)
        {
            targetList.Add(data);
        }
        else
        {
            Debug.LogWarning($"未找到字段: {fieldName}");
        }
    }

    // 添加字符串数据到PatientName字段
    public void AddStringData(string fieldName, string data)
    {
        if (fieldName != "PatientName")
        {
            Debug.LogWarning($"字段 {fieldName} 需要使用AddIntData方法");
            return;
        }

        patientNameData.Add(data);
    }

    // 通用添加方法，自动判断类型
    public void AddData(string fieldName, object data)
    {
        if (fieldName == "PatientName")
        {
            if (data is string stringData)
                patientNameData.Add(stringData);
            else
                Debug.LogWarning("PatientName字段需要字符串类型数据");
        }
        else
        {
            if (data is int intData)
            {
                List<int> targetList = GetDataListByField(fieldName) as List<int>;
                if (targetList != null)
                    targetList.Add(intData);
                else
                    Debug.LogWarning($"未找到字段: {fieldName}");
            }
            else
            {
                Debug.LogWarning($"{fieldName}字段需要整数类型数据");
            }
        }
    }

    // 获取指定字段的数据数量
    public int GetDataCount(string fieldName)
    {
        var targetList = GetDataListByField(fieldName);
        if (targetList is List<int> intList)
            return intList.Count;
        else if (targetList is List<string> stringList)
            return stringList.Count;
        else
            return 0;
    }

    // 获取PatientName列表中的特定姓名
    public string GetPatientName(int index)
    {
        if (index >= 0 && index < patientNameData.Count)
            return patientNameData[index];
        else
            return string.Empty;
    }

    // 获取整数类型字段的数据
    public int GetIntData(string fieldName, int index)
    {
        if (fieldName == "PatientName")
        {
            Debug.LogWarning("PatientName字段是字符串类型，请使用GetPatientName方法");
            return -1;
        }

        List<int> targetList = GetDataListByField(fieldName) as List<int>;
        if (targetList != null && index >= 0 && index < targetList.Count)
            return targetList[index];
        else
            return -1;
    }

    // 清空所有数据
    public void ClearAllData()
    {
        dateData.Clear();
        patientsTodayData.Clear();
        patientsNowData.Clear();
        medicineNumberData.Clear();
        patientHurt1Data.Clear();
        patientHurt2Data.Clear();
        patientHurt3Data.Clear();
        patientNameData.Clear();
    }
}