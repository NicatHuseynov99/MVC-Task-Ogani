namespace Ogani.Utilities.Paginate
{
    public class Pagination<T>
    {
        public Pagination(List<T> datas,int currentPage,int totalPage,int dataCount)
        {
            Datas = datas;
            CurrentPage = currentPage;
            TotalPage = totalPage; 
            DataCount = dataCount;
        }
        public List<T> Datas { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int DataCount { get; set; }
    }
}
