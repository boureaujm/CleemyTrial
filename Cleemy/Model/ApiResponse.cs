namespace Cleemy.Model
{
    public class ApiResponse<T>
    {
        public bool Succeed { get; set; }
        public T Result { get; set; }
    }
}