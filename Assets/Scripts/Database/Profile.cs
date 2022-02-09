using System.ComponentModel.Design;
public class Profile{
    private int idProfile;
    private string loginProfile;


    public Profile(int idProfile, string loginProfile){
        this.idProfile = idProfile;
        this.loginProfile = loginProfile;
    }

    public int GetIdProfile(){
        return idProfile;
    }

    public string GetLoginProfile(){
        return loginProfile;
    }


}