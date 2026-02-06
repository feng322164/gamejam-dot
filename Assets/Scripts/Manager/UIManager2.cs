using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class DayCounter : MonoBehaviour
{
    [Header("Main UI References")]
    [SerializeField] private Image clickableImage;
    [SerializeField] private Image closeButtonImage;
    [SerializeField] private Image additionalImage;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI additionalText;

    [Header("Hidden UI References - Images")]
    [SerializeField] private Image hiddenImage1; // 不可交互
    [SerializeField] private Image hiddenImage2; // 点击后隐藏隐藏UI，恢复交互
    [SerializeField] private Image hiddenImage3; // 点击后进入另一个场景

    [Header("Hidden UI References - Texts")]
    [SerializeField] private TextMeshProUGUI hiddenText1; // 不可交互
    [SerializeField] private TextMeshProUGUI hiddenText2; // 点击后隐藏隐藏UI，恢复交互
    [SerializeField] private TextMeshProUGUI hiddenText3; // 点击后进入另一个场景

    [Header("Scene Settings")]
    [SerializeField] private string targetSceneName = "NextScene";

    [Header("Hover Settings")]
    [SerializeField] private float hoverBrightness = 1.3f; // 所有UI的悬停变亮倍数
    [SerializeField] private float normalDarkness = 0.8f;   // 所有UI的正常变暗倍数
    [SerializeField] private float colorChangeDuration = 0.2f;

    [Header("Other Settings")]
    [SerializeField] private int maxDays = 6;
    [SerializeField] private string textPrefix = "Day ";

    private int currentDay = 1;
    private Button imageButton;
    private Button closeButton;
    private Button additionalTextButton;

    // 状态记录
    private bool wasClickableImageDisabledBeforeClose = false;
    private bool areHiddenUIsVisible = false;

    // 存储所有UI的原始颜色和组件引用
    private Dictionary<Graphic, Color> originalColors = new Dictionary<Graphic, Color>();
    private Dictionary<Graphic, bool> isHovering = new Dictionary<Graphic, bool>();
    private Dictionary<Graphic, Button> uiButtons = new Dictionary<Graphic, Button>();

    void Start()
    {
        InitializeComponents();
        UpdateDayText();

        // 存储原始颜色并设置初始变暗状态
        StoreOriginalColors();
        SetAllUIsDark();

        // 开始时隐藏隐藏的UI元素
        SetHiddenUIsVisible(false);
    }

    private void InitializeComponents()
    {
        InitializeMainUIComponents();
        InitializeHiddenUIComponents();
    }

    private void InitializeMainUIComponents()
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

            AddHoverEventsToUI(clickableImage, OnPointerEnterUI, OnPointerExitUI);
            uiButtons[clickableImage] = imageButton;
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

            AddHoverEventsToUI(closeButtonImage, OnPointerEnterUI, OnPointerExitUI);
            uiButtons[closeButtonImage] = closeButton;
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

            AddHoverEventsToUI(additionalText, OnPointerEnterUI, OnPointerExitUI);
            uiButtons[additionalText] = additionalTextButton;
        }
    }

    private void InitializeHiddenUIComponents()
    {
        // 初始化隐藏的UI Images
        InitializeHiddenImage(hiddenImage1, false, null); // 不可交互
        InitializeHiddenImage(hiddenImage2, true, OnHiddenImage2Clicked); // 隐藏隐藏UI，恢复交互
        InitializeHiddenImage(hiddenImage3, true, OnHiddenImage3Clicked); // 进入另一个场景

        // 初始化隐藏的UI Texts
        InitializeHiddenText(hiddenText1, false, null); // 不可交互
        InitializeHiddenText(hiddenText2, true, OnHiddenText2Clicked); // 隐藏隐藏UI，恢复交互
        InitializeHiddenText(hiddenText3, true, OnHiddenText3Clicked); // 进入另一个场景
    }

    private void InitializeHiddenImage(Image image, bool interactable, UnityEngine.Events.UnityAction clickAction)
    {
        if (image != null)
        {
            Button button = image.GetComponent<Button>();
            if (button == null && interactable)
            {
                button = image.gameObject.AddComponent<Button>();
            }

            if (button != null)
            {
                button.interactable = interactable;
                button.onClick.RemoveAllListeners();
                if (clickAction != null)
                {
                    button.onClick.AddListener(clickAction);
                }

                // 为可交互的隐藏UI添加悬停事件
                if (interactable)
                {
                    AddHoverEventsToUI(image, OnPointerEnterUI, OnPointerExitUI);
                    uiButtons[image] = button;
                }
            }
        }
    }

    private void InitializeHiddenText(TextMeshProUGUI text, bool interactable, UnityEngine.Events.UnityAction clickAction)
    {
        if (text != null)
        {
            Button button = text.GetComponent<Button>();
            if (button == null && interactable)
            {
                button = text.gameObject.AddComponent<Button>();
            }

            if (button != null)
            {
                button.interactable = interactable;
                button.onClick.RemoveAllListeners();
                if (clickAction != null)
                {
                    button.onClick.AddListener(clickAction);
                }

                // 为可交互的隐藏UI添加悬停事件
                if (interactable)
                {
                    AddHoverEventsToUI(text, OnPointerEnterUI, OnPointerExitUI);
                    uiButtons[text] = button;
                }
            }
        }
    }

    // 存储所有UI的原始颜色
    private void StoreOriginalColors()
    {
        StoreColorIfExists(clickableImage);
        StoreColorIfExists(closeButtonImage);
        StoreColorIfExists(additionalImage);
        StoreColorIfExists(dayText);
        StoreColorIfExists(additionalText);
        StoreColorIfExists(hiddenImage1);
        StoreColorIfExists(hiddenImage2);
        StoreColorIfExists(hiddenImage3);
        StoreColorIfExists(hiddenText1);
        StoreColorIfExists(hiddenText2);
        StoreColorIfExists(hiddenText3);
    }

    private void StoreColorIfExists(Graphic uiElement)
    {
        if (uiElement != null)
        {
            originalColors[uiElement] = uiElement.color;
            isHovering[uiElement] = false;
        }
    }

    // 为UI添加悬停事件
    private void AddHoverEventsToUI(Graphic uiElement, UnityEngine.Events.UnityAction<PointerEventData> enterAction,
                                  UnityEngine.Events.UnityAction<PointerEventData> exitAction)
    {
        EventTrigger eventTrigger = uiElement.GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = uiElement.gameObject.AddComponent<EventTrigger>();
        }

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

    // 检查UI是否可交互
    private bool IsUIInteractable(Graphic uiElement)
    {
        if (uiElement == null) return false;

        // 检查是否有Button组件并且可交互
        if (uiButtons.ContainsKey(uiElement))
        {
            return uiButtons[uiElement].interactable;
        }

        return false;
    }

    // 鼠标进入UI时的统一处理
    private void OnPointerEnterUI(PointerEventData eventData)
    {
        Graphic hoveredUI = eventData.pointerEnter.GetComponent<Graphic>();
        if (hoveredUI != null && originalColors.ContainsKey(hoveredUI))
        {
            isHovering[hoveredUI] = true;

            // 只有在可交互状态时才变亮
            if (IsUIInteractable(hoveredUI))
            {
                SetUIBright(hoveredUI);
            }
        }
    }

    // 鼠标离开UI时的统一处理
    private void OnPointerExitUI(PointerEventData eventData)
    {
        Graphic exitedUI = eventData.pointerEnter.GetComponent<Graphic>();
        if (exitedUI != null && originalColors.ContainsKey(exitedUI))
        {
            isHovering[exitedUI] = false;
            SetUIDark(exitedUI);
        }
    }

    // 设置UI变亮
    private void SetUIBright(Graphic uiElement)
    {
        if (uiElement != null && uiElement.gameObject.activeInHierarchy && originalColors.ContainsKey(uiElement))
        {
            Color brightColor = GetModifiedColor(originalColors[uiElement], hoverBrightness);
            uiElement.CrossFadeColor(brightColor, colorChangeDuration, true, true);
        }
    }

    // 设置UI变暗
    private void SetUIDark(Graphic uiElement)
    {
        if (uiElement != null && uiElement.gameObject.activeInHierarchy && originalColors.ContainsKey(uiElement))
        {
            Color darkColor = GetModifiedColor(originalColors[uiElement], normalDarkness);
            uiElement.CrossFadeColor(darkColor, colorChangeDuration, true, true);
        }
    }

    // 设置所有UI变暗
    private void SetAllUIsDark()
    {
        foreach (var uiElement in originalColors.Keys)
        {
            if (uiElement != null)
            {
                SetUIDark(uiElement);
            }
        }
    }

    // 获取修改后的颜色
    private Color GetModifiedColor(Color baseColor, float multiplier)
    {
        return new Color(
            Mathf.Clamp01(baseColor.r * multiplier),
            Mathf.Clamp01(baseColor.g * multiplier),
            Mathf.Clamp01(baseColor.b * multiplier),
            baseColor.a
        );
    }

    // 点击关闭按钮的处理
    private void OnCloseButtonClicked()
    {
        // 记录clickable image的当前状态
        wasClickableImageDisabledBeforeClose = (imageButton != null && !imageButton.interactable);

        // 禁用主UI的交互
        SetMainUIInteractable(false);

        // 显示隐藏的UI
        SetHiddenUIsVisible(true);

        areHiddenUIsVisible = true;
    }

    // 隐藏UI Image 2的点击事件
    private void OnHiddenImage2Clicked()
    {
        HideHiddenUIsAndRestore();
    }

    // 隐藏UI Image 3的点击事件
    private void OnHiddenImage3Clicked()
    {
        LoadTargetScene();
    }

    // 隐藏UI Text 2的点击事件
    private void OnHiddenText2Clicked()
    {
        HideHiddenUIsAndRestore();
    }

    // 隐藏UI Text 3的点击事件
    private void OnHiddenText3Clicked()
    {
        LoadTargetScene();
    }

    // 隐藏隐藏的UI并恢复主UI交互
    private void HideHiddenUIsAndRestore()
    {
        SetHiddenUIsVisible(false);
        areHiddenUIsVisible = false;

        // 恢复主UI交互
        SetMainUIInteractable(true, !wasClickableImageDisabledBeforeClose);
    }

    // 加载目标场景
    private void LoadTargetScene()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogError("目标场景名称为空！");
        }
    }

    // 设置隐藏UI的显示状态
    private void SetHiddenUIsVisible(bool visible)
    {
        SetUIVisibility(hiddenImage1, visible);
        SetUIVisibility(hiddenImage2, visible);
        SetUIVisibility(hiddenImage3, visible);
        SetUIVisibility(hiddenText1, visible);
        SetUIVisibility(hiddenText2, visible);
        SetUIVisibility(hiddenText3, visible);

        // 显示时设置正确的颜色状态
        if (visible)
        {
            foreach (var uiElement in new Graphic[] { hiddenImage1, hiddenImage2, hiddenImage3, hiddenText1, hiddenText2, hiddenText3 })
            {
                if (uiElement != null)
                {
                    SetUIDark(uiElement);
                }
            }
        }
    }

    // 设置主UI的交互状态
    private void SetMainUIInteractable(bool interactable, bool restoreClickableImage = true)
    {
        if (closeButton != null) closeButton.interactable = interactable;
        if (additionalTextButton != null) additionalTextButton.interactable = interactable;

        // 特殊处理clickable image
        if (imageButton != null)
        {
            if (interactable && (!restoreClickableImage || wasClickableImageDisabledBeforeClose))
            {
                imageButton.interactable = false;
            }
            else
            {
                imageButton.interactable = interactable;
            }
        }

        // 设置透明度表示交互状态
        float alpha = interactable ? 1f : 0.5f;
        SetMainUIsAlpha(alpha);

        // 如果UI变为不可交互状态，确保它们不会保持变亮状态
        if (!interactable)
        {
            foreach (var uiElement in new Graphic[] { clickableImage, closeButtonImage, additionalText })
            {
                if (uiElement != null && isHovering.ContainsKey(uiElement) && isHovering[uiElement])
                {
                    SetUIDark(uiElement);
                    isHovering[uiElement] = false;
                }
            }
        }
    }

    // 设置主UI的透明度
    private void SetMainUIsAlpha(float alpha)
    {
        SetUIAlpha(clickableImage, alpha);
        SetUIAlpha(closeButtonImage, alpha);
        SetUIAlpha(additionalImage, alpha);
        SetUIAlpha(dayText, alpha);
        SetUIAlpha(additionalText, alpha);
    }

    private void SetUIAlpha(Graphic uiElement, float alpha)
    {
        if (uiElement != null)
        {
            Color color = uiElement.color;
            color.a = alpha;
            uiElement.color = color;
        }
    }

    private void OnImageClicked()
    {
        if (currentDay >= maxDays || areHiddenUIsVisible) return;

        currentDay++;
        UpdateDayText();

        if (currentDay >= maxDays)
        {
            DisableImageInteraction();
        }
    }

    private void DisableImageInteraction()
    {
        if (imageButton != null)
        {
            imageButton.interactable = false;

            // 如果正在悬停，立即变暗
            if (isHovering.ContainsKey(clickableImage) && isHovering[clickableImage])
            {
                SetUIDark(clickableImage);
                isHovering[clickableImage] = false;
            }
        }

        // 设置禁用状态的外观
        Color disabledColor = GetModifiedColor(originalColors[clickableImage], normalDarkness);
        disabledColor.a = 0.5f;
        if (clickableImage != null)
        {
            clickableImage.color = disabledColor;
        }
    }

    public void ShowAllUIElements()
    {
        SetUIVisibility(clickableImage, true);
        SetUIVisibility(closeButtonImage, true);
        SetUIVisibility(additionalImage, true);
        SetUIVisibility(dayText, true);
        SetUIVisibility(additionalText, true);

        SetMainUIInteractable(true);
        SetAllUIsDark();
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
        SetHiddenUIsVisible(false);
        areHiddenUIsVisible = false;
        wasClickableImageDisabledBeforeClose = false;

        // 重置悬停状态
        foreach (var key in new List<Graphic>(isHovering.Keys))
        {
            isHovering[key] = false;
        }
    }

    // 公共方法
    public void SetTargetScene(string sceneName)
    {
        targetSceneName = sceneName;
    }

    public void SetHoverBrightness(float brightness)
    {
        hoverBrightness = Mathf.Clamp(brightness, 1f, 2f);
        // 更新所有正在悬停且可交互的UI
        foreach (var kvp in isHovering)
        {
            if (kvp.Value && kvp.Key != null && IsUIInteractable(kvp.Key))
            {
                SetUIBright(kvp.Key);
            }
        }
    }

    public void SetNormalDarkness(float darkness)
    {
        normalDarkness = Mathf.Clamp(darkness, 0.5f, 1f);
        // 更新所有UI
        SetAllUIsDark();
    }

    public bool AreHiddenUIsVisible() => areHiddenUIsVisible;

    private void OnValidate()
    {
        maxDays = Mathf.Max(2, maxDays);
        colorChangeDuration = Mathf.Max(0f, colorChangeDuration);
        hoverBrightness = Mathf.Clamp(hoverBrightness, 1f, 2f);
        normalDarkness = Mathf.Clamp(normalDarkness, 0.5f, 1f);
    }
}