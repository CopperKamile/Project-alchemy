using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Device;

public class CheckMapBoundaries : MonoBehaviour
{
    //LATER TO DELETE THIS SCRIPT< BECAUSE MAP BOUNDARIES ARE CHECKED BY BOUNDARIES GAME OBJECT 
    private CapsuleCollider2D capsuleCollider2D; //for colliding with walls
    private BoxCollider2D boxXollider; //for the taking damage
    public float rayDistance;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask obsticleLayer;
    [SerializeField] private float padding;

    private void Awake()
    {
        boxXollider = GetComponent<BoxCollider2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }
    private void Start()
    {
        //StartCoroutine(RaycastingWall());
    }

    private void Update()
    {
        //ClampPositionToScreenBounds();
    }

    private IEnumerator RaycastingWall()
    {
        WaitForSeconds waitTime = new WaitForSeconds(0.5f);

        while (true)
        {
            yield return waitTime;
            StopPlayerMovingThroughWalls();
        }
    }

    private bool CheckIfThePlayerHittedWall()
    {
        Vector2 position = transform.position;

        RaycastHit2D hitLeft = Physics2D.Raycast(position, Vector2.left, rayDistance, obsticleLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(position, Vector2.right, rayDistance, obsticleLayer);
        if (hitLeft.collider != null)
        {
            return true;
        }
        else if (hitRight.collider != null)
        {
            return true;
        }
        return false;
    }

    private void StopPlayerMovingThroughWalls()
    {
        if (CheckIfThePlayerHittedWall())
        {
            capsuleCollider2D.enabled = true;
            boxXollider.enabled = false;
        }
        else
        {
            boxXollider.enabled = true;
        }
    }

    private void ClampPositionToScreenBounds()
    {
        float screenLeft = Camera.main.ScreenToWorldPoint(Vector3.zero).x + padding; //bottom left corner

        Vector3 bottonRightCorner = new Vector3(UnityEngine.Screen.width, 0, 0);

        float screenRight = Camera.main.ScreenToWorldPoint(bottonRightCorner).x - padding;

        float currentX = transform.position.x;

        float clampedX = Mathf.Clamp(currentX, screenLeft, screenRight);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

    }
}
