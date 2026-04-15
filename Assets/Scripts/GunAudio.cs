using UnityEngine;

public class GunAudio : MonoBehaviour
{

    [SerializeField] private AssaultWeaponOriginal assaultWeaponOriginal;
    [SerializeField] private AudioSource assaultAudioSource;
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
        Debug.Log("audio");
        assaultAudioSource.Play();
    }
}
