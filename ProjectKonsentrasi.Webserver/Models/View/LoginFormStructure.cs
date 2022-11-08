namespace ProjectKonsentrasi.Webserver.Models.View;

public class LoginFormStructure
{
    public string Email;
    public string Password;

    public LoginFormStructure(IFormCollection form)
    {
        Email = form["email"][0];
        Password = form["password"][0];
    }
}
