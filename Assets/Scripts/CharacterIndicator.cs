using UnityEngine;
using UnityEngine.UI;

public class CharacterIndicator : MonoBehaviour
{
    public Text numberText;
    public Image tagImage;

    public void Setup(PlayerData player)
    {
        numberText.text = string.Format("P{0}", player.DisplayNumber.ToString());
        tagImage.color = player.DisplayColor;
    }
}
