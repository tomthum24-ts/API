namespace API.Domain
{
    public abstract class Entity<TKey>
    {
        public TKey Id { get; set; }

        public TKey? CreatedById { get; set; }

        public TKey? UpdatedById { get; set; }

        public TKey? DeletedById { get; set; }
    }
}
