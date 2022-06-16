using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SkateboardNeverDie.Services.SingleSignOn.ViewModels.Authorization
{
    public class LogoutViewModel
    {
        [BindNever]
        public string RequestId { get; set; }
    }
}
