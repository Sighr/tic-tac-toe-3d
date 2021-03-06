using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;
    
    private void Start()
    {
        menu ??= transform.GetChild(0).gameObject;
    }
    
    public void OnGameEnded()
    {
        menu.SetActive(true);
    }

    public void OnGameStarted()
    {
        menu.SetActive(false);
    }
}