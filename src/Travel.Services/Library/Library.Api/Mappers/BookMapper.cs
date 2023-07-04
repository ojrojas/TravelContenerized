namespace Library.Api.Mappers;

public class BookMapper : Profile
{
	public BookMapper()
	{
		CreateMap<Book, Core.Entities.Book>().ReverseMap();
	}
}

