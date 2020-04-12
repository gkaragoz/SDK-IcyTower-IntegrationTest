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
        // Wait Until "All Authentication Process Complete".
        while (!AuthProcess()) ;

        // Wait Until "All GameMode Process Complete".
        while (!GameModeProcess()) ;

        // Wait Until "All Scene Process Complete".
        while (!SceneProcess()) ;

        // All Process Completed, Start Game

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
