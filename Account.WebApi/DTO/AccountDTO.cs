using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Account.WebApi.DTO
{
    public class AccountDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Balance { get; set; }
   
    }
}
