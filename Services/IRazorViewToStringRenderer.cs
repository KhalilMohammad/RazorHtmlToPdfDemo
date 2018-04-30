using System.Threading.Tasks;

namespace RazorHtmlToPdfDemo.Services
{
    public interface IRazorViewToStringRenderer
    {
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
    }
}