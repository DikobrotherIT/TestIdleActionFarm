using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private DynamicJoystick _joystick;
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _cutAnimTime;
    [SerializeField] private GameObject _scythe;
    private bool _isFreezedAnim = false;

    private void FixedUpdate()
    {
        if (_isFreezedAnim == false)
        {
            _playerRigidbody.velocity = new Vector3(_joystick.Horizontal * _movementSpeed, _playerRigidbody.velocity.y, _joystick.Vertical * _movementSpeed);

            if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
            {
                transform.rotation = Quaternion.LookRotation(_playerRigidbody.velocity);
                _playerAnimator.SetBool("isRunning", true);
            }
            else
            {
                _playerAnimator.SetBool("isRunning", false);
            }
        }

    }

    public IEnumerator CutPlants(GameObject plant, Cell cell)
    {
        _scythe.SetActive(true);
        _isFreezedAnim = true;
        _playerRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        this.gameObject.transform.DOLookAt(plant.transform.position, 0.5f, AxisConstraint.X | AxisConstraint.Z);
        _playerAnimator.SetBool("isCutting", true);
        yield return new WaitForSeconds(_cutAnimTime);
        Destroy(plant);
        cell.ResetCell();
        _scythe.GetComponent<Scythe>().ResetScythe();
        this.gameObject.transform.DORotate(new Vector3(0, this.gameObject.transform.eulerAngles.y, 0), 0.1f);
        _playerAnimator.SetBool("isCutting", false);
        _playerRigidbody.constraints = RigidbodyConstraints.None;
        _playerRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        _isFreezedAnim = false;
        _scythe.SetActive(false);
        
    }


}
