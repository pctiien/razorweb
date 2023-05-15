using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razorEntity.Models;

namespace razorEntity
{
    public class DeleteModel :PageModel
    {
        public Article article{set;get;}
        public static  MyBlogContext myBlogContext{set;get;}
        public DeleteModel(MyBlogContext _myBlogContext)
        {
            myBlogContext = _myBlogContext;
        }
        public static Article FindById(int? id)
        {
            var _article = (from a in myBlogContext.articles
                            where a.Id == id select a).FirstOrDefault();
            return _article;               
        }
        public void OnGet()
        {
            ViewData["Title"]="Delete Item";
            int? id = Int32.Parse(Request.Query["id"].ToString());
            article = FindById(id);
        }
        public IActionResult OnPost()
        {
            int? id = Int32.Parse(Request.Query["id"].ToString());
            var _article = FindById(id);
            if(_article !=null)
            {
                myBlogContext.articles.Remove(_article);
                myBlogContext.SaveChanges();
            }
            return RedirectToPage("/Index");
        }
    }
}