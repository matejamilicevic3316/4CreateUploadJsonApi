using Appplication;
using AutoMapper;
using _4CreateWebApiJsonUpload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Implementation.Commands
{
    public abstract class BaseCommand<TReq, TResp> : IBaseCommand<TReq, TResp>
    {
        protected MedicineContext _medicineContext;
        protected IMapper _mapper;
        public BaseCommand(MedicineContext medicineContext, IMapper mapper)
        {
            _medicineContext = medicineContext;
            _mapper = mapper;
        }

        public abstract TResp Execute(TReq req);
    }
}
