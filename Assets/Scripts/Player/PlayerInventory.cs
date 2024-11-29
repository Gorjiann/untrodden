using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using System.Collections;
using System;
public class PlayerInventory : MonoBehaviour
{
    [SerializeField] public int[] ItemsCount;// health invis bomb stone torch 
    [SerializeField] private TMP_Text[] AmointCounts;
    [SerializeField] private AudioClip[] UsingAudio;
    [SerializeField] private CinemachineCamera Camera;
    [SerializeField] private float CameraChangingSpeed;
    [SerializeField] private GameObject TorchPrefab;
    [SerializeField] private GameObject BombPrefab;
    [SerializeField] private float CamMax;
    [SerializeField] private float CamMin;
    private PlayerCharacterController player;
    private Coroutine CameraCoroutine;
    private PlayerInputs _input;
    private AudioSource selfaudio;
    public int cameraturns;
    private void Awake()
    {
        selfaudio = GetComponent<AudioSource>();
        _input = new PlayerInputs();
        player = GetComponent<PlayerCharacterController>();
        EventManager.OnTurnPassed.AddListener(ChangeCameraFovControl);
        _input.Player.Items.performed += context => UseItem(context.ReadValue<float>() - 1);
        for (int i = 0; i<AmointCounts.Length; i++)
        {
            AmointCounts[i].text = ItemsCount[i].ToString();
        }
        updatetext();
    }
    public void updatetext()
    {
        for (int i = 0; i < ItemsCount.Length; i++)
        {
            ItemsCount[i] = LevelGlobalPreference.ItemCounts[i];
            AmointCounts[(int)i].text = ItemsCount[(int)i].ToString();
        }
    }
    public void AltarUpdating()
    {
        for (int i = 0; i < ItemsCount.Length; i++)
        {
            ItemsCount[i]++;
            AmointCounts[(int)i].text = ItemsCount[(int)i].ToString();
        }
    }
    public void Steal(int index)
    {
        ItemsCount[index]--;
        AmointCounts[index].text = ItemsCount[index].ToString();
    }
    private IEnumerator ChangeFovCoroutine(float targetFOV)
    {

        float currentFOV = Camera.Lens.OrthographicSize;

        while (!Mathf.Approximately(currentFOV, targetFOV))
        {
            currentFOV = Mathf.MoveTowards(currentFOV, targetFOV, CameraChangingSpeed * Time.deltaTime);
            Camera.Lens.OrthographicSize = currentFOV;
            yield return null;
        }
    }
    private void ChangeCameraFovControl()
    {
        if (cameraturns > 0)
        {
            cameraturns--;
        }
        else
        {
            if (Camera.Lens.FieldOfView > CamMin)
                 StartCoroutine(ChangeFovCoroutine(CamMin));
        }

    }
private void OnEnable() => _input.Enable();
    private void OnDisable() => _input.Disable();

    public void UseItem(float index)
    {

        //Debug.Log(index);
        string bindingPath = _input.Player.Items.bindings[(int)index].path;
        string WeaponIndex = bindingPath.Substring(bindingPath.Length - 1);
        int itemindex = int.Parse(WeaponIndex) - 1;
        if (ItemsCount[(int)index] > 0)
        {
            switch (itemindex)
            {
                case 0:
                    player.CurrentCharacter.GetCharacter(this.gameObject).OnTakeHealth(5);
                    break;
                case 1:
                    player.GetComponent<PlayerStats>().SetInvisability(15);
                    EventManager.ChangeInvisability(true);
                    if (player.GetComponent<PlayerCharacterController>().CurrentCharacter.CharacterName == "Psychic")
                        EventManager.AgressionUpdate(-3);
                        break;
                case 2:
                 GameObject newbomb = Instantiate(BombPrefab, transform.position, Quaternion.identity);
                    newbomb.GetComponent<Bomb>().SetSpawner(player.GetComponent<PlayerStats>().Spawner);
                    break;
                case 3:

                    CameraCoroutine = StartCoroutine(ChangeFovCoroutine(CamMax));
                    if (player.GetComponent<PlayerCharacterController>().CurrentCharacter.CharacterName == "Psychic")
                    {
                        int chanse1 = UnityEngine.Random.Range(0, 100);
                        if (chanse1 > 50)
                        player.GetComponent<PlayerStats>().TakeTreasure(45, false);
                    }
                    cameraturns = 7;
                    
                    break;
                case 4:
                    Instantiate(TorchPrefab, transform.position, Quaternion.identity);
                    break;
            }
            selfaudio.PlayOneShot(UsingAudio[(int)index]);
            ItemsCount[(int)index]--;
            AmointCounts[(int)index].text = ItemsCount[(int)index].ToString();
            EventManager.PassTheTurn();
        }
       
    }
}
