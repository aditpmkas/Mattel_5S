using UnityEngine;

public class DripSound : MonoBehaviour
{
    public AudioClip dripSound;
    public float volume = 0.5f;
    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        if (dripSound == null || ps == null) return;

        ParticleCollisionEvent[] events = new ParticleCollisionEvent[16];
        int count = ps.GetCollisionEvents(other, events);

        for (int i = 0; i < count; i++)
        {
            AudioSource.PlayClipAtPoint(dripSound, events[i].intersection, volume);
        }
    }
}
