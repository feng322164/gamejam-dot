using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DayCounter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI References")]
    [SerializeField] private Image clickableImage;
    [SerializeField] private Image closeButtonImage;
    [SerializeField] private Image additionalImage;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI additionalText;

    [Header("Hover Settings")]
    [SerializeField] private Color closeButtonHoverColor = new Color(1f, 0.8f, 0.8f, 1f); // 鼠标悬停时关闭按钮的颜色
    [SerializeField] private float colorChangeDuration = 0.2f; // 颜色变化持续时间

    [Header("Other Settings")]
    [SerializeField] private int maxDays = 6;
    [SerializeField] private string textPrefix = "Day ";

    private int currentDay = 1;
    private Button imageButton;
    private Button closeButton;
    private Button additionalTextButton;
    private Color closeButtonOriginalColor; // 关闭按钮的原始颜色
    private bool isHoveringAdditionalText = false;

    void Start()
    {
        InitializeComponents();
        UpdateDayText();

        // 保存关闭按钮的原始颜色
        if (closeButtonImage != null)
        {
            closeButtonOriginalColor = closeButtonImage.color;
        }
    }

    private void InitializeComponents()
    {
        // 初始化可点击的Image
        if (clickableImage != null)
        {
            imageButton = clickableImage.GetComponent<Button>();
            if (imageButton == null)
            {
                imageButton = clickableImage.gameObject.AddComponent<Button>();
            }

            imageButton.onClick.RemoveAllListeners();
            imageButton.onClick.AddListener(OnImageClicked);
        }
        else
        {
            Debug.LogError("Clickable Image is not assigned!");
        }

        // 初始化关闭按钮Image
        if (closeButtonImage != null)
        {
            closeButton = closeButtonImage.GetComponent<Button>();
            if (closeButton == null)
            {
                closeButton = closeButtonImage.gameObject.AddComponent<Button>();
            }

            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(OnCloseButtonClicked);
        }
        else
        {
            Debug.LogError("Close Button Image is not assigned!");
        }

        // 初始化第二个文本的点击功能
        if (additionalText != null)
        {
            additionalTextButton = additionalText.GetComponent<Button>();
            if (additionalTextButton == null)
            {
                additionalTextButton = additionalText.gameObject.AddComponent<Button>();
            }

            additionalTextButton.onClick.RemoveAllListeners();
            additionalTextButton.onClick.AddListener(OnCloseButtonClicked);

            // 为第二个文本添加事件触发器来处理鼠标悬停
            AddEventTriggerToAdditionalText();
        }
        else
        {
            Debug.LogWarning("Additional TextMeshPro is not assigned, but it's optional.");
        }

        if (dayText == null)
        {
            Debug.LogError("Day TextMeshPro is not assigned!");
        }

        if (additionalImage == null)
        {
            Debug.LogWarning("Additional Image is not assigned, but it's optional.");
        }
    }

    // 为第二个文本添加事件触发器
    private void AddEventTriggerToAdditionalText()
    {
        EventTrigger eventTrigger = additionalText.GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = additionalText.gameObject.AddComponent<EventTrigger>();
        }

        // 清除现有的事件
        eventTrigger.triggers.Clear();

        // 添加鼠标进入事件
        EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry();
        pointerEnterEntry.eventID = EventTriggerType.PointerEnter;
        pointerEnterEntry.callback.AddListener((data) => { OnPointerEnterAdditionalText((PointerEventData)data); });
        eventTrigger.triggers.Add(pointerEnterEntry);

        // 添加鼠标离开事件
        EventTrigger.Entry pointerExitEntry = new EventTrigger.Entry();
        pointerExitEntry.eventID = EventTriggerType.PointerExit;
        pointerExitEntry.callback.AddListener((data) => { OnPointerExitAdditionalText((PointerEventData)data); });
        eventTrigger.triggers.Add(pointerExitEntry);
    }

    // 鼠标进入第二个文本时的处理
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 这个接口方法是为了实现IPointerEnterHandler，但我们会用自定义方法
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 这个接口方法是为了实现IPointerExitHandler，但我们会用自定义方法
    }

    private void OnPointerEnterAdditionalText(PointerEventData eventData)
    {
        isHoveringAdditionalText = true;
        ChangeCloseButtonColor(closeButtonHoverColor);
    }

    private void OnPointerExitAdditionalText(PointerEventData eventData)
    {
        isHoveringAdditionalText = false;
        ChangeCloseButtonColor(closeButtonOriginalColor);
    }

    // 改变关闭按钮颜色（带平滑过渡）
    private void ChangeCloseButtonColor(Color targetColor)
    {
        if (closeButtonImage != null)
        {
            // 使用LeanTween实现平滑颜色过渡（如果没有LeanTween，可以使用协程）
            if (closeButtonImage.gameObject.activeInHierarchy)
            {
                closeButtonImage.CrossFadeColor(targetColor, colorChangeDuration, true, true);
            }
            else
            {
                closeButtonImage.color = targetColor;
            }
        }
    }

    // 点击主Image时的处理
    private void OnImageClicked()
    {
        if (currentDay >= maxDays) return;

        // 增加天数
        currentDay++;

        // 更新文本显示
        UpdateDayText();

        // 检查是否达到最大天数
        if (currentDay >= maxDays)
        {
            DisableImageInteraction();
            Debug.Log("已达到最大天数：" + maxDays + "，Image已禁用交互");
        }
    }

    // 点击关闭按钮时的处理
    private void OnCloseButtonClicked()
    {
        HideAllUIElements();
        Debug.Log("所有UI元素已隐藏（由关闭按钮或第二个文本触发）");
    }

    // 隐藏所有UI元素
    private void HideAllUIElements()
    {
        // 隐藏clickableImage
        if (clickableImage != null)
        {
            clickableImage.gameObject.SetActive(false);
        }

        // 隐藏closeButtonImage
        if (closeButtonImage != null)
        {
            closeButtonImage.gameObject.SetActive(false);
        }

        // 隐藏additionalImage
        if (additionalImage != null)
        {
            additionalImage.gameObject.SetActive(false);
        }

        // 隐藏dayText
        if (dayText != null)
        {
            dayText.gameObject.SetActive(false);
        }

        // 隐藏additionalText
        if (additionalText != null)
        {
            additionalText.gameObject.SetActive(false);
        }
    }

    // 显示所有UI元素（可选功能）
    public void ShowAllUIElements()
    {
        if (clickableImage != null)
        {
            clickableImage.gameObject.SetActive(true);
        }

        if (closeButtonImage != null)
        {
            closeButtonImage.gameObject.SetActive(true);
            // 恢复原始颜色
            closeButtonImage.color = closeButtonOriginalColor;
        }

        if (additionalImage != null)
        {
            additionalImage.gameObject.SetActive(true);
        }

        if (dayText != null)
        {
            dayText.gameObject.SetActive(true);
        }

        if (additionalText != null)
        {
            additionalText.gameObject.SetActive(true);
        }

        // 重置交互状态
        if (imageButton != null && currentDay < maxDays)
        {
            imageButton.interactable = true;
        }

        // 恢复Image外观
        if (clickableImage != null && currentDay < maxDays)
        {
            Color enabledColor = clickableImage.color;
            enabledColor.a = 1f;
            clickableImage.color = enabledColor;
        }

        isHoveringAdditionalText = false;
    }

    // 更新天数文本
    private void UpdateDayText()
    {
        if (dayText != null)
        {
            dayText.text = textPrefix + currentDay;
        }
    }

    // 禁用Image的交互
    private void DisableImageInteraction()
    {
        if (imageButton != null)
        {
            imageButton.interactable = false;
        }

        // 可选：改变Image的外观表示已禁用
        if (clickableImage != null)
        {
            Color disabledColor = clickableImage.color;
            disabledColor.a = 0.5f; // 半透明表示禁用
            clickableImage.color = disabledColor;
        }
    }

    // 重置天数计数和显示状态（可选功能）
    public void ResetCounter()
    {
        currentDay = 1;
        UpdateDayText();

        // 重新启用Image交互
        if (imageButton != null)
        {
            imageButton.interactable = true;
        }

        // 恢复Image外观
        if (clickableImage != null)
        {
            Color enabledColor = clickableImage.color;
            enabledColor.a = 1f; // 完全不透明
            clickableImage.color = enabledColor;
        }

        // 确保所有UI元素显示
        ShowAllUIElements();

        Debug.Log("天数计数器和UI显示已重置");
    }

    // 获取当前天数（只读）
    public int GetCurrentDay()
    {
        return currentDay;
    }

    // 在Inspector中修改值时进行验证
    private void OnValidate()
    {
        maxDays = Mathf.Max(2, maxDays); // 至少需要2天
        colorChangeDuration = Mathf.Max(0f, colorChangeDuration);
    }

    // 手动设置天数（可选，用于测试）
    public void SetCurrentDay(int day)
    {
        if (day >= 1 && day <= maxDays)
        {
            currentDay = day;
            UpdateDayText();

            // 如果设置的天数达到最大值，禁用交互
            if (currentDay >= maxDays)
            {
                DisableImageInteraction();
            }
            else if (imageButton != null)
            {
                imageButton.interactable = true;
            }
        }
    }

    // 设置关闭按钮悬停颜色
    public void SetCloseButtonHoverColor(Color color)
    {
        closeButtonHoverColor = color;
        if (isHoveringAdditionalText)
        {
            ChangeCloseButtonColor(closeButtonHoverColor);
        }
    }

    // 设置颜色变化持续时间
    public void SetColorChangeDuration(float duration)
    {
        colorChangeDuration = Mathf.Max(0f, duration);
    }

    // 设置TextMeshPro的字体样式（可选扩展功能）
    public void SetTextStyle(TMP_FontAsset font, float fontSize, Color color)
    {
        if (dayText != null)
        {
            if (font != null) dayText.font = font;
            dayText.fontSize = fontSize;
            dayText.color = color;
        }
    }

    // 设置第二个TextMeshPro的字体样式
    public void SetAdditionalTextStyle(TMP_FontAsset font, float fontSize, Color color)
    {
        if (additionalText != null)
        {
            if (font != null) additionalText.font = font;
            additionalText.fontSize = fontSize;
            additionalText.color = color;
        }
    }

    // 设置第二个TextMeshPro的内容
    public void SetAdditionalText(string text)
    {
        if (additionalText != null)
        {
            additionalText.text = text;
        }
    }

    // 设置第二个UI Image的精灵
    public void SetAdditionalImageSprite(Sprite sprite)
    {
        if (additionalImage != null && sprite != null)
        {
            additionalImage.sprite = sprite;
        }
    }

    // 设置第二个UI Image的颜色
    public void SetAdditionalImageColor(Color color)
    {
        if (additionalImage != null)
        {
            additionalImage.color = color;
        }
    }

    // 设置文本前缀
    public void SetTextPrefix(string prefix)
    {
        textPrefix = prefix;
        UpdateDayText();
    }

    // 启用/禁用第二个文本的点击功能
    public void SetAdditionalTextClickable(bool clickable)
    {
        if (additionalTextButton != null)
        {
            additionalTextButton.interactable = clickable;
        }
    }

    // 检查UI元素是否可见
    public bool AreUIElementsVisible()
    {
        bool clickableVisible = clickableImage != null && clickableImage.gameObject.activeInHierarchy;
        bool closeButtonVisible = closeButtonImage != null && closeButtonImage.gameObject.activeInHierarchy;
        bool additionalImageVisible = additionalImage != null && additionalImage.gameObject.activeInHierarchy;
        bool dayTextVisible = dayText != null && dayText.gameObject.activeInHierarchy;
        bool additionalTextVisible = additionalText != null && additionalText.gameObject.activeInHierarchy;

        return clickableVisible && closeButtonVisible && additionalImageVisible && dayTextVisible && additionalTextVisible;
    }

    // 单独显示/隐藏第二个UI Image（可选功能）
    public void SetAdditionalImageVisible(bool visible)
    {
        if (additionalImage != null)
        {
            additionalImage.gameObject.SetActive(visible);
        }
    }

    // 单独显示/隐藏第二个TextMeshPro（可选功能）
    public void SetAdditionalTextVisible(bool visible)
    {
        if (additionalText != null)
        {
            additionalText.gameObject.SetActive(visible);
        }
    }

    // 检查是否正在悬停第二个文本
    public bool IsHoveringAdditionalText()
    {
        return isHoveringAdditionalText;
    }
}
