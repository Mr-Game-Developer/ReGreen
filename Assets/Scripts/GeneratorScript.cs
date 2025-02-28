using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    public ParticleSystem GeneratorParticle;
    public AudioSource GeneratorSound;

    public void GeneratorFunction()
    {
        Destroy(GeneratorParticle);
        GeneratorSound.Stop();
        GeneratorSound.mute = true;
    }
}
