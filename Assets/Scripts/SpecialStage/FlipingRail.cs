using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipingRail : RailMovement
{
    bool lookingRigth = true;
    protected override void Update()
    {
        base.Update();

        if(movePoints[currentIndex].position.x < transform.position.x && lookingRigth)
        {
            Flip();
        }

        if (movePoints[currentIndex].position.x > transform.position.x && !lookingRigth)
        {
            Flip();
        }

        
    }

    private void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        lookingRigth = !lookingRigth;
    }
}
