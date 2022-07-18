using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBlock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            player.GetComponent<Backpack>().AddBlockInBackpack(this.gameObject);
        }
    }

    [SerializeField] private float followSpeed;

    public void UpdateCubePosition(Transform followedCube, bool isFollowStart)
    {
        StartCoroutine(StartFollowingToLastCubePosition(followedCube, isFollowStart));
    }

    IEnumerator StartFollowingToLastCubePosition(Transform followedCube, bool isFollowStart)
    {

        while (isFollowStart)
        {
            yield return new WaitForFixedUpdate();
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, followedCube.position.x, followSpeed * Time.deltaTime),
                transform.position.y,
                Mathf.Lerp(transform.position.z, followedCube.position.z, followSpeed * Time.deltaTime));
        }
    }
}
