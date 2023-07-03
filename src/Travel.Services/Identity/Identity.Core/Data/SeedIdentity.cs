namespace Identity.Core.Data;

public class SeedIdentity
{
	public static ApplicationUser CreateApplicationUser()
	{
		return new ApplicationUser
		{
			Id= "182276cd-9ce5-452f-a303-bc77728a6e33",
			Name = "Pepe",
			LastName = "Perele",
			Identification = "123456789",
			UserName ="pepe@example.com",
			PasswordHash ="Abc123456#"
		};
	}
}

