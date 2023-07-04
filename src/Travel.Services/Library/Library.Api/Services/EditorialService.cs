using System;
namespace Library.Api.Services
{
	public class EditorialService: EditorialTravelService.EditorialTravelServiceBase
	{
        private readonly EditorialRepository _EditorialRepository;
        private readonly ILogger<EditorialService> _logger;
        private readonly IMapper _mapper;

        public EditorialService(EditorialRepository EditorialRepository, ILogger<EditorialService> logger, IMapper mapper)
        {
            _EditorialRepository = EditorialRepository ?? throw new ArgumentNullException(nameof(EditorialRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Create Editorial
        /// </summary>
        /// <param name="request">Request Editorial model</param>
        /// <param name="context">Context Server</param>
        /// <returns>Editorial created</returns>
        public override async Task<CreateEditorialResponse> CreateEditorial(CreateEditorialRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Create Editorial request...");
            CreateEditorialResponse response = new();
            response.EditorialCreated = _mapper.Map<Editorial>(
                await _EditorialRepository.CreateAsync(_mapper.Map<Core.Entities.Editorial>(request.Editorial), context.CancellationToken));

            return response;
        }

        /// <summary>
        /// Update Editorial 
        /// </summary>
        /// <param name="request">Update model</param>
        /// <param name="context">Context call server</param>
        /// <returns></returns>
        public override async Task<UpdateEditorialResponse> UpdateEditorial(UpdateEditorialRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Update Editorial request...");
            UpdateEditorialResponse response = new();
            response.EditorialUpdated = _mapper.Map<Editorial>(
                await _EditorialRepository.UpdateAsync(_mapper.Map<Core.Entities.Editorial>(request.Editorial), context.CancellationToken));

            return response;
        }

        /// <summary>
        /// Delete Editorial
        /// </summary>
        /// <param name="request">Id Editorial to delete</param>
        /// <param name="context">Server context</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public override async Task<DeleteEditorialResponse> DeleteEditorial(DeleteEditorialRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Delete Editorial by id request...");
            DeleteEditorialResponse response = new();
            var entityToDelete = await _EditorialRepository.GetByIdAsync(Guid.Parse(request.Id), context.CancellationToken);
            if (entityToDelete == null) throw new InvalidOperationException("Not found Editorial to delete");
            response.EditorialDeleted = _mapper.Map<Editorial>(
                await _EditorialRepository.DeleteAsync(entityToDelete, context.CancellationToken));

            return response;
        }

        /// <summary>
        /// Get all Editorials 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<ListEditorialsResponse> ListEditorials(Empty request, ServerCallContext context)
        {
            _logger.LogInformation("Get all Editorials request...");
            var list = await _EditorialRepository.ListAsync(context.CancellationToken);
            ListEditorialsResponse response = new();
            response.Editorials.AddRange(list.Select(x => _mapper.Map<Editorial>(x)));

            return response;
        }

        /// <summary>
        /// Get Editorial by id
        /// </summary>
        /// <param name="request">Get Editorial by id</param>
        /// <param name="context">Server call context</param>
        /// <returns>Editorial found</returns>
        public override async Task<GetEditorialByIdResponse> GetEditorialById(GetEditorialByIdRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Get Editorial by id request...");
            GetEditorialByIdResponse response = new();
            response.Editorial = _mapper.Map<Editorial>(await _EditorialRepository.GetByIdAsync(Guid.Parse(request.Id), context.CancellationToken));

            return response;
        }
    }
}

