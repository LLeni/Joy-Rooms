using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResultsManager : MonoBehaviour
{
    
    private const string SELECT_CONDITIONS_SECTION_QUERY = "SELECT evaluationtype.name_evaluationtype, evaluation.timeborder_evaluation FROM evaluation INNER JOIN evaluationtype ON evaluation.id_evaluationtype = evaluationtype.id_evaluationtype WHERE evaluation.id_section = 1;";
    public static ScreenResultsManager instance;

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
}
