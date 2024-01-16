using Group_Project_1.Models;
namespace Group_Project_1.ViewModels;

public class UserWithRolesViewModel
{
	public User User { get; set; } = default!;
	public IList<string> Roles { get; set; } = new List<string>();
}

