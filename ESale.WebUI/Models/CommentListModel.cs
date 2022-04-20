using ESale.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESale.WebUI.Models
{
    public class CommentListModel
    {
        public ICollection<Comment> Comments { get; set; }
    }
}
