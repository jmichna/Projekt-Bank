using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Projekt
{
    public partial class LoginData
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public virtual Balance Balance { get; set; }
        public virtual Data Data { get; set; }
        public virtual History History { get; set; }
    }
}
