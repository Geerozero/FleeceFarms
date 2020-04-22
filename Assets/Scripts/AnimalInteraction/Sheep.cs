
using UnityEngine;

public class Sheep : MonoBehaviour
{
   public void OnMouseDown()
    {
        FindObjectOfType<AudioManager>().Play("Sheep");

        // Start is called before the first frame update

    }
}
