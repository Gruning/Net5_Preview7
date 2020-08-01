using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5_Model.Models
{
    public class Publisher
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Publisher_Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Location { get; set; }
        public List<Book> Books { get; set; }

    }
}
