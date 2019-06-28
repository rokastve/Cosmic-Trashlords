using UnityEngine;
using UnityEngine.Experimental.Input;
using UnityEngine.Experimental.Input.Plugins.Users;

public class GameManager : MonoBehaviour
{
    public CharacterActions characterInputAsset;
    public GameObject characterPrefab;

    private CameraController cameraController;

    void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
        SpawnCharacters();
    }

    private void SpawnCharacters()
    {
        if (PlayerManager.Instance == null)
        {
            Debug.Log("PlayerManager instance is null");
            return;
        }

        int count = PlayerManager.Instance.PlayerCount;
        if (count == 0)
        {
            Debug.Log("PlayerManager: PlayerCount is 0");
            return;
        }

        for(int i = 0; i < count; i++)
        {
            SpawnCharacter(i);
        }
    }

    private void SpawnCharacter(int index)
    {
        //Instantiate character
        GameObject character = Instantiate(characterPrefab, transform.position, Quaternion.identity) as GameObject;
        PlayerData playerData = PlayerManager.Instance.GetPlayer(index);
        PlayerManager.Instance.InstantiateCharacter(playerData.inputDeviceId, character);

        //Setup player indicator
        character.GetComponentInChildren<CharacterIndicator>().Setup(playerData);

        //Change character color
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        block.SetColor("_Color", playerData.DisplayColor);
        character.GetComponentInChildren<SkinnedMeshRenderer>().SetPropertyBlock(block, 0);

        //Setup camera target
        cameraController.AddTarget(character.transform);

        //Setup controls for every player
        CharacterActions input = new CharacterActions(Instantiate(characterInputAsset.asset));
        InputDevice device = InputSystem.GetDeviceById(playerData.inputDeviceId);
        InputUser? user = InputUser.FindUserPairedToDevice(device);
        character.GetComponentInChildren<CharacterInput>().SetupInput(input, playerData.inputDeviceId);
    }
}
