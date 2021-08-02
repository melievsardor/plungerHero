using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour, IStateUpdate
{
    [SerializeField]
    private GameObject menuPanel;


    private void Start()
    {
        GameManager.Instance.AddState(this);
    }


    public void Finish()
    {
       
    }

    public void GameOver()
    {
        
    }

    public void OnClickPlay()
    {
        GameManager.Instance.Play();
    }

    public void Play()
    {
        menuPanel.SetActive(false);

        GameManager.Instance.IsPlay = true;
    }
}
