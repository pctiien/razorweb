using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using razorEntity.Models;

namespace razorEntity
{
    public class EditModel: PageModel
    {
        [BindProperty]
        public Article _article {set;get;}
        public static MyBlogContext myBlogContext;
        public EditModel(MyBlogContext _myBlogContext)
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
            int? id = Int32.Parse(Request.Query["id"].ToString());
            if(id==null) return;
             _article = FindById(id);
             ViewData["Title"]="Edit page";
        }
        public async Task<IActionResult> OnPost()
        {

            if(!ModelState.IsValid)
            {
                return Page();
            }
            if(_article!=null)
            {
               myBlogContext.Attach(_article).State=EntityState.Modified;
                try
                {
                    await myBlogContext.SaveChangesAsync();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return RedirectToPage("./Index");
        }
    }
}