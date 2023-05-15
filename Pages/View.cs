using Microsoft.AspNetCore.Mvc.RazorPages;
using razorEntity.Models;

namespace razorEntity
{
    public class ViewModel :PageModel
    {
        public Article article {set;get;}
        public MyBlogContext context;
        public ViewModel(MyBlogContext _myBlogContext)
        {
            context = _myBlogContext;
        }
        public void OnGet()
        {
            int? id = Int32.Parse(Request.Query["id"].ToString());
            article = context.articles.FirstOrDefault(a=>a.Id==id);
            ViewData["Title"]="View page";
        }
    }
}