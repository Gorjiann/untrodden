using UnityEngine;
using TMPro;

public class GameMessage : MonoBehaviour
{
    [SerializeField] private TMP_Text message;
    [SerializeField] private Transform Container;

    private void Awake()
    {
        EventManager.OnMessageSend.AddListener(SpawnMessage);
    }
    private void SpawnMessage(string messagetext)
    {
        TMP_Text mess = Instantiate(message, Container.position,Quaternion.identity);
        mess.text = messagetext;
        mess.transform.parent = Container;
        mess.transform.localScale = new Vector3(1, 1, 1);
    }
}
