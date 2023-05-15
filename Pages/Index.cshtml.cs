using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razorEntity.Models;

namespace razorEntity.Pages;

public class IndexModel : PageModel
{
    public PagingModel paging{set;get;}=new PagingModel();
    private readonly ILogger<IndexModel> _logger;
    private readonly MyBlogContext blogContext;
    [BindProperty(SupportsGet =true,Name ="p")]
    public int currentPage{set;get;}
    public const int ITEM_PER_PAGE = 10;
    public List<Article> ITEM_LIST{set;get;}=new List<Article>();

    public IndexModel(ILogger<IndexModel> logger,MyBlogContext _blogContext)
    {
        _logger = logger;
        blogContext = _blogContext;
    }

    public void OnGet()
    {
        this.paging.countPages = (int)Math.Ceiling((double)blogContext.articles.Count()/ITEM_PER_PAGE);
        if(currentPage<1) currentPage=1;
        if(currentPage>this.paging.countPages) currentPage=this.paging.countPages;
        this.paging.currentPage =currentPage;
        this.paging.generateUrl =(int?p)=>Url.Page("Index/",new {p=p});
        ITEM_LIST = (from item in blogContext.articles
                    orderby item.Created descending
                    select item).Skip((currentPage-1)*ITEM_PER_PAGE).Take(ITEM_PER_PAGE).ToList();
        var listBlog = (from list in blogContext.articles
                        orderby list.Created descending
                        select list).ToList();
        ViewData["posts"]=ITEM_LIST;
    }
}
