﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Projekt
{
    public partial class History
    {
        public int Idlogin { get; set; }
        public DateTime? Date { get; set; }
        public decimal OldBalance { get; set; }
        public decimal NewBalance { get; set; }

        public virtual LoginData IdloginNavigation { get; set; }
    }
}