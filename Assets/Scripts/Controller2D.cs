using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour
{

    float skinWidth = 0.015f;
    int horizontalRayCount = 4;
    int verticalRayCount = 4;
    float horizontalSpacing;
    float verticalSpacing;
    public LayerMask collisionMask;

    BoxCollider2D collider;
    RaycastOrigins raycastOrigins;
    public CollisionsInformation collisionInfo;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }
    
    public void MovePlayer(Vector3 moveDistance)
    {
        UpdateRaycastOrigins();
        collisionInfo.ResetInformation();

        if (moveDistance.y != 0)
        {
            VerticalCollisions(ref moveDistance);
        }

        transform.Translate(moveDistance);
    }

    private void VerticalCollisions(ref Vector3 moveDistance)
    {
        float rayDirectionY = Mathf.Sign(moveDistance.y);
        float rayLength = Mathf.Abs(moveDistance.y) + skinWidth;

        for(int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = rayDirectionY == -1 ? raycastOrigins.bottomLeft : raycastOrigins.bottomLeft;
            rayOrigin += Vector2.right * (verticalSpacing * i + moveDistance.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirectionY * Vector2.up, rayLength, collisionMask);
            Debug.DrawRay(rayOrigin, rayDirectionY * Vector2.up, Color.red);

            if (hit)
            {
                rayLength = hit.distance;
                moveDistance.y = (hit.distance - skinWidth) * rayDirectionY;

                collisionInfo.below = rayDirectionY == -1;
                collisionInfo.above = rayDirectionY == 1;
            }
        }

    }

    void UpdateRaycastOrigins()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(-2f * skinWidth);

        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(-skinWidth);

        Mathf.Clamp(verticalRayCount, 2, int.MaxValue);
        Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);

        verticalSpacing = bounds.size.x / (verticalRayCount - 1);
        horizontalSpacing = bounds.size.y / (horizontalRayCount - 1);
    }

    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    public struct CollisionsInformation
    {
        public bool above, below, left, right;

        public void ResetInformation()
        {
            above = below = left = right = false;
        }

    }

}
