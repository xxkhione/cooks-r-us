namespace CooksRUs.Components.Cookie
{
    public interface ICookie
    {
        public Task SetValue(int value, int? days=null);
        public Task<int> GetValue(int def = -1);
    }
}
