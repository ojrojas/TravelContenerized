namespace Library.Core.Entities
{
    /// <summary>
    /// Model Book
    /// </summary>
    public class Book : IAggregateRoot
    {
        [Key]
        public int ISBN { get; set; }
        [StringLength(45)]
        public string Title { get; set; }
        public string Sipnosis { get; set; }
        public int Pages { get; set; }
        public Guid EditorialId { get; set; }
        public Editorial Editorial { get; set; }
        public ICollection<AuthorsBooks> AuthorsBooks { get; set; }
    }

}
