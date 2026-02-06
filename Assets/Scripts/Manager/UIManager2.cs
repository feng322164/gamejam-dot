using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DayCounter : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image clickableImage;
    [SerializeField] private Image closeButtonImage;
    [SerializeField] private Image additionalImage;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI additionalText;

    [Header("Hover Settings")]
    [SerializeField] private Color closeButtonHoverColor = new Color(1f, 0.8f, 0.8f, 1f);
    [SerializeField] private float clickableImageBrightness = 1.3f; // 悬停时变亮倍数
    [SerializeField] private float clickableImageDarkness = 0.8f;   // 正常时变暗倍数
    [SerializeField] private float colorChangeDuration = 0.2f;

    [Header("Other Settings")]
    [SerializeField] private int maxDays = 6;
    [SerializeField] private string textPrefix = "Day ";

    private int currentDay = 1;
    private Button imageButton;
    private Button closeButton;
    private Button additionalTextButton;
    private Color clickableImageOriginalColor;
    private bool isHoveringAdditionalText = false;
    private bool isHoveringClickableImage = false;
    private EventTrigger clickableImageEventTrigger; // 保存事件触发器引用

    void Start()
    {
        InitializeComponents();
        UpdateDayText();

        if (clickableImage != null)
        {
            clickableImageOriginalColor = clickableImage.color;
            // 初始状态设为变暗
            SetClickableImageDark();
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

            // 添加悬停事件
            AddHoverEventsToClickableImage();
        }

        // 初始化关闭按钮
        if (closeButtonImage != null)
        {
            closeButton = closeButtonImage.GetComponent<Button>();
            if (closeButton == null)
            {
                closeButton = closeButtonImage.gameObject.AddComponent<Button>();
            }
            closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        // 初始化第二个文本
        if (additionalText != null)
        {
            additionalTextButton = additionalText.GetComponent<Button>();
            if (additionalTextButton == null)
            {
                additionalTextButton = additionalText.gameObject.AddComponent<Button>();
            }
            additionalTextButton.onClick.AddListener(OnCloseButtonClicked);

            AddHoverEventsToImage(additionalText, OnPointerEnterAdditionalText, OnPointerExitAdditionalText);
        }
    }

    // 为clickable image添加悬停事件
    private void AddHoverEventsToClickableImage()
    {
        clickableImageEventTrigger = clickableImage.GetComponent<EventTrigger>();
        if (clickableImageEventTrigger == null)
        {
            clickableImageEventTrigger = clickableImage.gameObject.AddComponent<EventTrigger>();
        }

        // 添加悬停事件
        AddHoverEventsToEventTrigger(clickableImageEventTrigger, OnPointerEnterClickableImage, OnPointerExitClickableImage);
    }

    // 为其他UI元素添加悬停事件
    private void AddHoverEventsToImage(Graphic uiElement, UnityEngine.Events.UnityAction<PointerEventData> enterAction,
                                     UnityEngine.Events.UnityAction<PointerEventData> exitAction)
    {
        EventTrigger eventTrigger = uiElement.GetComponent<EventTrigger>();
        if (eventTrigger == null) eventTrigger = uiElement.gameObject.AddComponent<EventTrigger>();

        AddHoverEventsToEventTrigger(eventTrigger, enterAction, exitAction);
    }

    // 为事件触发器添加悬停事件
    private void AddHoverEventsToEventTrigger(EventTrigger eventTrigger, UnityEngine.Events.UnityAction<PointerEventData> enterAction,
                                            UnityEngine.Events.UnityAction<PointerEventData> exitAction)
    {
        eventTrigger.triggers.Clear();

        EventTrigger.Entry enterEntry = new EventTrigger.Entry();
        enterEntry.eventID = EventTriggerType.PointerEnter;
        enterEntry.callback.AddListener((data) => enterAction((PointerEventData)data));
        eventTrigger.triggers.Add(enterEntry);

        EventTrigger.Entry exitEntry = new EventTrigger.Entry();
        exitEntry.eventID = EventTriggerType.PointerExit;
        exitEntry.callback.AddListener((data) => exitAction((PointerEventData)data));
        eventTrigger.triggers.Add(exitEntry);
    }

    // 移除clickable image的悬停事件
    private void RemoveHoverEventsFromClickableImage()
    {
        if (clickableImageEventTrigger != null)
        {
            clickableImageEventTrigger.triggers.Clear();
        }
    }

    // 恢复clickable image的悬停事件
    private void RestoreHoverEventsToClickableImage()
    {
        if (clickableImageEventTrigger != null)
        {
            AddHoverEventsToEventTrigger(clickableImageEventTrigger, OnPointerEnterClickableImage, OnPointerExitClickableImage);
        }
    }

    private void OnPointerEnterClickableImage(PointerEventData eventData)
    {
        isHoveringClickableImage = true;
        SetClickableImageBright(); // 鼠标进入时变亮
    }

    private void OnPointerExitClickableImage(PointerEventData eventData)
    {
        isHoveringClickableImage = false;
        SetClickableImageDark(); // 鼠标离开时变暗
    }

    private void OnPointerEnterAdditionalText(PointerEventData eventData)
    {
        isHoveringAdditionalText = true;
        if (closeButtonImage != null)
            closeButtonImage.CrossFadeColor(closeButtonHoverColor, colorChangeDuration, true, true);
    }

    private void OnPointerExitAdditionalText(PointerEventData eventData)
    {
        isHoveringAdditionalText = false;
        if (closeButtonImage != null)
            closeButtonImage.CrossFadeColor(Color.white, colorChangeDuration, true, true);
    }

    // 设置clickable image变亮
    private void SetClickableImageBright()
    {
        if (clickableImage != null && clickableImage.gameObject.activeInHierarchy)
        {
            Color brightColor = GetModifiedColor(clickableImageOriginalColor, clickableImageBrightness);
            clickableImage.CrossFadeColor(brightColor, colorChangeDuration, true, true);
        }
    }

    // 设置clickable image变暗
    private void SetClickableImageDark()
    {
        if (clickableImage != null && clickableImage.gameObject.activeInHierarchy)
        {
            Color darkColor = GetModifiedColor(clickableImageOriginalColor, clickableImageDarkness);
            clickableImage.CrossFadeColor(darkColor, colorChangeDuration, true, true);
        }
    }

    // 获取修改后的颜色（亮度/暗度调整）
    private Color GetModifiedColor(Color baseColor, float multiplier)
    {
        return new Color(
            Mathf.Clamp01(baseColor.r * multiplier),
            Mathf.Clamp01(baseColor.g * multiplier),
            Mathf.Clamp01(baseColor.b * multiplier),
            baseColor.a
        );
    }

    private void OnImageClicked()
    {
        if (currentDay >= maxDays) return;

        currentDay++;
        UpdateDayText();

        if (currentDay >= maxDays)
        {
            DisableImageInteraction();
        }
    }

    // 禁用点击交互并移除悬停效果
    private void DisableImageInteraction()
    {
        if (imageButton != null)
        {
            imageButton.interactable = false;
        }

        // 移除悬停事件
        RemoveHoverEventsFromClickableImage();

        // 设置禁用状态的外观
        Color disabledColor = GetModifiedColor(clickableImageOriginalColor, clickableImageDarkness);
        disabledColor.a = 0.5f; // 半透明表示禁用
        if (clickableImage != null)
        {
            clickableImage.color = disabledColor;
        }

        Debug.Log("已达到最大天数：" + maxDays + "，Image已禁用点击功能和悬停效果");
    }

    // 启用点击交互并恢复悬停效果
    private void EnableImageInteraction()
    {
        if (imageButton != null)
        {
            imageButton.interactable = true;
        }

        // 恢复悬停事件
        RestoreHoverEventsToClickableImage();

        // 恢复正常外观
        if (clickableImage != null)
        {
            clickableImage.color = GetModifiedColor(clickableImageOriginalColor, clickableImageDarkness);
        }
    }

    private void OnCloseButtonClicked()
    {
        HideAllUIElements();
    }

    private void HideAllUIElements()
    {
        SetUIVisibility(clickableImage, false);
        SetUIVisibility(closeButtonImage, false);
        SetUIVisibility(additionalImage, false);
        SetUIVisibility(dayText, false);
        SetUIVisibility(additionalText, false);
    }

    public void ShowAllUIElements()
    {
        SetUIVisibility(clickableImage, true);
        SetUIVisibility(closeButtonImage, true);
        SetUIVisibility(additionalImage, true);
        SetUIVisibility(dayText, true);
        SetUIVisibility(additionalText, true);

        // 根据当前天数决定是否启用交互
        if (currentDay >= maxDays)
        {
            DisableImageInteraction();
        }
        else
        {
            EnableImageInteraction();
        }

        // 显示时恢复变暗状态
        SetClickableImageDark();
    }

    private void SetUIVisibility(Behaviour uiElement, bool visible)
    {
        if (uiElement != null) uiElement.gameObject.SetActive(visible);
    }

    private void UpdateDayText()
    {
        if (dayText != null) dayText.text = textPrefix + currentDay;
    }

    public void ResetCounter()
    {
        currentDay = 1;
        UpdateDayText();
        ShowAllUIElements();
    }

    // 公共方法
    public void SetClickableImageBrightness(float brightness)
    {
        clickableImageBrightness = Mathf.Clamp(brightness, 1f, 2f);
        if (isHoveringClickableImage && clickableImage != null && currentDay < maxDays)
        {
            SetClickableImageBright();
        }
    }

    public void SetClickableImageDarkness(float darkness)
    {
        clickableImageDarkness = Mathf.Clamp(darkness, 0.5f, 1f);
        if (!isHoveringClickableImage && clickableImage != null && currentDay < maxDays)
        {
            SetClickableImageDark();
        }
    }

    public int GetCurrentDay() => currentDay;

    // 检查是否已达到最大天数
    public bool IsMaxDayReached() => currentDay >= maxDays;

    private void OnValidate()
    {
        maxDays = Mathf.Max(2, maxDays);
        colorChangeDuration = Mathf.Max(0f, colorChangeDuration);
        clickableImageBrightness = Mathf.Clamp(clickableImageBrightness, 1f, 2f);
        clickableImageDarkness = Mathf.Clamp(clickableImageDarkness, 0.5f, 1f);
    }
}
