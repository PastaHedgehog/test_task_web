using System.Web;
using System.Web.Mvc;
// Фильтер для обработки всех исключений
namespace test_task_web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
