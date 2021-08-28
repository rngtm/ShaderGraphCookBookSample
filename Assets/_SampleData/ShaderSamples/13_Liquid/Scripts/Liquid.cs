using UnityEngine;

public class Liquid : MonoBehaviour
{
    [SerializeField] float maxWaveSize = 0.3f; // 波の高さ
    [SerializeField] float k = 1f; // 数値を増やすほど波が速く減衰
    [SerializeField] float waveSize = 0f;
    [SerializeField] float waveSpeed = 0f;
    [SerializeField] Vector3 waveDirection = new Vector3(1f, 0f, 0f); // 波の方向
    [SerializeField] float waveSizeInfluenceByMove = 0.001f; // 移動が波の高さに与える影響
    [SerializeField] float waveDirectionInfluenceByMove = 10f; // 移動が波の向きに与える影響
    MeshRenderer meshRenderer;
    Material material;
    Vector3 position;
    Vector3 previousPosition;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
    }

    void Update()
    {
        previousPosition = position;
        position = transform.position;

        float dt = Time.deltaTime;
        Vector3 diff = position - previousPosition;

        // 波の高さの更新
        waveSize += waveSizeInfluenceByMove * diff.magnitude / dt;
        if (waveSize > maxWaveSize) waveSize = maxWaveSize;

        // 波の速度の更新
        waveSpeed = -waveSize * k;
        waveSize += waveSpeed * dt;

        // 波の向きの更新
        if (position != previousPosition)
        {
            diff.y = 0f;
            waveDirection = Vector3.Lerp(waveDirection, diff.normalized, waveDirectionInfluenceByMove * Time.deltaTime).normalized;
            material.SetVector("_WaveDirection", waveDirection);
        }

        material.SetFloat("_WaveSize", waveSize);
    }
}
