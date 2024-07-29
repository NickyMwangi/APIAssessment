using Library.Common;
using Library.Dtos;
using Library.Enums;
using System.Web.Mvc;

namespace Business.IProcesses.shared
{
    public interface IOptionsProcess
    {
        List<SelectListItem> DropDownList(ListTypes listType, string[] filterValues);
    }
}
