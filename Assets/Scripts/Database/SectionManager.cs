using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
public class SectionManager : MonoBehaviour
{
    public static SectionManager instance;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        
    }

    public int GetIdSection(string nameSection){
        return int.Parse(DBConnector.GetTable($"SELECT section.id_section FROM section WHERE section.name_section = '{nameSection}'").Rows[0][0].ToString());
    }

    public int[] GetChildSection(int idSection){
        DataTable childrenSectionTable = DBConnector.GetTable($"SELECT section.id_section FROM section WHERE section.id_parent_section = {idSection}");
        int[] children = new int[childrenSectionTable.Rows.Count];
        for(int i = 0; i < childrenSectionTable.Rows.Count; i++){
            children[i] = int.Parse(childrenSectionTable.Rows[i][0].ToString());
        }
        return children;
    }
}
