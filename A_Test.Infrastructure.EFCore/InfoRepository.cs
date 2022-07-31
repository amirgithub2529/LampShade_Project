using A_Test.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Test.Infrastructure.EFCore
{
    public class InfoRepository : IInfoRepository
    {
        private readonly TestContext _context;

        public InfoRepository(TestContext context)
        {
            _context = context;
        }

        public Info Get(long id)
        {
            return _context.Informations.FirstOrDefault(x => x.Id == id);
        }

        public List<Info> GetAll()
        {
            return _context.Informations.ToList();
        }

        public void Create(string name , string family , int age)
        {
            var info = new Info(name, family, age);
            _context.Add(info);
            _context.SaveChanges();
        }
    }
}
