using System.ComponentModel.DataAnnotations;

namespace StudioModel.Domain
{
    public interface IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }
}