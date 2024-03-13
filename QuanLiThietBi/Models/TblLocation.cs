using System;
using System.Collections.Generic;

namespace QuanLiThietBi.Models
{
    public partial class TblLocation
    {
        public TblLocation()
        {
            TblEmployees = new HashSet<TblEmployee>();
            TblProducts = new HashSet<TblProduct>();
        }

        public int LocationId { get; set; }
        public string NameLocation { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Type { get; set; } = null!;

        public virtual ICollection<TblEmployee> TblEmployees { get; set; }
        public virtual ICollection<TblProduct> TblProducts { get; set; }
    }
}
