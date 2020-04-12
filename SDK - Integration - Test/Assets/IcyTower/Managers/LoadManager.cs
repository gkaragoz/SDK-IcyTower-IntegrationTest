using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadManager
{
    private AuthManager authManager;

    private GameModeManager gameModeManager;

    private SceneManager sceneManager;

    protected enum LoadState { ONLINEMODE, OFFLINEMODE }

    protected LoadState loadState;

    protected bool stopCycle;

    protected ProgressBar progressBar;


    public LoadManager( ) { }

    public LoadManager(ProgressBar progressBar)
    {
        this.progressBar = progressBar;

        // Initialize Main Process
        StartManager();
    }

    public void StartManager() 
    {
        Debug.LogWarning("GG");

        // Wait Until "All Authentication Process Complete".
        while (!AuthProcess() && !stopCycle) ;

        // Wait Until "All GameMode Process Complete".
        while (!GameModeProcess() && !stopCycle) ;

        // Wait Until "All Scene Process Complete".
        while (!SceneProcess() && !stopCycle) ;

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
    {
        // Initialize AuthManager
        authManager = new AuthManager();

        return authManager.ProcessComplete();
    }

    private bool GameModeProcess()
    {
        // Initialize GameModeManager
        gameModeManager = new GameModeManager();

        return true;
        
        //return gameModeManager.ProcessComplete();
    }

    private bool SceneProcess()
    {
        // Initialize SceneManager
        sceneManager = new SceneManager();

        return true;

        //return sceneManager.ProcessComplete();
    }

}
