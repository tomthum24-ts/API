
namespace BaseCommon.Caching.Interfaces
{
    public interface IDistributedCacheSerializer
    {
        byte[] Serialize<T>(T obj);

        T Deserialize<T>(byte[] bytes);
    }
}
