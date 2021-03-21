namespace ShaderGraphCookBook
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    public class UITransition : MonoBehaviour
    {
        [SerializeField] private float transitionTime = 2f; // 画面遷移の時間
        [SerializeField] private bool loop = false;
        private Material targetMaterial; // 画面遷移ポストエフェクトのマテリアル
        private readonly int _progressId = Shader.PropertyToID("_Progress"); // シェーダープロパティのReference名

        /// <summary>
        /// 開始時に実行
        /// </summary>
        void Start()
        {
            targetMaterial = GetComponent<MaskableGraphic>()?.material;
            
            if (targetMaterial != null)
            {
                StartCoroutine(Transition());
            }
        }

        /// <summary>
        /// 画面遷移
        /// </summary>
        IEnumerator Transition()
        {
            float t = 0f;
            while (t < transitionTime)
            {
                float progress = t / transitionTime;

                // シェーダーの_Progressに値を設定
                targetMaterial.SetFloat(_progressId, progress);
                yield return null;

                t += Time.deltaTime;
            }

            targetMaterial.SetFloat(_progressId, 1.001f);

            if (loop)
            {
                yield return new WaitForSeconds(1f);
                yield return Transition();
            }
        }
    }
}