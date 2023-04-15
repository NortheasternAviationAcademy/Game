using UnityEngine;
using UnityEngine.UI;

public class TNVirtualKeyboard : MonoBehaviour
{
    public static TNVirtualKeyboard instance;

    public string words = "";

    public GameObject vkCanvas;

    public InputField targetText;


    // Start is called before the first frame update
    private void Start()
    {
        instance = this;
        HideVirtualKeyboard();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void KeyPress(string k)
    {
        words += k;
        targetText.text = words;
    }

    public void Del()
    {
        words = words.Remove(words.Length - 1, 1);
        targetText.text = words;
    }

    public void ShowVirtualKeyboard()
    {
        vkCanvas.SetActive(true);
    }

    public void HideVirtualKeyboard()
    {
        vkCanvas.SetActive(false);
    }
}