using System.ComponentModel.DataAnnotations;

namespace LumiaMVC.Views.Account;

public class LoginVM
{
    [EmailAddress]
    public string EmailAdress { get; set; } = null!;
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
