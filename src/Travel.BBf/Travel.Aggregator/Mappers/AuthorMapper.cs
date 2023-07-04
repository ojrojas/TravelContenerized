namespace Aggregator.Api.Mappers;

public class AuthorMapper: Profile
{
	public AuthorMapper()
	{
		CreateMap<Author, Entities.Author>().ReverseMap();
	}
}

