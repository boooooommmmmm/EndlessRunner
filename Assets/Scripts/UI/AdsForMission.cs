using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AdsForMission : MonoBehaviour
{
    public MissionUI missionUI;
    public Button addButton;

    void Update()
    {
        addButton.gameObject.SetActive(false);
    }

    public void ShowAds()
    {
    }

    void AddNewMission()
    {
        PlayerData.instance.AddMission();
        PlayerData.instance.Save();
        missionUI.Open();
    }
}
