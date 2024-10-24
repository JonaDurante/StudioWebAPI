﻿using System.ComponentModel.DataAnnotations;

namespace StudioModel.Domain
{
	public class Courses
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		[StringLength(20)]
		public string Name { get; set; }

		[Required]
		[StringLength(100)]
		public string Description { get; set; }

		[Required]
		public string Level { get; set; }

		public ICollection<Enrollments> Enrollments { get; set; }

	}
}
