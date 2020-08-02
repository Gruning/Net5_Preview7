using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5_Model.Models
{
    public class FluentBookAuthor
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public FluentBook FluentBook { get; set; }
        public FluentAuthor FluentAuthor { get; set; }

    }
}
