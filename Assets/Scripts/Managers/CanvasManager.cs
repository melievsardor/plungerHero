using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPanel;

    


    public void OnClickPlay()
    {
        menuPanel.SetActive(false);

        GameManager.Instance.IsPlay = true;

    }


}
