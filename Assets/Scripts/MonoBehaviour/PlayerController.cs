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


    private int groundIndex = 0;

    private void Start()
    {
        DOTween.Init();

        GameManager.Instance.AddState(this);

        level = FindObjectOfType<Level>();

        crossbow = GetComponentInChildren<CrossbowController>();
    }

    public void SetPlay()
    {
        transform.DOMove(points[0].position, 2f).SetDelay(0.5f);
        transform.DOJump(points[1].position, 4f, 1, 2f).SetEase(Ease.Flash).SetDelay(2f);
        transform.DOMove(points[2].position, 2f).SetDelay(4f).OnComplete(PlayEnemy);
    }

    private void PlayEnemy()
    {
        level.SetActiveGround(groundIndex);
        groundIndex++;

        crossbow.IsShoot = true;
    }

    public void Play()
    {
        SetPlay();
    }

    public void GameOver()
    {
        
    }

    public void Finish()
    {
        
    }
}
