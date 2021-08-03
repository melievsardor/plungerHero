using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour, IStateUpdate
{

    [SerializeField]
    private List<Transform> points = new List<Transform>();

    private Level level;

    private CrossbowController crossbow;

    private bool isDeath = false;
    public bool IsDeath { get { return isDeath; }  set { isDeath = value; } }

    private bool isFinish = false;

    private int groundIndex = 0;
    private int pointIndex = 0;

    private void Start()
    {
        DOTween.Init();

        GameManager.Instance.AddState(this);

        level = FindObjectOfType<Level>();

        crossbow = GetComponentInChildren<CrossbowController>();
    }

    public void SetPlay()
    {
        NextGround();
    }

    public void NextGround()
    {
        transform.DOMove(points[pointIndex++].position, 2f);

        if(pointIndex == 12) // finish
        {
            transform.DOJump(points[pointIndex].position, 4f, 1, 2f).SetEase(Ease.Flash).SetDelay(2f);
            StartCoroutine(FinishRoutine());
            pointIndex = 0;
            isFinish = true;
            return;
        }
        else if(pointIndex == 10)
        {
            transform.DORotate(new Vector3(0f, 90f, 0f), 4f);
            Camera.main.transform.DORotate(new Vector3(0f, 90f, 0f), 4f);
            //transform.DOMove(points[pointIndex++].position, 2f).SetDelay(2f);
            transform.DOMove(points[pointIndex++].position, 2f).SetDelay(4f).OnComplete(PlayEnemy);
        }
        else
        {
            transform.DOJump(points[pointIndex++].position, 4f, 1, 2f).SetEase(Ease.Flash).SetDelay(2f);
            transform.DOMove(points[pointIndex++].position, 2f).SetDelay(4f).OnComplete(PlayEnemy);
        }

    }


    private void PlayEnemy()
    {
        level.SetActiveGround(groundIndex);
        groundIndex++;

        crossbow.IsShoot = true;
    }

    private IEnumerator FinishRoutine()
    {
        yield return new WaitForSeconds(4f);

        GameManager.Instance.Finish();
    }

    public void SetDeath()
    {
        GameManager.Instance.GameOver();
    }

    public void Play()
    {
        if(!isDeath || !isFinish)
            SetPlay();
    }

    public void GameOver()
    {
        
    }

    public void Finish()
    {
        
    }
}
