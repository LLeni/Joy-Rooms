using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ResultsManager : MonoBehaviour
{
    public static ResultsManager instance;

    private Record resultLevel;
    private Record[] resultsScreens;

    private bool isNewRecordLevel;
    private bool[] isNewRecordScreens;

    void Start()
    {
        instance = this;
        resultsScreens = new Record[5];
        isNewRecordLevel = false;

        isNewRecordScreens = new bool[5];
        for(int i = 0; i < isNewRecordScreens.Length; i++){
            isNewRecordScreens[i] = false;
        }
    }

    public void SaveResultLevel(int idLevel, string timeRun, int amountDeath){
        resultLevel = new Record(Session.currentProfile.GetIdProfile(), idLevel, DateTime.Now.ToString("dd.MM.yyyy"), timeRun, amountDeath);
    }

    public void SaveResultScreen(int orderScreen, int idScreen, string timeRun){
        resultsScreens[orderScreen] = new Record(Session.currentProfile.GetIdProfile(), idScreen, DateTime.Now.ToString("dd.MM.yyyy"), timeRun, -1);
    }

    public void UploadResults(){
        if(resultLevel != null){
            if(RecordsManager.instance.IsRecordExist(resultLevel)){
                if(RecordsManager.instance.IsRecordBetter(resultLevel)){
                    isNewRecordLevel = true;
                    RecordsManager.instance.UpdateRecord(resultLevel);
                }
            } else {
                  RecordsManager.instance.AddNewRecord(resultLevel);
            }
        }

        for(int i = 0; i < resultsScreens.Length; i++){
            if(resultsScreens[i] != null){
                if(RecordsManager.instance.IsRecordExist(resultsScreens[i])){
                    if(RecordsManager.instance.IsRecordBetter(resultsScreens[i])){
                        isNewRecordScreens[i] = true;
                        RecordsManager.instance.UpdateRecord(resultsScreens[i]);
                    }
                } else {
                    isNewRecordScreens[i] = true;
                    RecordsManager.instance.AddNewRecord(resultsScreens[i]);
                }
            }
        }

        for(int i = 0; i < resultsScreens.Length; i++){
             Debug.Log(i + "      " + isNewRecordScreens[i]);
            Debug.Log(i + "      " + resultsScreens[i]);
        }
    }

    public bool IsNewRecordLevel(){
        return isNewRecordLevel;
    }

    public bool IsNewRecordScreen(int orderScreen){
        return isNewRecordScreens[orderScreen];
    }

    public Record GetResultLevel(){
        return resultLevel;
    }

    public Record GetResultScreen(int orderScreen){
        return resultsScreens[orderScreen];
    }
}
