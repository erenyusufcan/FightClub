using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassicProgressBar : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] private Color m_MainColor = Color.white;

    [Header("Fill Gradient")]
    [SerializeField] private Color m_Green = new Color(0.1f, 1f, 0.2f);
    [SerializeField] private Color m_Yellow = new Color(1f, 0.9f, 0.1f);
    [SerializeField] private Color m_Orange = new Color(1f, 0.55f, 0.1f);
    [SerializeField] private Color m_Red = new Color(1f, 0.15f, 0.15f);

    [Header("General")]
    [SerializeField] private int m_NumberOfSegments = 5;
    [SerializeField] private float m_SizeOfNotch = 5;
    [Range(0, 1f)][SerializeField] private float m_FillAmount = 0.0f;

    private RectTransform m_RectTransform;
    private Image m_Image;
    private readonly List<Image> m_ProgressToFill = new List<Image>();
    private float m_SizeOfSegment;

    // ✅ Dışarıdan kontrol
    public void SetFill(float value01) => m_FillAmount = Mathf.Clamp01(value01);
    public float GetFill() => m_FillAmount;

    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();

        // Template image (child)
        m_Image = GetComponentInChildren<Image>(true);
        m_Image.color = m_MainColor;
        m_Image.gameObject.SetActive(false);

        // Önceden üretilmiş segmentleri temizle (Play-Stop tekrarlarında sorun olmasın)
        m_ProgressToFill.Clear();
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            if (transform.GetChild(i).gameObject != m_Image.gameObject)
                Destroy(transform.GetChild(i).gameObject);
        }

        // Segment genişliği
        m_SizeOfSegment = m_RectTransform.sizeDelta.x / m_NumberOfSegments;

        // UI için start X (ortalamak)
        float totalNotch = (m_NumberOfSegments - 1) * m_SizeOfNotch;
        float totalWidth = (m_NumberOfSegments * m_SizeOfSegment) + totalNotch;
        float startX = -totalWidth / 2f + m_SizeOfSegment / 2f;

        for (int i = 0; i < m_NumberOfSegments; i++)
        {
            GameObject currentSegment = Instantiate(m_Image.gameObject, transform);
            currentSegment.SetActive(true);

            Image segmentImage = currentSegment.GetComponent<Image>();
            segmentImage.fillAmount = 1f; // ✅ 0..1 arası olmalı

            RectTransform segRT = segmentImage.GetComponent<RectTransform>();
            segRT.sizeDelta = new Vector2(m_SizeOfSegment, segRT.sizeDelta.y);

            float x = startX + i * (m_SizeOfSegment + m_SizeOfNotch);
            segRT.anchoredPosition = new Vector2(x, 0f);
            segRT.localRotation = Quaternion.identity;
            segRT.localScale = Vector3.one;

            // Fill child (burayı boyuyoruz)
            Image segmentFillImage = segmentImage.transform.GetChild(0).GetComponent<Image>();

            RectTransform fillRT = segmentFillImage.GetComponent<RectTransform>();
            fillRT.sizeDelta = new Vector2(m_SizeOfSegment, fillRT.sizeDelta.y);

            m_ProgressToFill.Add(segmentFillImage);
        }
    }

    private void Update()
    {
        // ✅ Fill rengi: yeşil -> sarı -> turuncu -> kırmızı
        Color c = EvaluateChargeColor(m_FillAmount);
        for (int i = 0; i < m_ProgressToFill.Count; i++)
            m_ProgressToFill[i].color = c;

        // Segment dolum mantığı
        for (int i = 0; i < m_NumberOfSegments; i++)
        {
            m_ProgressToFill[i].fillAmount = m_NumberOfSegments * m_FillAmount - i;
        }
    }

    private Color EvaluateChargeColor(float t)
    {
        t = Mathf.Clamp01(t);

        if (t <= 0.5f)
            return Color.Lerp(m_Green, m_Yellow, t / 0.5f);

        if (t <= 0.8f)
            return Color.Lerp(m_Yellow, m_Orange, (t - 0.5f) / 0.3f);

        return Color.Lerp(m_Orange, m_Red, (t - 0.8f) / 0.2f);
    }
}
