using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{


    private bool isPlay;
    public bool IsPlay { get { return isPlay; } set { isPlay = value; } }


    protected override void Awake()
    {
        base.Awake();


        DontDestroyOnLoad(this);


    }


}
