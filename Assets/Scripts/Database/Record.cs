public class Record{
    private int idRecord;
    private  int idProfile;
    private int idSection;
    private string dateRecord;
    private string timeRunRecord;
    private int amountDeath;

    public Record(int idRecord, int idProfile, int idSection, string dateRecord, string timeRunRecord, int amountDeath){
        this.idRecord = idRecord;
        this.idProfile = idProfile;
        this.idSection = idSection;
        this.dateRecord = dateRecord;
        this.timeRunRecord = timeRunRecord;
        this.amountDeath = amountDeath;
    }

    public int GetIdRecord(){
        return idRecord;
    }

    public int GetIdProfile(){
        return idProfile;
    }

    public int GetIdSection(){
        return idSection;
    }

    public string GetDateRecord(){
        return dateRecord;
    }

    public string GetTimeRunRecord(){
        return timeRunRecord;
    }

    public int GetAmountDeath(){
        return amountDeath;
    }
}