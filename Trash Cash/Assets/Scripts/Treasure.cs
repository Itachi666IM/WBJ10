using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField] private float minTreasureValue;
    [SerializeField] private float maxTreasureValue;
    private float treasureValue;

    private void Start()
    {
        treasureValue = Random.Range(minTreasureValue, maxTreasureValue);
    }

    public void AddCash()
    {
        TrashCan.Instance.cash += treasureValue;
        TrashCan.Instance.UpdateCash();
        SFX.Instance.PlayTreasure();
        Destroy(gameObject);
    }
}
