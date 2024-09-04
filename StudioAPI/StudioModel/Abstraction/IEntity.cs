using System.ComponentModel.DataAnnotations;

namespace StudioModel.Abstraction
{
	public interface IEntity
	{
		[Key]
		public Guid Id { get; set; }
		public bool IsActive { get; set; }
	}
}