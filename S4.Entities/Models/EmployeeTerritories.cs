using System;
using System.Collections.Generic;

namespace S4.Entities.Models
{
    public partial class EmployeeTerritories
    {
        public int EmployeeId { get; set; }
        public string TerritoryId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Territories Territory { get; set; }
    }
}
