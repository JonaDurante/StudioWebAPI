using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudioModel.Domain;

namespace StudioModel.Dtos.Enrollment
{
	public class EnrollmentDto
	{
		public DateTime EnrollmentDate { get; set; }
		public string UserId { get; set; }
		public Guid CourseId { get; set; }
	}
}
