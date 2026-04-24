using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleFlash;  //gets the particlesystem in the script
    [SerializeField] private AssaultWeaponOriginal assaultWeaponOriginal;
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
    }

    private void AssaultWeaponOriginal_OnFire(object sender, System.EventArgs e)
    {
        muzzleFlash.Play();
    }

}
