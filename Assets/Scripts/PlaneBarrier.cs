using UnityEngine;

public class PlaneBarrier : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Plane")
        {
            PlaneManager.Instance.RemovePlane(collision.GetComponent<Plane>());
        }
    }

}
