using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razorEntity.Models;

namespace razorEntity.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly MyBlogContext blogContext;

    public IndexModel(ILogger<IndexModel> logger,MyBlogContext _blogContext)
    {
        _logger = logger;
        blogContext = _blogContext;
    }

    public void OnGet()
    {
        var listBlog = (from list in blogContext.articles
                        orderby list.Created ascending
                        select list).ToList();
        ViewData["posts"]=listBlog;
    }
}
