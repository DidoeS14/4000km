using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelLevel : MonoBehaviour
{
    [SerializeField, Range(1, 5)] public int partLevel = 1;
    [SerializeField] SpriteRenderer renderer;
    [SerializeField] Sprite[] sprites;
    [SerializeField] WheelCollider wheel;

    // Optimmization Idea:
    //implement this

    /* 
     * In that case, you may want to use a formula that gives you values similar to the ones you provided originally for level 1 and level 5, while still allowing you to adjust the values for the intermediate levels.

One option is to use a formula that gives you a linear progression from level 1 to level 5, but with an adjustable slope. For example:

Copy code
wheel.suspensionDistance = 0.02f + (partLevel - 1) * slope1;
wheel.radius = 0.05f + (partLevel - 1) * slope2;
You can adjust the values of slope1 and slope2 to control the rate at which the values of suspensionDistance and radius change as you go from level 1 to level 5.

For example, if you want the values at level 5 to be similar to the ones you provided originally, you can set slope1 and slope2 so that the values at level 5 are close to the original values:

Copy code
float slope1 = (0.2f - 0.02f) / 4.0f; // (final value - initial value) / (number of levels - 1)
float slope2 = (0.09f - 0.05f) / 4.0f;

wheel.suspensionDistance = 0.02f + (partLevel - 1) * slope1;
wheel.radius = 0.05f + (partLevel - 1) * slope2;
This would give you the following values for suspensionDistance and radius:

Level 1: suspensionDistance = 0.02, radius = 0.05
Level 2: suspensionDistance = 0.05, radius = 0.06
Level 3: suspensionDistance = 0.08, radius = 0.07
Level 4: suspensionDistance = 0.11, radius = 0.08
Level 5: suspensionDistance = 0.14, radius = 0.09
I hope this helps! Let me know if you have any more questions.
     */
    void Update()
    {
        switch (partLevel)
        {
            case 1:
                renderer.sprite = sprites[0];
                wheel.suspensionDistance = .02f;
                wheel.radius = .05f;
                break;
            case 2:
                renderer.sprite = sprites[1];
                wheel.suspensionDistance = .02f;
                wheel.radius = 0.055f;
                break;
            case 3:
                renderer.sprite = sprites[2];
                wheel.suspensionDistance = .04f;
                wheel.radius =  0.06f;
                break;
            case 4:
                renderer.sprite = sprites[3];
                wheel.suspensionDistance = .06f;
                wheel.radius =  0.075f;
                break;
            case 5:
                renderer.sprite = sprites[4];
                wheel.suspensionDistance = .07f;
                wheel.radius =  0.09f;
                break;
        }
    }
}
