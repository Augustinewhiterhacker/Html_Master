 using UnityEngine;

// public class Bomb : MonoBehaviour
// {
   
//     private void OnTriggerEnter(Collider other)
//     {
//         if( other.CompareTag("Player")) {
//            FindObjectOfType<GameManager>().Explode();
//         }
//     }
// }


public class Bomb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.GameOver();
            }
            else
            {
                Debug.LogError("GameManager non trouvé dans la scène.");
            }
        }
    }
}
