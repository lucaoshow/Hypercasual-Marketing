namespace Root.General.API
{
    public interface IGetResultContainer<T> 
    {
        public void AddRange(T element);
        public int Count();
    }
}
