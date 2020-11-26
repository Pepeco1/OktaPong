using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utils
{
    public static IEnumerable<T> FindInterfacesOfType<T>(bool includeInactive = false)
    {
        return SceneManager.GetActiveScene().GetRootGameObjects()
                .SelectMany(go => go.GetComponentsInChildren<T>(includeInactive));
    }

}
