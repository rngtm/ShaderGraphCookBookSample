namespace ShaderGraphCookBook
{
    using UnityEngine;
    using UnityEngine.UI;

    [ExecuteAlways]
    [RequireComponent(typeof(MaskableGraphic))]
    public class UIMaterialProgress : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float progress = 0f; // マテリアルに渡す_Progressの値
        private Image _image = null;
        private RawImage _rawImage = null;
        private readonly int _progressId = Shader.PropertyToID("_Progress"); // シェーダープロパティのReference名

        private void Start()
        {
            Initialize();
        }

        /// <summary>
        /// 描画フレームで呼ばれる
        /// </summary>
        void Update()
        {
            var material = GetMaterial();
            if (material != null)
            {
                // マテリアルのプロパティ _Progress を更新
                material.SetFloat(_progressId, progress);
            }
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        void Initialize()
        {
            var component = GetComponent<MaskableGraphic>();
            _image = component as Image;
            _rawImage = component as RawImage;
        }

        /// <summary>
        /// Imageにアタッチされているマテリアルの取得
        /// </summary>
        Material GetMaterial()
        {
            if (_image != null) return _image.material;
            if (_rawImage != null) return _rawImage.material;
            return null;
        }
    }
}