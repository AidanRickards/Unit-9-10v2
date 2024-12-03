using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public AudioClip win;

    public AudioSource source;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source.PlayOneShot(win);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadWin()
    {
        SceneManager.LoadScene(1);
    }
}
