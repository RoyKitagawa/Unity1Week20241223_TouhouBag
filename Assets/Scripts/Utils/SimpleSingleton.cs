public abstract class SimpleSingleton<T> where T : SimpleSingleton<T>, new()
{
    private static T instance;
    public static T Instance{
        get {
            if(instance != null) return instance;
            instance = new T();
            return instance;
        }
    }
}
