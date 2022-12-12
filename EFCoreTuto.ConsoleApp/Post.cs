namespace EFCoreTuto.ConsoleApp
{
    internal class Post : BaseEntity
    {
        public string Title { get; set; }
        
        public string Content { get; set; }

        public int BlogId { get; set; }

        public Blog Blog { get; set; }

    }
}
