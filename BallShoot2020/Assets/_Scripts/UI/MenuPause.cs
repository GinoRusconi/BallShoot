using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuPause : MonoBehaviour
{

    public GameObject _Panel;
    
    
    void Start()
    {
        
    }

    public void EnablePanelPause()
    {
        _Panel.SetActive(true);
    }

    public void Restart()
    {
        //PoolObjects.Current.ClearPool();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
        GameManager.Current.RestarExtraLife();
        _Panel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
