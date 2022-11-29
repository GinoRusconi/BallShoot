using UnityEngine;

public class GunCore : MonoBehaviour
{
    [SerializeField] private GameObject _StartingPositionBullet;
    private AudioSource _bulletSound;

    private void Start()
    {
        _bulletSound = GetComponent<AudioSource>();
    }
    public void Shoot()
    {
        GameObject myObject = PoolObjects.Current.SpawnFromPool("bullet");
        if (myObject != null)
        {
            myObject.transform.SetPositionAndRotation(_StartingPositionBullet.transform.position, _StartingPositionBullet.transform.rotation);
            myObject.SetActive(true);
            _bulletSound.Play();
        }
    }
}