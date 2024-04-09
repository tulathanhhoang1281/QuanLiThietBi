using System;
using System.Collections.Generic;

namespace QuanLiThietBi.Models
{
    public partial class TblMaintenance
    {
        public int MaintenanceId { get; set; }
        public int ProductId { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string Description { get; set; } = null!;
        public int Status { get; set; }
        public string AssignTo { get; set; } = null!;
        public DateTime CompletionDate { get; set; }

        public virtual TblProduct Product { get; set; } = null!;
    }
}
