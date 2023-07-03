namespace Library.Core.Entities
{
    /// <summary>
    /// Model Author
    /// </summary>
    public class Author : BaseEntity, IAggregateRoot
    {
        [StringLength(45)]
        public string Name { get; set; }
        [StringLength(45)]
        public string LastName { get; set; }
        public ICollection<AuthorsBooks> AuthorsBooks { get; set; }
    }
}
