using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataAccess
{
    public class LoginDTO
    {
        public int AccountID { get; set; }
        public long PESEL { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public byte[] Password { get; set; }
    }
}
