using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace pay.Models.ViewModels
{
    public class UserViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [DataType(DataType.Currency)]
        public float NetSalary { get; set; }
        [DataType(DataType.Currency)]
        public float GroosSalary { get; set; }
        public UserViewModel()
        {

        }
    }
}
