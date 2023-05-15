using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using razorEntity.Models;

namespace razorEntity.Pages
{
    public class Create :PageModel
    {
        [BindProperty]
        public Article _article{set;get;} = new Article();
        public MyBlogContext myBlogContext{set;get;}
        public Create(MyBlogContext _myBlogContext)
        {
            myBlogContext =_myBlogContext;
        }
        public void OnGet()
        {
            ViewData["Title"]="Create a new blog";
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            _article.Id = myBlogContext.articles.Count()+1;
            Console.WriteLine($"{_article.Id} {_article.Title} {_article.Created} {_article.Content}");
            myBlogContext.articles.Add(_article);
            await myBlogContext.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}