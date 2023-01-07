using UnityEngine;

public class RotationPivot : MonoBehaviour
{
    public Transform pivot;

    private void Update()
    {
        transform.position = pivot.position;
        transform.rotation = pivot.rotation;
    }
}
