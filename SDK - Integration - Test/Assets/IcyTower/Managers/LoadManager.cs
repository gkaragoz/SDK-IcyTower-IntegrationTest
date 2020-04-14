using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AuthManager),typeof(GameModeManager), typeof(SceneManager))]
public class LoadManager : MonoBehaviour
{
    private AuthManager authManager;

    private GameModeManager gameModeManager;

    private SceneManager sceneManager;

    protected enum LoadState { ONLINEMODE, OFFLINEMODE }

    protected LoadState loadState;

    protected bool stopCycle;

    private void Awake()
    {
        authManager = GetComponent<AuthManager>();

        gameModeManager = GetComponent<GameModeManager>();

        sceneManager = GetComponent<SceneManager>();
    }

    private void Start()
    { 
        // Initialize Main Process
        StartManager();
    }
    
    /// <summary>
    ///  Initialize CORE LOADING CONTROL PROCESS
    /// </summary>
    public void StartManager() 
    {
        /// <summary>
        /// This is where all loading core progress initialized.
        /// 1 - ControlAuthProcess - > It's control all authentication control state and make ready the game for success launch.
        /// 2 - ControlGameModeProcess - > Organizes game contents depends on which state has been selected by AuthManager like Offline Mode or Online Mode.
        /// 3 - ControlSceneProcess -> Final loading state that controls all ingame content loading completed succefully.
        /// </summary>

        /// Start with first control manager that's AuthManager
        StartCoroutine(ControlAuthProcess());
    }

    // First Stage Control -> AuthMANAGER
    IEnumerator ControlAuthProcess()
    {
        // Wait Until "All Authentication Process Complete".
        while (!AuthProcess() && !stopCycle)
        {
            yield return new WaitForSeconds(0.25f);
        }

        // Stop Process
        StopCoroutine(ControlAuthProcess());

        // Start Second Stage Control
        StartCoroutine(ControlGameModeProcess());
    }

    // Second Stage Control -> GameModeMANAGER
    IEnumerator ControlGameModeProcess()
    {
        // Wait Until "All GameMode Process Complete".
        while (!GameModeProcess() && !stopCycle)
        {
            yield return new WaitForSeconds(0.25f);
        }

        // Stop Process
        StopCoroutine(ControlGameModeProcess());

        // Start Third Stage Control
        StartCoroutine(ControlSceneProcess());
    }

    // Third Stage Control -> SceneMANAGER
    IEnumerator ControlSceneProcess()
    {
        // Wait Until "All Scene Process Complete".
        while (!SceneProcess() && !stopCycle)
        {
            yield return new WaitForSeconds(0.25f);
        }

        // Stop Process
        StopCoroutine(ControlSceneProcess());

        // All stage process completed. 
        // Determine the process completed as success or fail
        FinalizeAllProcess();
    }

    private void FinalizeAllProcess( )
    {
        // All Process Completed, Start Game
        if (!stopCycle)
        {
            // DONE
            // START AWESOME GAME
        }

        else
        {
            Debug.LogError("Loading Failed. Exiting from game...");

            // EXIT GAME
        }
    }


    private bool AuthProcess()
    {                            // Initialize AuthManager
        if (!authManager._startService) 
        { 
            authManager.StartProcess();
        }
            
        return authManager.ProcessComplete();
    }

    private bool GameModeProcess()
    {
        

        return true;
        
        //return gameModeManager.ProcessComplete();
    }

    private bool SceneProcess()
    {
        
        return true;

        //return sceneManager.ProcessComplete();
    }

}
