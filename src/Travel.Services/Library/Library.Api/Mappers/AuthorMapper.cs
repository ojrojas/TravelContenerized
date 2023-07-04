namespace Library.Api.Mappers;

public class AuthorMapper: Profile
{
	public AuthorMapper()
	{
		CreateMap<Author, Core.Entities.Author>().ReverseMap();
	}
}

