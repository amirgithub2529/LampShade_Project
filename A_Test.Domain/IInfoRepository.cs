using System.Collections.Generic;

namespace A_Test.Domain
{
    public interface IInfoRepository
    {
        Info Get(long id);
        List<Info> GetAll();
        void Create(string name, string family, int age);
    }
}
