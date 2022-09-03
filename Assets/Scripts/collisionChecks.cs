using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionChecks : MonoBehaviour
{
    public void checkCollisionSide(Collider2D collision, string collisionLayerTags, Collider2D selfCollider, out collisionObjectData colliData)
    {
        colliData = new collisionObjectData(0);
        
        if(collision.tag!=null)
            if (collisionLayerTags == collision.tag)
            {
                colliData.collidingUpPt = collision.transform.position.y + collision.bounds.size.y / 2 + collision.offset.y;
                colliData.collidingDownPt = collision.transform.position.y - collision.bounds.size.y / 2 + collision.offset.y;
                colliData.collidingRightPt = collision.transform.position.x + collision.bounds.size.x / 2 + collision.offset.x;
                colliData.collidingLeftPt = collision.transform.position.x - collision.bounds.size.x / 2 + collision.offset.x;

                colliData.selfUpPt = transform.position.y + selfCollider.bounds.size.y / 2 + selfCollider.offset.y;
                colliData.selfDownPt = transform.position.y - selfCollider.bounds.size.y / 2 + selfCollider.offset.y;
                colliData.selfRightPt = transform.position.x + selfCollider.bounds.size.x / 2 + selfCollider.offset.x;
                colliData.selfLeftPt = transform.position.x - selfCollider.bounds.size.x / 2 + selfCollider.offset.x;

                if (colliData.collidingDownPt >= colliData.selfUpPt - 0.2f)
                {
                    colliData.vertical = collisionSide.Up;
                }
                else if(colliData.collidingUpPt <= colliData.selfDownPt + 0.2f)
                {
                    colliData.vertical = collisionSide.Down;
                }

                if(colliData.collidingRightPt >= colliData.selfLeftPt + 0.2f)
                {
                    colliData.horizontal = collisionSide.Right;
                }
                else if(colliData.collidingLeftPt <= colliData.selfRightPt - 0.2f)
                {
                    colliData.horizontal = collisionSide.Left;
                }
            }
    }


    public struct collisionObjectData
    {
        public float collidingUpPt, collidingDownPt, collidingLeftPt, collidingRightPt;
        public float selfUpPt, selfDownPt, selfRightPt, selfLeftPt;
        public collisionSide vertical, horizontal;

        public collisionObjectData(float cUpPt, float cDownPt, float cLeftpt, float cRightPt, float sUpPt, float sDownPt, float sRightPt, float sLeftPt, collisionSide ver, collisionSide hor)
        {
            collidingUpPt = cUpPt;
            collidingDownPt = cDownPt;
            collidingLeftPt = cLeftpt;
            collidingRightPt = cRightPt;
            selfUpPt = sUpPt;
            selfDownPt = sDownPt;
            selfRightPt = sRightPt;
            selfLeftPt = sLeftPt;
            vertical = ver;
            horizontal = hor;
        }

        public collisionObjectData(int a)
        {
            collidingUpPt = collidingDownPt = collidingRightPt = collidingLeftPt = selfUpPt = selfDownPt = selfRightPt = selfLeftPt = 0f;
            vertical = collisionSide.noData;
            horizontal = collisionSide.noData;
        }

        public collisionObjectData(collisionSide ver, collisionSide hor)
        {
            collidingUpPt = collidingDownPt = collidingRightPt = collidingLeftPt = selfUpPt = selfDownPt = selfRightPt = selfLeftPt = 0f;
            vertical = ver;
            horizontal = hor;
        }
    }

    public enum collisionSide
    {
        noData, Up, Right, Left, Down
    }
}
