using System;
using System.Collections.Generic;

namespace QuanLiThietBi.Models
{
    public partial class TblComponent
    {
        public int ComponentId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string Manufacturer { get; set; } = null!;
        public string SerialNumber { get; set; } = null!;
        public string Dvt { get; set; } = null!;

        public virtual TblProduct Product { get; set; } = null!;
    }
}
