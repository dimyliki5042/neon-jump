using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private GameObject _chunkPrefab;

    private const int CHUNK_COUNT = 3;
    private Transform[] _chunkQueue;

    #region ПОДПИСКА НА СОБЫТИЯ
    private void OnEnable() => ChunkChecker.OnOffChunk += Regenerate;

    private void OnDisable() => ChunkChecker.OnOffChunk -= Regenerate;
    #endregion

    private void Awake() => _chunkQueue = new Transform[CHUNK_COUNT];

    private void Start() => FirstGenerate();

    private void FirstGenerate()
    {
        for(int i = 0; i < CHUNK_COUNT; i++)
        {
            GameObject chunk = Instantiate(_chunkPrefab, transform, false); // Создание чанка
            chunk.transform.position = new Vector2(0, 10 * i); // Постановка чанков друг над другом
            chunk.name = i.ToString(); // Наименование чанка
            _chunkQueue[i] = chunk.transform; // Добавление чанка в очередь
            for (int j = -5; j < 5; j++)
            {
                float x = Random.Range(-2.15f, 2.15f);
                float y = Random.Range(j, j + 1);
                Vector2 platformPos = new Vector2(x, y);
                GameObject platform = Instantiate(_platformPrefab, chunk.transform, false);
                platform.transform.localPosition = platformPos;
            }
        }
    }

    private void Regenerate(int chunkIndex)
    {
        Transform chunk = _chunkQueue[0]; // Получаем первый в очереди чанк (по логике игры его и покинул игрок)
        float yPosition = _chunkQueue[_chunkQueue.Length - 1].position.y; // Получаем у-координату самого верхнего чанка
        chunk.position = new Vector3(0, yPosition + 10, 0); // Задаем новому чанку позицию выше последнего чанка
        ClearChunk(chunk); // Очищаем все дочерние объекты чанка

        for (int j = -5; j < 5; j++)
        {
            float x = Random.Range(-2.15f, 2.15f);
            float y = Random.Range(j, j + 1);
            Vector2 platformPos = new Vector2(x, y);
            GameObject platform = Instantiate(_platformPrefab, chunk, false);
            platform.transform.localPosition = platformPos;
        }

        ResortArray(); // Сортировка очереди чанков
    }

    private void ClearChunk(Transform chunk)
    {
        if (chunk.childCount != 0) // Если в чанке есть платформы
        {
            for (int i = chunk.childCount - 1; i != 0; i--)
                Destroy(chunk.GetChild(i).gameObject);
        }
    }

    private void ResortArray()
    {
        Queue<Transform> queue = new Queue<Transform>(_chunkQueue);
        Transform a = queue.Dequeue(); // Вытаскиваем первый элемент в очереди (первый чанк)
        queue.Enqueue(a); // Заносим его в конец очереди
        _chunkQueue = queue.ToArray(); // Присваиваем очередь массиву
    }
}
