using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Service
{
    public class AccountDTO
    {
        public int AccountID { get; set; }
        public long PESEL { get; set; }
        public byte[] Password { get; set; }
    }
}
