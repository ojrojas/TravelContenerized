namespace Travel.Aggregator.Endpoints;

public static class EditorialEndpoint
{
    public static RouteGroupBuilder EditorialEndpointGroup(this RouteGroupBuilder group)
    {
        group.MapGet("GetAllEditorials", () => { });
        group.MapGet("GetByIdEditorial/{Id}", () => { });
        group.MapPost("CreateEditorial", () => { });
        group.MapPatch("UpdateEditorial", () => { });
        group.MapDelete("DeleteEditorial/{Id}", () => { });

        return group;
    }
}

