using UnityEngine;

public class TestEvents : MonoBehaviour
{
    [SerializeField] private Barricade barricade;

    private void OnEnable()
    {
        barricade.OnRepaired += TestEvent;
    }

    private void OnDisable()
    {
        barricade.OnRepaired -= TestEvent;
    }


    private void TestEvent(object sender, BarricadeEventArgs e)
    {
        Debug.Log("WASSAREPAOIREDDD");
    }



}
