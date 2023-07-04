namespace Library.Api.Mappers;

public class EditorialMapper: Profile
{
    public EditorialMapper()
    {
        CreateMap<Editorial, Core.Entities.Editorial>().ReverseMap();
    }
}

