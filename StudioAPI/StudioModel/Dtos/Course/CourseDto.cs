using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudioModel.Domain;

namespace StudioModel.Dtos.Course
{
	public class CourseDto
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public string Level { get; set; }

	}
}
