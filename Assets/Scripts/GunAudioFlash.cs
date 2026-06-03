using UnityEngine;

public class GunAudioFlash : MonoBehaviour
{   [Header("particle")]
    [SerializeField] private ParticleSystem pistolFlash;
    [SerializeField] private ParticleSystem muzzleFlash;  //gets the particlesystem in the script
    [Header("gunscripts")]
    [SerializeField] private AssaultWeaponOriginal assaultWeaponOriginal;
    [SerializeField] private PistolWeapon Pistolsound;
    [Header("audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource assaultAudioSource;
    [SerializeField] private AudioSource knifeSwing;
    [SerializeField] private AudioSource knifeHit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        assaultWeaponOriginal.OnFire += AssaultWeaponOriginal_OnFire;
        Pistolsound.OnClick += PistolWeapon_OnClick;
        knifeSwing.OnSwing += knifeSwing_OnSwing;
    }

    private void AssaultWeaponOriginal_OnFire(object sender, System.EventArgs e)
    {
        
        Debug.Log("audio");
        assaultAudioSource.Play();
        muzzleFlash.Play();
    }

    private void PistolWeapon_OnClick(object sender, System.EventArgs e)
    {
        pistolFlash.Play();
        audioSource.Play();
    }
    private void knifeSwing_OnSwing(object sender, System.EventArgs e)
    {
        knifeSwing.Play();
    } 
   

}
