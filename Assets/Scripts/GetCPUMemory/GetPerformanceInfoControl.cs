using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPerformanceInfoControl : MonoBehaviour
{
    public GameObject PerformanceInfoDashboard;
    public Button ShowDashboard_Btn;

    private string FilePath = "PerformanceInfo.json";
    private bool isWriteinJson = false;

    private void Start()
    {
        ShowDashboard_Btn.onClick.AddListener(ChangeDashboardActive);
    }

    private void Update()
    {
        if(CPUusage.Instance.isGetInfoOver && !isWriteinJson)
        {
            SaveJsonToFile();
            isWriteinJson = true;
        }
    }

    public void ChangeDashboardActive()
    {
        if(PerformanceInfoDashboard.activeSelf == true)
        {
            PerformanceInfoDashboard.SetActive(false);
        }
        else if(PerformanceInfoDashboard.activeSelf == false)
        {
            PerformanceInfoDashboard.SetActive(true);
        }
    }

    //Windows：保存PerformanceInfoList到游戏根目录下的json文件内
    private void SaveJsonToFile()
    {
        //获取游戏根目录路径
        string directory = Path.GetDirectoryName(Application.dataPath);
        //组合路径以保存json文件到根目录下
        FilePath = Path.Combine(directory, FilePath);
        //判断目录下是否有该文件
        if (!File.Exists(FilePath))
        {
            File.Create(FilePath);
        }
        StreamWriter sw = new StreamWriter(FilePath);
        for(int i = 0; i < CPUusage.Instance.listCount; i++)
        {
            sw.WriteLine(CPUusage.Instance.performanceInfoList[i]);
        }
        sw.Close();
    }
}
