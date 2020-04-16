using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityExample1.Models
{
    public class Task
    {
		/*
		create table Tasks (
			Id int NOT NULL primary key identity(100,1),
			UserID int NOT NULL,
			Description nvarchar(300) NOT NULL,
			DueDate datetime NOT NULL,
			Complete bit NOT NULL,
			foreign key (UserID) references IdentityUser(Id)
		)
		*/
		public int ID { get; set; }
		public int UserID { get; set; }

		[Required]
		[MinLength(1, ErrorMessage = "Must have task description")]
		[MaxLength(300, ErrorMessage = "Max 300 characters")]
		public string Description { get; set; }

		[Required]
		[DataType(DataType.Date)]
		//[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime DueDate { get; set; }
		public bool Complete { get; set; }

	}
}
