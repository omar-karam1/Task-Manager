using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
namespace projactC_2.manage
{
    internal class Users
    {

        [Key]
        public int UserId { get; set; }
        [Required]
        public string Fname { get; set; }
         public string Lname { get; set; }
        [Required]
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        public DateTime? BirthDate { get; set; }
        [Required]
        public byte[] ImageData { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }

    }
}
