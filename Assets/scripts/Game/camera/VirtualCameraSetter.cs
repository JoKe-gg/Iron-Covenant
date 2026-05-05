using Cinemachine;
using UnityEngine;

public class VirtualCameraSetter : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    private void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        GameObject player = PlayerSpawnManager.CurrentPlayer;
        _virtualCamera.Follow = player.transform;
    }
}
