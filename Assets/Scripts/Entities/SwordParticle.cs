using UnityEngine;
using System.Collections;
using Particle = UnityEngine.ParticleSystem.Particle;

public class SwordParticle : MonoBehaviour {

    public int numberOfParticles = 5;

    private ParticleSystem ps;
    Vector3 lastPos;
    float elapsedTime = 0f;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();

        //ps.emission.rate = 0f;

        StartCoroutine(EmitSwordEffectParticles());
    }

    void OnEnable()
    {
        StartCoroutine(EmitSwordEffectParticles());
    }

    void Update()
    {
        StartCoroutine(EmitSwordEffectParticles());

    }

    IEnumerator EmitSwordEffectParticles()
    {
        Vector3 pos = transform.position;
        Vector3 movement = pos - lastPos;
        float movementSpeed = movement.magnitude;
        lastPos = pos;

        float particlesPerSecond = movementSpeed * 25f;

        if (movementSpeed != 0)
            elapsedTime += Time.deltaTime;

        if (ps != null)
        {
            while (elapsedTime > 1f / particlesPerSecond)
            {
                ps.Emit(1); //view overloads
                elapsedTime -= 1f / particlesPerSecond;
            }

            Particle[] particles = new Particle[ps.particleCount];
            ps.GetParticles(particles);
            //Debug.Log(particles.Length);
            for (int i = 0; i < particles.Length; i++)
            {
                Vector3 velocity = particles[i].velocity;
                velocity.y += 10f * Time.deltaTime;
                particles[i].velocity = velocity;
            }
        }

        

        //Vector2 pos = transform.position;
        //if (ps != null)
        //{
        //    for (int j = 0; j < numberOfParticles; j++)
        //    {
        //        ps.Emit(transform.position, Vector3.zero * 1f, 0.1f, 1f, Color.red);

        //        ps.Emit(numberOfParticles);
        //    }

        //    Particle[] particles = new Particle[ps.particleCount];
        //    ps.GetParticles(particles);

        //    for (int i = 0; i < particles.Length; i++)
        //    {
        //        Vector3 velocity = particles[i].velocity;
        //        velocity.z -= 10f * (i + 1) * Time.deltaTime;
        //        particles[i].velocity = velocity;
        //    }

        //    ps.SetParticles(particles, particles.Length);
        //}

        //yield return new WaitForSeconds(1f);
        ////StartCoroutine(EmitSwordEffectParticles());
        yield return null;
    }
    


}
