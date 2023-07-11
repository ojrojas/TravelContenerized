namespace Travel.Aggregator.Endpoints;

public static class BookEndpoint
{
	public static RouteGroupBuilder BookEndpointGroup(this RouteGroupBuilder group)
	{
		group.MapGet("GetAllBooks", () => { });
		group.MapPost("CreateBook", () => { });
		group.MapGet("GetByIdBook/{Id}", () => { });
		group.MapPatch("UpdateBook", () => { });
		group.MapDelete("DeleteBook/{Id}", () => { });

		return group;
	}
}

