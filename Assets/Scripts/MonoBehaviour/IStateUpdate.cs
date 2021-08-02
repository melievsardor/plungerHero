using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateUpdate
{
    void Play();
    void GameOver();
    void Finish();
}
