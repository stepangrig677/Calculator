using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace Calculator.Models
{
    public class ResultModels
    {
        public string Operations { get; set; }
        public decimal Result { get; set; }
    }
}