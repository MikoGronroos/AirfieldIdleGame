using UnityEngine;

public class GridCreator : MonoBehaviour
{

    [SerializeField] private int height;
    [SerializeField] private int width;
    [SerializeField] private int cellSize;

    private Grid<GridCell> grid;

    public Grid<GridCell> Grid { get { return grid; } }

    #region Singleton

    private static GridCreator _instance;

    public static GridCreator Instance { get { return _instance; } }

    #endregion

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        grid = new Grid<GridCell>(width, height, cellSize, transform.position, (Vector2 pos) => new GridCell(pos));
    }
}

public class GridCell
{

    private GameObject _currentObject;

    public GameObject CurrentObject 
    { 
        get 
        { 
            return _currentObject; 
        } 
        set 
        {
            _currentObject = value;
            if (_currentObject != null)
            {
                _currentObject.transform.position = GetGridPosition();
            }
        }
    }

    public Vector2 GridPosition;

    public Vector3 GetGridPosition()
    {
        return GridCreator.Instance.Grid.GetWorldPosition((int)GridPosition.x, (int)GridPosition.y);
    }

    public GridCell(Vector2 pos)
    {
        GridPosition = pos;
    }

    public bool IsEmpty()
    {
        return CurrentObject == null;
    }

}
