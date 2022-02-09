using System.Collections.Generic;
public class Section{
    private int idSession;
    private string nameSession;
    private Dictionary<char, float>  marksConditions;


    public Section(int idSession, string nameSession, Dictionary<char, float> marksConditions){
        this.idSession = idSession;
        this.nameSession = nameSession;
        this.marksConditions = marksConditions;
    }


}