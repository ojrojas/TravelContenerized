namespace Identity.Core.Specifications;

public class ApplicationUserSpecification: Specification<ApplicationUser>
{
	public ApplicationUserSpecification(string userName)
	{
		Query.Where(x => x.UserName.Equals(userName));
	}
}

