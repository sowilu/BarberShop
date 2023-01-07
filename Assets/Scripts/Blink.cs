using UnityEngine;
using Random = UnityEngine.Random;

public class Blink : MonoBehaviour
{
    public GameObject eyes;

    private void Start()
    {
        BlinkRoutine();
    }

    async void BlinkRoutine()
    {
        while (true)
        {
            await new WaitForSeconds(Random.Range(4f, 6f));
            eyes.SetActive(false);
            await new WaitForSeconds(0.2f);
            eyes.SetActive(true);
        }
    }
}
