
using UnityEngine;

public class TriggerEnter : MonoBehaviour
{
    public delegate void RobotTriggerDelegate(Collider other);
    public RobotTriggerDelegate triggerDelegate;
    private void OnTriggerEnter(Collider other)
    {
        if (triggerDelegate != null)
        {
            triggerDelegate(other);
        }
    }
}
