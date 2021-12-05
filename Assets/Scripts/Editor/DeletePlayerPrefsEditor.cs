using UnityEditor;
using UnityEngine;

public class DeletePlayerPrefsEditor
{
    [MenuItem("Edit/DeletePlayerPrefs &D")]
    public static void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Player Prefs Deleted!");
    }
}