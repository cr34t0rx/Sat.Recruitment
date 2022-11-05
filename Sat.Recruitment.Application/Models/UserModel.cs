using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Application.Models;

public class UserModel
{
    [Required]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public string Phone { get; set; }

    [Required]
    [RegularExpression("Normal|SuperUser|Premium", ErrorMessage = "Only Normal, SuperUser and Premium are allowed as UserType.")]
    public string UserType { get; set; }

    [Required]
    public decimal Money { get; set; }
}
