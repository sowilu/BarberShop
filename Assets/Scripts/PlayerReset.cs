using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    public static PlayerReset inst;
    
    public Transform player;
    public Camera playerHead;

    private AudioSource _audioSource;
    private void Awake()
    {
        if (inst == null) inst = this;
        else Destroy(this);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        _audioSource.Play();
        
        var rotationY = transform.rotation.eulerAngles.y -
                                    playerHead.transform.rotation.eulerAngles.y;
                    player.Rotate(0, rotationY, 0);
        
                    var distance = transform.position - playerHead.transform.position;
                    player.transform.position += distance;
    }
}
