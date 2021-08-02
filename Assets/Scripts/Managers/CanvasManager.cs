using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CanvasManager : MonoBehaviour, IStateUpdate
{

    [SerializeField]
    private Text stateText;

    [SerializeField]
    private CanvasGroup menuGroup;

    [SerializeField]
    private CanvasGroup stateGroup;

    [SerializeField]
    private Text buttonText;

    private bool isPlay = true;

    private void Start()
    {
        GameManager.Instance.AddState(this);
    }


    public void Finish()
    {
        isPlay = false;
        stateText.text = "Completed Level";
        buttonText.text = "Play";
        stateGroup.DOFade(1f, 0.5f).SetDelay(1f).SetEase(Ease.InOutBounce);
        menuGroup.DOFade(1, 1f);
        menuGroup.interactable = true;
    }

    public void GameOver()
    {
        isPlay = false;
        stateText.text = "Game Over";
        buttonText.text = "Restart";
        stateGroup.DOFade(1f, 0.5f).SetDelay(1f).SetEase(Ease.InOutBounce);
        menuGroup.DOFade(1, 1f);
        menuGroup.interactable = true;
    }

    public void OnClickPlay()
    {
        GameManager.Instance.Play();
    }

    public void Play()
    {
        if(isPlay)
        {
            menuGroup.DOFade(0, 0.2f);
            menuGroup.interactable = false;

            GameManager.Instance.IsPlay = true;
        }
        else
        {
            DOTween.KillAll();
            UnityEngine.SceneManagement.SceneManager.LoadScene("game");
        }
    }


}
