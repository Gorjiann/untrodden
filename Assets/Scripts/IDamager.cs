using UnityEngine;

public interface IDamager
{
    MonoBehaviour MonoRef { get; }
    int Damage {  get; set; }
}
