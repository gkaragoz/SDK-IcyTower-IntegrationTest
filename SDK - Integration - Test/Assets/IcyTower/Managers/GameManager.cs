
using UnityEngine;

[RequireComponent(typeof(LoadManager))]
public class GameManager : MonoBehaviour
{
    #region SINGLETON

    public static GameManager current;

    private void Awake()
    {
        if (current == null) { current = this; }

        else if (current != this) { Destroy(gameObject); }

    }

    #endregion

}
