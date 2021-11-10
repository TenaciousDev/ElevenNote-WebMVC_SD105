using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Data
{
    public class Category
    {
        public int CategoryId { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public virtual List<Note> Notes { get; set; }
    }
}
