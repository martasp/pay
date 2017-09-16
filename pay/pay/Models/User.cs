using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pay.Models
{
    public class User
    {
        public int UserId { get; set; }
        // user IDentity(email) from AspNetUser table
        public string OwnerID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public float Salary { get; set; }

        internal class Identities
        {
        }
    }
}
