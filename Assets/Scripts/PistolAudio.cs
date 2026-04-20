using UnityEngine;

public class PistolAudio : MonoBehaviour
{
    [SerializeField] private PistolWeapon Pistolsound;
    [SerializeField] private AudioSource audioSource;
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
        Pistolsound.OnClick += PistolWeapon_OnClick;
    }
    private void PistolWeapon_OnClick(object sender, System.EventArgs e)
    {
        audioSource.Play();
    }
}
