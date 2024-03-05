using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private GameObject _chunkPrefab;

    private const int CHUNK_COUNT = 3;
    private float _minX;  // Левая граница камеры
    private float _minY;  // Нижняя граница камеры
    private int _upperChunkPos; // У-позиция верхнего чанка
    [SerializeField] private int _platformCount = 10;
    private Dictionary<int, List<GameObject>> _chunkMap = new Dictionary<int, List<GameObject>>();

    #region ПОДПИСКА НА СОБЫТИЯ
    private void OnEnable() => GlobalActions.OnOffChunk += ResortPlatforms;

    private void OnDisable() => GlobalActions.OnOffChunk -= ResortPlatforms;
    #endregion

    private void Start()
    {
        _minX = CameraController.XBorder + 0.35f; // Левая граница камеры + половина платформы (чтоб не уходила за край экрана)
        _minY = CameraController.YBorder;
        GeneratePlatformsAndChunks();
        _upperChunkPos = 20;
    }

    private void GeneratePlatformsAndChunks()
    {
        for(int i = 0; i < CHUNK_COUNT; i++)
        {
            GameObject chunk = Instantiate(_chunkPrefab, transform, false); // Создание чанка
            chunk.transform.position = new Vector2(0, 10 * i); // Постановка чанков друг над другом
            chunk.name = i.ToString(); // Наименование чанка
            _chunkMap.Add(i, new List<GameObject>());
            float modify = 10 / _platformCount;
            for (float y = _minY; y < Mathf.Abs(_minY); y += modify)
            {
                float x = Random.Range(_minX, Mathf.Abs(_minX));
                Vector2 platformPos = new Vector2(x, y);
                GameObject platform = Instantiate(_platformPrefab, chunk.transform, false);
                platform.transform.localPosition = platformPos;
                _chunkMap[i].Add(platform);
            }
        }
    }

    private void ResortPlatforms(Transform chunk, int chunkIndex)
    {
        _upperChunkPos += 10;
        chunk.position = new Vector3(0, _upperChunkPos, 0); // Задаем новому чанку позицию выше последнего чанка
        
        foreach(GameObject platform in _chunkMap[chunkIndex])
        {
            float x = Random.Range(_minX, Mathf.Abs(_minX));
            platform.transform.position = new Vector2(x, platform.transform.position.y);
        }
    }
}
