using Business.IProcesses.shared;
using Data.Interfaces;
using Library.Enums;
using System.Web.Mvc;

namespace Business.Processes.shared
{
    public class OptionsProcess : IOptionsProcess
    {
        private readonly IRepoService repo;
        public OptionsProcess(IRepoService _repo) { 
            this.repo = _repo;
        }

        public List<SelectListItem> DropDownList(ListTypes listType, string[] filterValues)
        {
            List<SelectListItem> droplist = new();
            switch (listType)
            {

                case ListTypes.OptionsList:
                    droplist = [];
                    break; ;
            }
            return droplist;
        }

    }
}
