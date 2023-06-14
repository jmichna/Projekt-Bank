using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Projekt
{
    public partial class Balance
    {
        public int Idlogin { get; set; }
        public decimal Balance1 { get; set; }

        public virtual LoginData IdloginNavigation { get; set; }
    }
}
