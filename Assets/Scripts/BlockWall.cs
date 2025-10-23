using UnityEngine;

public class BlockWall : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    [SerializeField] private GameObject _wall;

    private void Start()
    {
        _wall.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(GameModeManager.Mode == GameModeManager.GameMode.Annihilate)
        {
            _wall.SetActive(true);
        }
        else
        {
            _wall.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (GameModeManager.Mode == GameModeManager.GameMode.Explore)
        {
            _wall.SetActive(false);
        }
    }
}
