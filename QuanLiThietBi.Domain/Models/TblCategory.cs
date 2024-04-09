using System;
using System.Collections.Generic;

namespace QuanLiThietBi.Models
{
    public partial class TblCategory
    {
        public TblCategory()
        {
            TblProducts = new HashSet<TblProduct>();
        }

        public int CategoryId { get; set; }
        public string NameCategory { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<TblProduct> TblProducts { get; set; }
    }
}
