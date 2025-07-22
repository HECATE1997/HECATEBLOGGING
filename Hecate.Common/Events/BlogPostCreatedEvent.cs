using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hecate.Common.Events
{
    public class BlogPostCreatedEvent
    {
        public string PostId { get; set; }
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
