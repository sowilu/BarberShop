using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class Item : MonoBehaviour
{
    [Header("Effects")]
    public AudioClip impactSound;
    public AudioClip useSound;
    public ParticleSystem particles;
    
    [Header("Collision information")] 
    public bool playParticlesOnCollision;
    public List<string> colliders;
    
    private AudioSource _audioSource;
    private Vector3 startPos;
    private Quaternion startRotation;
    
    protected XRBaseController _controller;
    protected bool _inUse;
    protected bool _colliding;
    

    protected virtual void Start()
    {
        startPos = transform.position;
        startRotation = transform.rotation;
        
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = true;
        _audioSource.clip = useSound;
        
        var interactable = GetComponent<XRBaseInteractable>();
        interactable.selectEntered.AddListener(OnSelectEntered);
        interactable.selectExited.AddListener(OnSelectExit);
        interactable.activated.AddListener(OnActivated);
        interactable.deactivated.AddListener(OnDeactivated);
    }

    private void OnDeactivated(BaseInteractionEventArgs e)
    {
        _inUse = false; 
        _audioSource.Stop();
        particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        EndUsing();
    }
    
    private void OnActivated(BaseInteractionEventArgs e)
    {
        _inUse = true; 
        _audioSource.Play();
        
        if(!playParticlesOnCollision) particles.Play();
        StartUsing();
    }
    
    private void OnSelectExit(BaseInteractionEventArgs e)
    {
        _controller = null; 
        
        if(_inUse) OnDeactivated(e);

        _inUse = false;
        
        PutDown();
    }

    public virtual void StartUsing()
    {
        
    }

    public virtual void EndUsing()
    {
        
    }

    private void OnSelectEntered(BaseInteractionEventArgs e)
    {
        var interactor = e.interactorObject as XRBaseControllerInteractor;
        _controller = interactor.xrController;
        Take();
    }
    public virtual void Take()
    {
       
    }

    
    public virtual void PutDown()
    {
        
    }
    
    protected virtual void OnCollisionEnter(Collision collision)
    {
        var speed = collision.relativeVelocity.magnitude;

        var power = Mathf.InverseLerp(0.1f, 5, speed);

        if (power == 0) return;
        
        if(_audioSource != null) _audioSource.PlayOneShot(impactSound, power);
        if(_controller != null) _controller.SendHapticImpulse(power, 0.1f);

        if (!_inUse && collision.transform.CompareTag("Ground"))
        {
            
            Invoke(nameof(Respawn), 2f);
        }

        if (colliders.Contains(collision.transform.tag))
        {
            _colliding = true;
            particles.Play();
        }
    }

    protected virtual void OnCollisionExit(Collision other)
    {
        if (colliders.Contains(other.transform.tag))
        {
            _colliding = false;
            particles.Stop();
        }
    }

    void Respawn()
    {
        transform.rotation = startRotation;
        transform.position = startPos;
    }
}
