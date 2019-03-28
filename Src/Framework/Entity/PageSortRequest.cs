namespace Framework.Entity
{
    public class PageSortRequest : IPageSortRequest
    {
        public string Field { get; set; }
        public string Direction { get; set; }

    }

    public interface IPageFieldRequest
    {
         string Field { get; set; }
    }

    public interface ISortRequest
    {
         string Direction { get; set; }

    }
    public interface IPageSortRequest : IPageFieldRequest,ISortRequest
    {
      

    }
}