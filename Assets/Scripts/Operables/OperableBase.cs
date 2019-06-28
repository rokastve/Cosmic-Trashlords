using UnityEngine;
using UnityEngine.Experimental.Input;
using UnityEngine.Experimental.Input.Plugins.Users;

public abstract class OperableBase : MonoBehaviour
{
    public OperableIndicator indicator;
    protected PlayerData Operator { get; set; }
    protected InputActionAssetReference input = null;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterInput charInput = other.GetComponent<CharacterInput>();
            charInput.UsePressed += TryEnter;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(Operator == null)
        {
            if(other.CompareTag("Player"))
            {
                int id = other.GetComponent<CharacterInput>().Id;
                indicator.Show(id);
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterInput charInput = other.GetComponent<CharacterInput>();
            charInput.UsePressed -= TryEnter;
            indicator.Hide();
        }
    }

    private void TryEnter(int id)
    {
        bool success = Enter(id);
        if (success)
        {
            Debug.Log("Character " + id + " have entered this operable");
            indicator.Hide();
        }
    }

    public virtual bool Enter(int id)
    {
        //Check if operable is taken
        if (Operator != null)
        {
            Debug.Log("This is already in use");
            return false;
        }

        //Check if charcater does not carry scrap
        GameObject character = PlayerManager.Instance.GetPlayerCharacter(id);
        if(character.GetComponentInChildren<Container<Scrap>>().IsEmpty == false)
        {
            Debug.Log("You first need to drop the scrap in order to enter");
            return false;
        }

        Operator = PlayerManager.Instance.GetPlayerById(id);

        //Change character control scheme
        CharacterInput characterInput = character.GetComponent<CharacterInput>();
        InputUser? user = InputUser.FindUserPairedToDevice(InputSystem.GetDeviceById(id));
        if (user != null)
        {
            characterInput.GetComponent<CharacterMovement>().enabled = false;
            characterInput.UseActionMap(input);
        }
        else
        {
            Debug.LogError("InputUser is null");
            return false;
        }
        return true;
    }

    public virtual void Exit(int id)
    {
        if (input == null)
        {
            Debug.LogError("input is null");
            return;
        }

        //No longer operate this thing
        Operator = null;

        //Switch back to character's default control scheme
        CharacterInput characterInput = PlayerManager.Instance.GetPlayerCharacter(id).GetComponent<CharacterInput>();
        characterInput.UseDefaultActionMap();
        characterInput.GetComponent<CharacterMovement>().enabled = true;

        indicator.Show(id);
    }

    public abstract void SetupInputEvents();

    public abstract void ClearInputEvents();
}
