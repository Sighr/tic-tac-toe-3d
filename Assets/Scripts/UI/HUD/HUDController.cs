using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField]
    private GameObject HUD;
    

    private void Start()
    {
        HUD ??= transform.GetChild(0).gameObject;
    }

    public void OnGameEnded()
    {
        HUD.SetActive(false);
    }
    
    public void OnGameStarted()
    {
        HUD.SetActive(true);
    }
}