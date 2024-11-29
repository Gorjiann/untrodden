using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputs _input;
    private Room currentRoom;
   [SerializeField] private GameObject MainLights;
    [SerializeField] private AudioSource Source;
   [SerializeField] private AudioClip [] Stepsounds;
   [SerializeField] private GameObject SubLights;

    void Awake()
    {
        _input = new PlayerInputs();
        _input.Player.Move.started += _ => TryMove(_input.Player.Move.ReadValue<Vector2>());

    }
    private void Start()
    {
        MainLights.SetActive(true);
        SubLights.SetActive(true);
       EventManager.PassTheTurn();
        EventManager.SendGameMessage("Now, something will look at you...");
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable() => _input.Disable();

    private void TryMove(Vector2 directions)
    {
        if (currentRoom == null)
            return;
        Vector2 newpos = new Vector2(transform.position.x, transform.position.y) + directions;
        bool canMove = false;
        if (directions == Vector2.up && currentRoom.Directions[0])
        {
            canMove = true;
        }
        else if (directions == Vector2.right && currentRoom.Directions[1])
        {
            canMove = true;
        }
        else if (directions == Vector2.down && currentRoom.Directions[2])
        {
            canMove = true;
        }
        else if (directions == Vector2.left && currentRoom.Directions[3])
        {
            canMove = true;
        }

        if (canMove)
        {
            int rand = Random.Range(0, 2);
            Source.PlayOneShot(Stepsounds[rand]);
            transform.position = newpos;
            EventManager.PassTheTurn();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Room room = collision.GetComponent<Room>();
        if (room != null)
        {
            currentRoom = room;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Room>() == currentRoom)
        {
            currentRoom = null;
        }
    }
}
