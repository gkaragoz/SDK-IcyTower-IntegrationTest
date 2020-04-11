
using UnityEngine;

[RequireComponent(typeof(ProgressBar))]
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

    /********************************************************/
    // LOAD SYSTEM

    private LoadManager _loadManager;

    /********************************************************/

    private void Start()
    {
        _loadManager = new LoadManager(GetComponent<ProgressBar>());
    }

}
