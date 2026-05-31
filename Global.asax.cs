using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using JapnesseCafe.Models;

namespace JapnesseCafe
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer(new CafeDbInitializer());
            using (var db = new CafeDbContext())
            {
                db.Database.Initialize(false);
            }
        }
    }
}
