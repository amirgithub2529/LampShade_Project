using _0_Framework.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using System.Collections.Generic;

namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {
            OperationResult operation = new OperationResult();

            if (_colleagueDiscountRepository.Exists(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var ColleagueDiscount = new ColleagueDiscount(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.Create(ColleagueDiscount);
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succedded();
        }



        public OperationResult Edit(EditColleagueDiscount command)
        {
            OperationResult operation = new OperationResult();

            if (_colleagueDiscountRepository.Exists(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var ColleagueDiscount = _colleagueDiscountRepository.Get(command.Id);

            if(ColleagueDiscount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            ColleagueDiscount.Edit(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succedded();
        }



        public EditColleagueDiscount GetDetails(long id)
        {
            return _colleagueDiscountRepository.GetDetails(id);
        }



        public OperationResult Remove(long id)
        {
            OperationResult operation = new OperationResult();

            var ColleagueDiscount = _colleagueDiscountRepository.Get(id);

            if (ColleagueDiscount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            ColleagueDiscount.Remove();
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succedded();
        }



        public OperationResult Restore(long id)
        {
            OperationResult operation = new OperationResult();

            var ColleagueDiscount = _colleagueDiscountRepository.Get(id);

            if (ColleagueDiscount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            ColleagueDiscount.Restore();
            _colleagueDiscountRepository.SaveChanges();
            return operation.Succedded();
        }



        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            return _colleagueDiscountRepository.Search(searchModel);
        }
    }
}
