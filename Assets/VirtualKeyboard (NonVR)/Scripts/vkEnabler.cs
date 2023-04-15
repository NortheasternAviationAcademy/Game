using UnityEngine;
using UnityEngine.UI;

public class vkEnabler : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        //ShowVirtualKeyboard();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void ShowVirtualKeyboard()
    {
        TNVirtualKeyboard.instance.ShowVirtualKeyboard();
        TNVirtualKeyboard.instance.targetText = gameObject.GetComponent<InputField>();
    }
}