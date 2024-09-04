using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StudioModel.Abstraction
{
	public abstract class Entity : IEntity
	{
		[Key]
		public Guid Id { get; set; }
		public bool IsActive { get; set; }

	}
}
