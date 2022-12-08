using QFramework;
using UnityEngine;
using UnityEngine.UIElements;

namespace BrotatoM
{
    public class ShopScreenUI : BrotatoGameController
    {
        private IPlayerSystem mPlayerSystem;
        private ItemConfigItem[] mBuyableItems;
        private VisualElement mRootElement;
        private VisualElement mItemsContainer;
        private VisualElement mAttrPanel;
        private Label mWaveLabel;
        private Label mHarvestLabel;

        private void Awake()
        {
            mPlayerSystem = this.GetSystem<IPlayerSystem>();
            mBuyableItems = this.GetModel<ItemConfigModel>().GetAllBuyableItems();
        }
        private void Start()
        {
            mRootElement = GetComponent<UIDocument>().rootVisualElement;
            mWaveLabel = mRootElement.Q<Label>("wave-text");
            mHarvestLabel = mRootElement.Q<Label>("harvest-text");

            mHarvestLabel.text = ((int)mPlayerSystem.Harvest.Value).ToString();

            // 按钮hover => 可以找到当前创建了的模板上的button，但如果模板重新创建了就管不到了。
            // 最好放在模板创建之前，否则会多次注册事件。
            mRootElement.Query<Button>().ForEach(btn =>
            {
                var rawBackgroundColor = btn.style.backgroundColor;
                var rawColor = btn.style.color;
                // :hover 的替代方案。 鼠标移动上去变成白底黑字，离开则恢复为黑底白字。
                btn.RegisterCallback<MouseOverEvent>((type) =>
                {
                    btn.style.backgroundColor = new Color(1f, 1f, 1f, 0.8f);
                    btn.style.color = new Color(0, 0, 0, 0.8f);
                });

                btn.RegisterCallback<MouseLeaveEvent>((type) =>
                {
                    btn.style.backgroundColor = rawBackgroundColor;
                    btn.style.color = rawColor;
                });
            });

            // 生成4个物品
            ShowRandomItems();

            // 属性显示
            mAttrPanel = mRootElement.Q("attrs");
            mAttrPanel.Clear();
            var mNeedShowProperties = this.SendQuery(new NeedShowPropertiesQuery());
            AttrRow attrRow;
            for (int i = 0; i < mNeedShowProperties.Length; i++)
            {
                attrRow = new AttrRow(mNeedShowProperties[i]);
                attrRow.style.flexBasis = Length.Percent(6.5f);
                mAttrPanel.Add(attrRow);
            }

            // 刷新按钮
            var refresh_btn = mRootElement.Q<Button>("refresh-btn");
            refresh_btn.RegisterCallback<ClickEvent>(e =>
            {
                ShowRandomItems();
            });

            this.RegisterEvent<WaveOverEvent>(e =>
            {
                mWaveLabel.text = $"商店(第{mPlayerSystem.CurrWave}波)";
            });
        }

        private void ShowRandomItems()
        {
            mItemsContainer = mRootElement.Q("item-row");
            mItemsContainer.Clear();
            mItemsContainer.style.flexDirection = FlexDirection.Row;
            if (mPlayerSystem.CurrLockIndices.Count == 0)
            {
                var items = mBuyableItems.GetRandomElements(4);
                ItemPanel itemPanel;
                for (int i = 0; i < 4; i++)
                {
                    itemPanel = new ItemPanel(items[i]);
                    itemPanel.style.flexBasis = Length.Percent(25);
                    mItemsContainer.Add(itemPanel);
                }
            }
            else
            {
                var items = mBuyableItems.GetRandomElements(4);
                ItemPanel itemPanel;
                for (int i = 0; i < 4; i++)
                {
                    if (i < mPlayerSystem.CurrLockIndices.Count)
                    {
                        items[i] = this.GetModel<ItemConfigModel>().GetConfigItemById(mPlayerSystem.CurrLockIndices[i]);
                        itemPanel = new ItemPanel(items[i], true);
                    }
                    else
                    {
                        itemPanel = new ItemPanel(items[i]);
                    }
                    itemPanel.style.flexBasis = Length.Percent(25);
                    mItemsContainer.Add(itemPanel);
                }
            }
        }
    }
}
