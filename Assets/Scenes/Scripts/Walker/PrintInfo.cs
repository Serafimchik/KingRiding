using UnityEngine;
using UnityEngine.UI;

public class PrintInfo : MonoBehaviour
{
    public string InfoToPrint;
    public GameObject InfoWindow;
    public Text OutputInfo;
    void Start()
    {
        InfoWindow.SetActive(false);

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        InfoWindow.SetActive(true);
        OutputInfo.text = InfoToPrint;
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        // InfoWindow.SetActive(false);
    }
}
