namespace Aggregator.Api.Mappers;

public class EditorialMapper: Profile
{
    public EditorialMapper()
    {
        CreateMap<Editorial, Entities.Editorial>().ReverseMap();
    }
}

