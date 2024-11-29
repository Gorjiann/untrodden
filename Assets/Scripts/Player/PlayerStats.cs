using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    private PlayerInputs _input; 
    [SerializeField] private AudioSource Source;
    [SerializeField] private AudioClip Atackplay;
    [SerializeField] private AudioClip Damageplay;
    [SerializeField] private AudioClip Treasureplay;
    public int currentlevel;
    [SerializeField] public int CurrentHealth;
    [SerializeField] public int MaxHealth;
    [SerializeField] public int CurrentTreasure;
    [SerializeField] private Image HealthBar;
    private bool IsInvisible;
    private int InvisCurrentTurns;
    [SerializeField] private SpriteRenderer selfsprite;
    [SerializeField] private int Turns;
    [SerializeField] private TMP_Text TurnsText;
    [SerializeField] private TMP_Text TreasuresText;
    public DungeonGenerator Spawner;
    private PlayerCharacterController characterController;
    private bool exitspawned;

    private int CurrentTurns= 60;
    private int MaxRevailTurns = 60;
    void Awake()
    {
        characterController = GetComponent<PlayerCharacterController>();
        if (!LevelGlobalPreference.IsEndlees)
        {
            currentlevel = LevelGlobalPreference.LevelIndex;
        }
        else
        {
            currentlevel = -1;
        }
        _input = new PlayerInputs();
        HealthBar.fillAmount = CurrentHealth / MaxHealth;
        _input.Player.Menu.started += _ => LeaveToMenu();
        _input.Player.Attack.started += _ => Atack();
        _input.Player.Interact.started += _ => TakeInteraction();
        EventManager.OnTurnPassed.AddListener(ControlTurns);
        EventManager.OnTurnPassed.AddListener(ControlInvisability);

    }
    private void Start()
    {
        TreasuresText.text = "Treasures: " + CurrentTreasure;
    }
    public void InstaliszeStats(int hp)
    {
        MaxHealth = hp;
        CurrentHealth = hp;
        HealthBar.fillAmount = CurrentHealth / MaxHealth;
    }
    private void OnEnable()=>  _input.Enable();

    private void OnDisable() => _input.Disable();
    public void LeaveToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void SetInvisability(int turns)
    {
        IsInvisible = true;
        InvisCurrentTurns = turns;
    }
    private void ControlInvisability()
    {
        if (InvisCurrentTurns > 0)
            InvisCurrentTurns--;
        else
        {
            IsInvisible = false;
            EventManager.ChangeInvisability(false);
        }

    }
    public void TakeHealth(int health)
    {
        CurrentHealth += health;
        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;
        HealthBar.fillAmount = (float)CurrentHealth / (float)MaxHealth;
    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        HealthBar.fillAmount = (float) CurrentHealth / (float)MaxHealth;
        Source.PlayOneShot(Damageplay);
        if (CurrentHealth <= 0)
            Die();
    }
    public void EndLevel()
    {
        SceneManager.LoadScene(0);
        if (!LevelGlobalPreference.IsEndlees)
        {
            SaveSystem.SaveLevelData(this);
        }
    }
    public IInteractable GetInteract()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        IInteractable closestTreasure = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D collider in hitColliders)
        {
            IInteractable enemy = collider.GetComponent<IInteractable>();
            if (enemy != null)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTreasure = enemy;
                }
            }
        }

        return closestTreasure;
    }
    public void TakeInteraction()
    {
        if (GetInteract()!= null)
        {
            GetInteract().OnInteract(this);
        }

    }
    public void TakeTreasure(int worth, bool ismeaning)
    {

        if(GetComponent<PlayerCharacterController>().CurrentCharacter.CharacterName == "Marauder")
        {
            CurrentTreasure += (int)(worth * 1.2f);
        }
            else{
            CurrentTreasure += worth;
        }
        GetComponent<PlayerCharacterController>().CurrentCharacter.GetCharacter(this.gameObject).OnTakeTreasure();
        if (ismeaning && CurrentTreasure < 1800)
            Spawner.SpawnTreasure();
        else if(CurrentTreasure >= 1800 && !exitspawned)
        {
            Spawner.SpawnExit();
            exitspawned = true;
            EventManager.SendGameMessage("Go to ladder, if you can still move");
        }
        Source.PlayOneShot(Treasureplay);
        TreasuresText.text = "Treasures: " + CurrentTreasure;
    }
    public void ActivateAltar(int pray)
    {
        if (CurrentTreasure - pray > 0)
        {
            GetComponent<PlayerInventory>().AltarUpdating();
            CurrentHealth = MaxHealth;
            CurrentTreasure-= pray;
            TreasuresText.text = "Treasures: " + CurrentTreasure;

            EventManager.SendGameMessage("Scarifase has taken");
        }
    }
    public void ControlTurns()
    {
        Turns++;
        TurnsText.text = "Passed turns: "+ Turns.ToString();
        TreasuresText.text = "Treasures: " + CurrentTreasure;
        if (IsInvisible)
            selfsprite.color = new Color(1, 1, 1, 0.5f);
        else selfsprite.color = new Color(1, 1, 1, 1);
        OnlyForUndead();
    }
    public void OnlyForUndead()
    {
        if(CurrentTurns < MaxRevailTurns)
        {
            CurrentTurns++;
        }
    }
    private void Atack()
    {
        Source.PlayOneShot(Atackplay);
        characterController.CurrentCharacter.GetCharacter(this.gameObject).OnAtack();
    }
    private void Die() {

        if (GetComponent<PlayerCharacterController>().CurrentCharacter.CharacterName == "Undead" && CurrentTurns == MaxRevailTurns)
        {
            CurrentTurns = 0;
            TakeHealth(300);
            EventManager.SendGameMessage("Your death has been delayed...");
        }
        else SceneManager.LoadScene(0);
    }
}
