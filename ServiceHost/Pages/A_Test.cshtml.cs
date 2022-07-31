using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A_Test.Domain;
using A_Test.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class A_TestModel : PageModel
    {

        private readonly IInfoRepository _repository;

        public A_TestModel(IInfoRepository repository)
        {
            _repository = repository;
        }

        public List<Info> informations;

        public void OnGet()
        {
            informations = _repository.GetAll();
        }
    }
}
