using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BaseController<TController, TProccessContract> : ControllerBase
    {
        protected readonly TProccessContract context;

        public BaseController(TProccessContract context)
        {
            this.context = context;
        }
    }
}
