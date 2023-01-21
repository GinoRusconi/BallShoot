using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    #region Components

    public PlayerInput _playerInput;
    private TimeManager _timeManager;
    private Rigidbody2D myRb;

    #endregion Components

    [SerializeField] private GameObject _deadPlayer;
    public GameObject _Body;
    public bool _playerDead = false;

    void Start()
    {
        _timeManager = GetComponent<TimeManager>();
        myRb = GetComponent<Rigidbody2D>();
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DeadPlayer();
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            DeadPlayer();
        }
    }

    private void DeadPlayer()
    {
        _timeManager.ResetSlowMotion();
        _playerInput.enabled = false;
        _Body.SetActive(false);
        gameObject.layer = LayerMask.NameToLayer("Dead");
        _deadPlayer.SetActive(true);
        _deadPlayer.GetComponent<ParticleSystem>().Play();
        StartCoroutine(PlayerDie());
    }

    public void PlayerRestartExtraLife()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        _deadPlayer.GetComponent<ParticleSystem>().Stop();
        _deadPlayer.SetActive(false);
        gameObject.layer = LayerMask.NameToLayer("Player");
        _playerInput.enabled = true;
        _Body.SetActive(true);
    }

    private IEnumerator PlayerDie()
    {
        myRb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        GameManager.Current._MenuPause.EnablePanelPause();
    }
}
