namespace Aggregator.Api.Mappers;

public class BookMapper : Profile
{
	public BookMapper()
	{
		CreateMap<Book, Entities.Book>().ReverseMap();
	}
}

