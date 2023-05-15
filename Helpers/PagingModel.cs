namespace razorEntity
{
    public class PagingModel
    {
        public int currentPage {get;set;}
        public int countPages {set;get;}
        public Func<int?,string> generateUrl{set;get;}
    }
}