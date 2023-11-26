using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetailsAssigner : MonoBehaviour
{
    //refrence to all result texts 
    public TextMeshProUGUI TimerL1;
    public TextMeshProUGUI JumpsL1;
    public TextMeshProUGUI DashL1;
    public TextMeshProUGUI DeathL1;
    public TextMeshProUGUI TimerL2;
    public TextMeshProUGUI JumpsL2;
    public TextMeshProUGUI DashL2;
    public TextMeshProUGUI DeathL2;

    private void OnEnable() {
        Assinment();
    }
    //Assign All Values from Data Manager to respective Refrences
    void Assinment()
    {
        TimerL1.text= "Time : "+GameManager.instance.dataManager.Level1Time.ToString("0.00");
        JumpsL1.text= "Total Jumps: "+GameManager.instance.dataManager.Level1TotalJumps.ToString();
        DashL1.text ="Total Dash : "+ GameManager.instance.dataManager.Level1TotalDash.ToString();
        DeathL1.text ="Deaths : "+ GameManager.instance.dataManager.Level1NoOfDeaths.ToString();


        TimerL2.text= "Time : " +GameManager.instance.dataManager.Level2Time.ToString("0.00");
        JumpsL2.text= "Total Jumps: "+GameManager.instance.dataManager.Level2TotalJumps.ToString();
        DashL2.text ="Total Dash : "+ GameManager.instance.dataManager.Level2TotalDash.ToString();
        DeathL2.text="Deaths : "+ GameManager.instance.dataManager.Level2NoOfDeaths.ToString();
    }
}
