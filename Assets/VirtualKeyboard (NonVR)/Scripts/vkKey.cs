using UnityEngine;

public class vkKey : MonoBehaviour
{
    public string k = "xyz";

    // Start is called before the first frame update
    private void Start()
    {
        //KeyClick();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void KeyClick()
    {
        TNVirtualKeyboard.instance.KeyPress(k);
    }
}