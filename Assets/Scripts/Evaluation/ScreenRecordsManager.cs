using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System;
using UnityEngine;
using System.Data;
public class ScreenRecordsManager : MonoBehaviour
{
    private const string SELECT_CONDITIONS_SECTION_QUERY = "SELECT evaluationtype.name_evaluationtype, evaluation.timeborder_evaluation FROM evaluation INNER JOIN evaluationtype ON evaluation.id_evaluationtype = evaluationtype.id_evaluationtype WHERE evaluation.id_section = 1;";
    public static ScreenRecordsManager instance;

    private Section[] currentSections;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNewRecord(int idSection, string timeRun, int amountDeath){
            DBConnector.ExecuteQueryWithoutAnswer($"INSERT INTO record(id_profile, id_section, date_record, time_run_record, death_count_record) VALUES({Session.currentProfile.GetIdProfile()}, {idSection}, {DateTime.Today.ToString("d")}, {timeRun}, {amountDeath});");
    }
    public bool IsNewRecord(int idSection){
        if(DBConnector.ExecuteQueryWithAnswer($"SELECT record.id_record FROM record WHERE record.id_section = {idSection} && record.id_profile = {Session.currentProfile.GetIdProfile()};") == null){
            return true;
        } else {
            return false; 
        }
    }

    public char GetMarkRecord(int idSection, string timeRun){
        Debug.Log(timeRun);
        TimeSpan timeSpanRun = TimeSpan.Parse(timeRun);
        DataTable evaluationsForSection = DBConnector.GetTable($"SELECT evaluationtype.name_evaluationtype, evaluation.timeborder_evaluation FROM evaluation INNER JOIN evaluationtype ON evaluation.id_evaluationtype = evaluationtype.id_evaluationtype WHERE evaluation.id_section = {2} ORDER BY evaluation.timeborder_evaluation DESC");

         char currentMark = 'C';
         for(int r = 0; r < evaluationsForSection.Rows.Count; r++){
            TimeSpan timeSpanEvaluation = TimeSpan.Parse(evaluationsForSection.Rows[r][1].ToString());
            Debug.Log(TimeSpan.Compare(timeSpanRun, timeSpanEvaluation));
            if(TimeSpan.Compare(timeSpanRun, timeSpanEvaluation) <= 0){
                currentMark = char.Parse(evaluationsForSection.Rows[r][0].ToString());
            }

         }
        return currentMark;
    }
}
