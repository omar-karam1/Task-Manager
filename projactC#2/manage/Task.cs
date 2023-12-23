using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projactC_2.manage
{
    public enum priority { very_important, important, normal };
    public enum TaskType { Study, Entertainment, Another_thing };
    public enum Checked { Not_Complete,Complete  };
    internal class Task
    {

        [Key]
        public int TaskId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Discription { get; set; }
        public int UserID { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public priority Priority { get; set; }
        public TaskType TaskType { get; set; }
        public String Notes { get; set; }
        public Checked check { get; set; }
        [ForeignKey("UserID")]
        public virtual Users User { get; set; }


    }
}
