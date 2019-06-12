using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class FollowBehaviour : StateMachineBehaviour
{
    private Transform playerPosition;
    public float FollowSpeed;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(animator.transform.position, playerPosition.position) > 1f)
            animator.transform.position = new Vector3(SmoothStep(animator.transform.position.x, playerPosition.position.x, FollowSpeed * Time.deltaTime), SmoothStep(animator.transform.position.y, playerPosition.position.y, FollowSpeed * Time.deltaTime),0);
    }
}
