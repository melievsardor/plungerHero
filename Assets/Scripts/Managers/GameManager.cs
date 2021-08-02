using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    private List<IStateUpdate> stateUpdates = new List<IStateUpdate>();

    private bool isPlay;
    public bool IsPlay { get { return isPlay; } set { isPlay = value; } }


    protected override void Awake()
    {
        base.Awake();


       // DontDestroyOnLoad(this);


    }


    public void AddState(IStateUpdate state)
    {
        stateUpdates.Add(state);
    }

    public void Play()
    {
        foreach (var state in stateUpdates)
            state.Play();
    }

    public void Finish()
    {
        foreach (var state in stateUpdates)
            state.Finish();
    }

    public void GameOver()
    {
        foreach (var state in stateUpdates)
            state.GameOver();
    }
}
