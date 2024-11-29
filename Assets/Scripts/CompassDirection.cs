using UnityEngine;

public class CompassDirection : MonoBehaviour
{

    [SerializeField] private Transform PlayerPosition;
    [SerializeField] private Transform ArrowObj;
    [SerializeField] private Transform TargetPosition;
    [SerializeField] private bool IsDisactive;

    private void Awake()
    {
        EventManager.OnTurnPassed.AddListener(RotateArrow);
        EventManager.OnMagnetState.AddListener(UpdateMagnetic);
        EventManager.OnMagnetTargetSet.AddListener(InstalTarget);
    }
    public void UpdateMagnetic(bool flag)
    {
        IsDisactive = flag;
        ArrowObj.gameObject.SetActive(IsDisactive);
    }
    public void InstalTarget(Transform _target)
    {
        TargetPosition = _target;
    }
    public void RotateArrow()
    {
        if(TargetPosition != null)
        {
            Vector2 direction = TargetPosition.position - PlayerPosition.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
            ArrowObj.transform.localRotation = Quaternion.Euler(0, 0, angle);
        }

    }
}
