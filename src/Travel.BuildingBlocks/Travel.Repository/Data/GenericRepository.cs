namespace Travel.Repository.Data;

public class GenericRepository<T>  where T : class, IAggregateRoot
{
    private readonly ILogger<GenericRepository<T>> _logger;
    private readonly DbContext _context;
    private readonly ISpecificationEvaluator _specificationEvaluator;

    /// <summary>
    /// Generic Repository
    /// </summary>
    /// <param name="logger">logger application</param>
    /// <param name="context">context application</param>
    /// <param name="specificationEvaluator">specification evaluator</param>
    public GenericRepository(ILogger<GenericRepository<T>> logger, DbContext context, ISpecificationEvaluator specificationEvaluator)
    {
        _logger = logger;
        _context = context;
        _specificationEvaluator = specificationEvaluator;
    }

    /// <summary>
    /// Generic Repository "Service Inject"
    /// </summary>
    /// <param name="logger">logger application</param>
    /// <param name="context">context application</param>
    public GenericRepository(ILogger<GenericRepository<T>> logger, DbContext context) : this(logger, context, SpecificationEvaluator.Default) { }

    /// <summary>
    /// Create async
    /// </summary>
    /// <param name="entity">Entity to create</param>
    /// <param name="cancellationToken">Cancellationtoken request</param>
    /// <returns>Entity created</returns>
    public async ValueTask<T> CreateAsync(T entity, CancellationToken cancellationToken)
    {
        var entityCreated = await _context.Set<T>().AddAsync(entity, cancellationToken);
        await SaveAsync(cancellationToken);
        _logger.LogInformation($"Created entity {entity} type {typeof(T)}, model {JsonSerializer.Serialize(entity)}");
        return entityCreated.Entity;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask<T> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        var entityUpdate = _context.Set<T>().Update(entity);
        await SaveAsync(cancellationToken);
        _logger.LogInformation($"Updated entity {entity} type {typeof(T)}, model {JsonSerializer.Serialize(entity)}");
        return entityUpdate.Entity;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask<T> DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Delete entity {entity} type {typeof(T)}, model {JsonSerializer.Serialize(entity)}");
        var entityRemoved = _context.Set<T>().Remove(entity);
        await SaveAsync(cancellationToken);
        return entityRemoved.Entity;
    }

    public async ValueTask<int> CountAsync(CancellationToken cancellationToken)
    {
        int counts = await _context.Set<T>().CountAsync(cancellationToken);
        _logger.LogInformation(message: $"Count entities: {counts} type {typeof(T)}");
        return counts;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Get by id {id} type {typeof(T)}");
        return await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask<IEnumerable<T>> ListAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Get all entities type {typeof(T)}");
        return await _context.Set<T>().ToListAsync(cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="spec"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask<IEnumerable<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Specification settled {(spec)}");
        var specification = ApplySpecification(spec);
        _logger.LogInformation($"Get all entities type {typeof(T)}");
        return await specification.ToListAsync(cancellationToken);
    }

    public async ValueTask<T?> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"specification model {typeof(T)}");
        var spec = ApplySpecification(specification);
        return await spec.FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Apply Specification search property model
    /// </summary>
    /// <param name="spec">Property model specification</param>
    /// <param name="evaluateCriteriaOnly">Evaluate only criteria false</param>
    /// <returns>IQueryable Model entity</returns>
    protected virtual IQueryable<T> ApplySpecification(ISpecification<T> spec, bool evaluateCriteriaOnly = default)
    {
        if (spec is null) throw new ArgumentNullException("Specification is required");
        _logger.LogInformation($"Query result type {typeof(T)}");
        return _specificationEvaluator.GetQuery(_context.Set<T>().AsQueryable(), spec, evaluateCriteriaOnly);
    }

    /// <summary>
    /// Apply Specification 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="specification">specification instance model</param>
    /// <returns>IQueryable model entity</returns>
    protected virtual IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification)
    {
        if (specification is null) throw new ArgumentNullException("Specification is required");
        if (specification.Selector is null) throw new SelectorNotFoundException();
        _logger.LogInformation($"Query result type {typeof(T)}");

        return _specificationEvaluator.GetQuery(_context.Set<T>().AsQueryable(), specification);
    }

    /// <summary>
    /// Save async changes
    /// </summary>
    /// <param name="cancellationToken">Cancellationtoken request</param>
    /// <returns></returns>
    private async Task SaveAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}

