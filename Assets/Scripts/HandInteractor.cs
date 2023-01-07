using UnityEngine;

public class HandInteractor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reseter"))
        {
            PlayerReset.inst.ResetPosition();
        }
    }
}
