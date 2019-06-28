using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Input;

public class OperableIndicator : MonoBehaviour
{
    public Sprite keyIcon, gamepadIcon;
    public Text keyCode;
    public Image buttonImage;

    private Animation anim;
    private bool show = false;
    private string useButtonBinding = "Z";

    private void Start()
    {
        anim = GetComponent<Animation>();
        Hide();
    }

    public void Show(int id)
    {
        if (show)
            return;
        show = true;
        anim.Play("OperablePopup");
        if(InputSystem.GetDeviceById(id).name.CompareTo("Keyboard") == 0)
        {
            buttonImage.sprite = keyIcon;
            keyCode.text = useButtonBinding;
        }
        else
        {
            buttonImage.sprite = gamepadIcon;
            keyCode.text = "";
        }
    }

    public void Hide()
    {
        show = false;
        anim.Play("OperableFadeOut");
    }
}
