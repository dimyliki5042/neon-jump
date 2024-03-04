using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private GameObject _chunkPrefab;

    private const int CHUNK_COUNT = 3;
    [SerializeField] private int _platformCount = 10;
    private Transform[] _chunkQueue;

    #region �������� �� �������
    private void OnEnable() => GlobalActions.OnOffChunk += Regenerate;

    private void OnDisable() => GlobalActions.OnOffChunk -= Regenerate;
    #endregion

    private void Awake() => _chunkQueue = new Transform[CHUNK_COUNT];

    private void Start() => FirstGenerate();

    private void FirstGenerate()
    {
        for(int i = 0; i < CHUNK_COUNT; i++)
        {
            GameObject chunk = Instantiate(_chunkPrefab, transform, false); // �������� �����
            chunk.transform.position = new Vector2(0, 10 * i); // ���������� ������ ���� ��� ������
            chunk.name = i.ToString(); // ������������ �����
            _chunkQueue[i] = chunk.transform; // ���������� ����� � �������
            for (int j = 0; j < _platformCount; j++)
            {
                float x = Random.Range(-2.15f, 2.15f);
                float y = Random.Range(j, j + 1);
                Vector2 platformPos = new Vector2(x, y - 5);
                GameObject platform = Instantiate(_platformPrefab, chunk.transform, false);
                platform.transform.localPosition = platformPos;
            }
        }
    }

    private void Regenerate(int chunkIndex)
    {
        Transform chunk = _chunkQueue[0]; // �������� ������ � ������� ���� (�� ������ ���� ��� � ������� �����)
        float yPosition = _chunkQueue[_chunkQueue.Length - 1].position.y; // �������� �-���������� ������ �������� �����
        chunk.position = new Vector3(0, yPosition + 10, 0); // ������ ������ ����� ������� ���� ���������� �����
        ClearChunk(chunk); // ������� ��� �������� ������� �����

        for (int j = 0; j < _platformCount; j++)
        {
            float x = Random.Range(-2.15f, 2.15f);
            float y = Random.Range(j, j + 1);
            Vector2 platformPos = new Vector2(x, y - 5);
            GameObject platform = Instantiate(_platformPrefab, chunk, false);
            platform.transform.localPosition = platformPos;
        }

        ResortArray(); // ���������� ������� ������
    }

    private void ClearChunk(Transform chunk)
    {
        if (chunk.childCount != 0) // ���� � ����� ���� ���������
        {
            for (int i = chunk.childCount - 1; i != 0; i--)
                Destroy(chunk.GetChild(i).gameObject);
        }
    }

    private void ResortArray()
    {
        Queue<Transform> queue = new Queue<Transform>(_chunkQueue);
        Transform a = queue.Dequeue(); // ����������� ������ ������� � ������� (������ ����)
        queue.Enqueue(a); // ������� ��� � ����� �������
        _chunkQueue = queue.ToArray(); // ����������� ������� �������
    }
}
