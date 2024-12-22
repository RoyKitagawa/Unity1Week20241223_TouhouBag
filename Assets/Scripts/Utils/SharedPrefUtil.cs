using UnityEngine;

public static class SharedPrefUtil
{
    public static void Save(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        Debug.Log("Saved: " + key + " / " + value);
        PlayerPrefs.Save();
    }

    public static void Save(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        Debug.Log("Saved: " + key + " / " + value);
        PlayerPrefs.Save();
    }

    public static void Save(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        Debug.Log("Saved: " + key + " / " + value);
        PlayerPrefs.Save();
    }

    public static float LoadFloat(string key, float defaultValue = 0.0f)
    {
        return PlayerPrefs.GetFloat(key, defaultValue);
    }

    public static int LoadInt(string key, int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    public static string LoadString(string key, string defaultValue = "")
    {
        return PlayerPrefs.GetString(key, defaultValue);
    }

    public static bool IsKeySaved(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    public static void Delete(string key)
    {
        PlayerPrefs.DeleteKey(key);
        Debug.Log("Delete saved data: " + key);
        PlayerPrefs.Save();
    }

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Delete all saved data.");
        PlayerPrefs.Save();
    }
}