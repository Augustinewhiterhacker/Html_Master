using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject Whole;
    public GameObject Sliced;

    private Rigidbody fruitRigidbody;
    private Collider fruitCollider;
    private ParticleSystem JuiceParticleEffect;

    public int points = 1;
    public int fruitIndex;
    private Spawner spawner;

    private void Awake()
    {
        fruitRigidbody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        JuiceParticleEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        // Vérifiez si le fruit actuel est celui attendu
        if (spawner != null && spawner.CheckFruitOrder(fruitIndex))
        {
            Debug.Log("Fruit correct tranché");
            FindObjectOfType<GameManager>().IncreaseScore(points);

            Whole.SetActive(false);
            Sliced.SetActive(true);

            fruitCollider.enabled = false;
            JuiceParticleEffect.Play();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

            Rigidbody[] Slices = Sliced.GetComponentsInChildren<Rigidbody>();

            foreach (Rigidbody Slice in Slices)
            {
                Slice.velocity = fruitRigidbody.velocity;
                Slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
            }
        }
        else
        {
            Debug.Log("Fruit incorrect tranché, fin de partie");
            FindObjectOfType<GameManager>().GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Debug.Log("Lame détectée, tentative de trancher...");
            Slice(blade.direction, blade.transform.position, blade.SliceForce);
            
        }
    }

    public void SetSpawner(Spawner spawner)
    {
        this.spawner = spawner;
    }
}
