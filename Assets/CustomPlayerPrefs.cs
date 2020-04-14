using UnityEngine;

/// <summary>
/// PlayerPrefs wrapper to provide saving boolean values.
/// </summary>
public static class CustomPlayerPrefs
{
    private const int StorageFalse = 0;
    private const int StorageTrue = 1;

    #region bool
    public static void SetBool(string key, bool value, bool saveImmediately)
    {
        int intValue = value ? StorageTrue : StorageFalse;
        PlayerPrefs.SetInt(key, intValue);
        if (saveImmediately) { Save(); }
    }

    public static bool GetBool(string key)
    {
        int intValue = PlayerPrefs.GetInt(key);
        return intValue == StorageTrue;
    }
    #endregion

    #region int
    public static void SetInt(string key, int value, bool saveImmediately)
    {
        PlayerPrefs.SetInt(key, value);
        if (saveImmediately) { Save(); }
    }

    public static int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }

    public static int GetInt(string key, int defaultValue)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }
    #endregion

    #region float
    public static void SetFloat(string key, float value, bool saveImmediately)
    {
        PlayerPrefs.SetFloat(key, value);
        if (saveImmediately) { Save(); }
    }

    public static float GetFloat(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }

    public static float GetFloat(string key, float defaultValue)
    {
        return PlayerPrefs.GetFloat(key, defaultValue);
    }
    #endregion

    #region string
    public static void SetString(string key, string value, bool saveImmediately)
    {
        PlayerPrefs.SetString(key, value);

        if (saveImmediately) { Save(); }
    }

    public static string GetString(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    public static string GetString(string key, string defaultValue)
    {
        return PlayerPrefs.GetString(key, defaultValue);
    }
    #endregion

    public static bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    public static void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }

    #region private
    private static void Save()
    {
        PlayerPrefs.Save();
    }
    #endregion
}