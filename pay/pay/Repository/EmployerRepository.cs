using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pay.Models;
using pay.Data;

namespace pay.Repository
{
    public class EmployerRepository : IRepository<User>
    {
        private readonly ApplicationDbContext _context;

        public EmployerRepository(ApplicationDbContext context )
        {
            _context = context;
        }
        public User Get(int id)
        {
            throw new NotImplementedException();
        }


        public void Update(User obj)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetAll(int id)
        {
            return _context.User.Skip(id * 15).Take(15);
        }
    }
}
