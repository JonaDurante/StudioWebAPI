using System.ComponentModel.DataAnnotations;

namespace StudioModel.Domain
{
    public class Entity : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
