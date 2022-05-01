using UnityEngine;

public class CellsSpawner : MonoBehaviour
{
    public GameObject cellPrefab;

    public GridCellsVariable cells;
    public IntVariable gridDimension;

    private void Awake()
    {
        CreateCells();
    }

    [ContextMenu("SpawnCells")]
    private void CreateCells()
    {
        var dimension = gridDimension.value;
        cells.value = new Cell[dimension, dimension, dimension];
        float halfCellSize = cellPrefab.transform.localScale.x / 2;
        var residual = (cells.dimension.value + 1) % 2;
        int border = cells.dimension.value / 2;
        for (int i = 0; i < cells.dimension.value; i++)
        {
            for (int j = 0; j < cells.dimension.value; j++)
            {
                for (int k = 0; k < cells.dimension.value; k++)
                {
                    float shiftX = (i - border) * 2 + residual;
                    float shiftY = (j - border) * 2 + residual;
                    float shiftZ = (k - border) * 2 + residual;
                    GameObject go = Instantiate(cellPrefab,
                        new Vector3(halfCellSize * shiftX, halfCellSize * shiftY, halfCellSize * shiftZ),
                        Quaternion.identity,
                        transform);
                    cells.value[i, j, k] = go.GetComponent<Cell>();
                }
            }
        }
    }
}