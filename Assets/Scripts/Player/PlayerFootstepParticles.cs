using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEditor.Animations;
using UnityEditor;


public class PlayerFootstepParticles : MonoBehaviour
{
    public AnimationClip walkAnimation = null;
    public Animator playerAnmiatro = null;
    int index = 0;

    private EditorCurveBinding[] curveBindings;

    public string property1;
    public string property2;

    void Start()
    {
        curveBindings = AnimationUtility.GetCurveBindings(walkAnimation);

        /// Go through the list of curveBindings. 
        /// Get the bindings we want to use (positions) and save
        /// Whenever we want to player a particle effect. Get the current values of those properties
        /// 

        if (curveBindings[0].propertyName == "Animator.Left Foot Q.x")
            {
                index = 0;
            }
    }

    private void GetProperties()
    {
        for (int i = 0; i < curveBindings.Length; i++)
        {
            if (curveBindings[i].propertyName == property1)
            {
            //return curveBindings[i].

            }
        }
    }
}
