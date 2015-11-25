using UnityEngine;
using System.Collections;
using Particle = UnityEngine.ParticleSystem.Particle;

public class SwordParticle : MonoBehaviour {

    public int numberOfParticles = 15;

    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ps.emissionRate = 0f;

        StartCoroutine(EmitSwordEffectParticles());
    }

    void OnEnable()
    {
        StartCoroutine(EmitSwordEffectParticles());
    }

    void Update()
    {
        
    }

    IEnumerator EmitSwordEffectParticles()
    {
        Vector2 pos = transform.position;
        if (ps != null)
        {
            for (int j = 0; j < numberOfParticles; j++)
            {
                //ps.Emit(transform.position, Vector3.zero * 1f, 0.1f, 1f, Color.red);

                ps.Emit(numberOfParticles);
            }

            Particle[] particles = new Particle[ps.particleCount];
            ps.GetParticles(particles);

            for (int i = 0; i < particles.Length; i++)
            {
                Vector3 velocity = particles[i].velocity;
                velocity.z -= 10f * (i + 1) * Time.deltaTime;
                particles[i].velocity = velocity;
            }

            ps.SetParticles(particles, particles.Length);
        }

        //yield return new WaitForSeconds(1f);
        //StartCoroutine(EmitSwordEffectParticles());
        yield return null;
    }
    


}
