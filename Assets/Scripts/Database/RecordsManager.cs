using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System;
using UnityEngine;
using System.Data;
public class RecordsManager : MonoBehaviour
{

    public static RecordsManager instance;
    void Awake()
    {
        instance = this;
    }

    public Dictionary<char, string> GetSectionEvaluations(int idSection){
        DataTable evaluationsTable = DBConnector.GetTable($"SELECT evaluationtype.name_evaluationtype, evaluation.timeborder_evaluation FROM evaluation INNER JOIN evaluationtype ON evaluation.id_evaluationtype = evaluationtype.id_evaluationtype WHERE evaluation.id_section = {idSection} ORDER BY evaluation.timeborder_evaluation DESC");
        Dictionary<char, string> evaluationsDictionary = new Dictionary<char, string>();
        for(int i = 0; i < evaluationsTable.Rows.Count; i++){
            evaluationsDictionary.Add(char.Parse(evaluationsTable.Rows[i][0].ToString()), evaluationsTable.Rows[i][1].ToString());
        }
        return evaluationsDictionary;
    }

    public string GetSpecificEvaluation(int idSection, char mark){
        Debug.Log(idSection + " " + mark);
        DataTable evaluationTable = DBConnector.GetTable($"SELECT evaluation.timeborder_evaluation FROM evaluation INNER JOIN evaluationtype ON evaluation.id_evaluationtype = evaluationtype.id_evaluationtype WHERE evaluation.id_section = {idSection} AND evaluationtype.name_evaluationtype = '{mark.ToString()}';");
        
        if(evaluationTable.Rows.Count == 0){
            return null;
        } else {
            return evaluationTable.Rows[0][0].ToString();
        }
    }
    
    public char GetMarkRecord(int idSection, string timeRun, int amountDeath){
        TimeSpan timeSpanRun = TimeSpan.Parse(timeRun);
        DataTable evaluationsForSection = DBConnector.GetTable($"SELECT evaluationtype.name_evaluationtype, evaluation.timeborder_evaluation FROM evaluation INNER JOIN evaluationtype ON evaluation.id_evaluationtype = evaluationtype.id_evaluationtype WHERE evaluation.id_section = {idSection} ORDER BY evaluation.timeborder_evaluation DESC");

         char currentMark = 'C';
         for(int r = 0; r < evaluationsForSection.Rows.Count; r++){
            TimeSpan timeSpanEvaluation = TimeSpan.Parse(evaluationsForSection.Rows[r][1].ToString());
            if(TimeSpan.Compare(timeSpanRun, timeSpanEvaluation) <= 0){
                currentMark = char.Parse(evaluationsForSection.Rows[r][0].ToString());
                if(currentMark == 'J' && amountDeath > 0){
                    currentMark = 'A';
                }
            }
         }
        return currentMark;
    }

    public Record GetRecord(int idProfile, int idSection){
        DataTable recordTable = DBConnector.GetTable($"SELECT record.id_record, record.id_profile, record.id_section, record.date_record, record.time_run_record, record.death_count_record FROM record WHERE id_profile = {idProfile} AND id_section = {idSection}");
        if(recordTable.Rows.Count == 0){
            return null;
        } else {
            return new Record(int.Parse(recordTable.Rows[0][0].ToString()), int.Parse(recordTable.Rows[0][1].ToString()), int.Parse(recordTable.Rows[0][2].ToString()), recordTable.Rows[0][3].ToString(), recordTable.Rows[0][4].ToString(), int.Parse(recordTable.Rows[0][5].ToString()));
        }
    }

    public string GetNameLastFinishedLevel(int idProfile){
        DataTable nameLastFinishedLevelTable = DBConnector.GetTable($"SELECT section.name_section, MAX(record.id_section) FROM record INNER JOIN section ON record.id_section = section.id_section INNER JOIN sectiontype ON section.id_sectiontype = sectiontype.id_sectiontype WHERE sectiontype.name_sectiontype = 'Уровень' AND record.id_profile = {idProfile};");

        if(nameLastFinishedLevelTable.Rows[0][0].ToString().Equals("")){
            return null;
        } else {
            return nameLastFinishedLevelTable.Rows[0][0].ToString();
        }
    }
}
