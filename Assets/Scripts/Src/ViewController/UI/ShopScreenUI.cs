using QFramework;
using UnityEngine;
using UnityEngine.UIElements;

namespace BrotatoM
{
    public class ShopScreenUI : BaseUI
    {
        private IPlayerSystem mPlayerSystem;
        private ItemConfigItem[] mBuyableItems;
        private VisualElement mItemsContainer;
        private Label mWaveLabel;
        private Label mHarvestLabel;
        private Button mRefreshBtn;

        private void Awake()
        {
            mPlayerSystem = this.GetSystem<IPlayerSystem>();
            mBuyableItems = this.GetModel<ItemConfigModel>().GetAllBuyableItems();
        }

        protected override void OnUIEnable()
        {
            // mRootElement = GetComponent<UIDocument>().rootVisualElement;// 不能放到awake里面
            mWaveLabel = mRootElement.Q<Label>("wave-text");
            mHarvestLabel = mRootElement.Q<Label>("harvest-text");
            mRefreshBtn = mRootElement.Q<Button>("refresh-btn");

            // 按钮hover => 可以找到当前创建了的模板上的button，但如果模板重新创建了就管不到了。
            // 所以最好放在模板创建之前，否则会多次注册事件。
            RegisterBtnHoverBehaviour();

            // 生成4个物品
            ShowRandomItems();

            // 属性显示
            ShowPlayerProperties();

            // 刷新按钮
            var refreshBtn = mRootElement.Q<Button>("refresh-btn");
            refreshBtn.RegisterCallback<ClickEvent>(e =>
            {
                if (mPlayerSystem.Harvest.Value < mPlayerSystem.RefreshCost.Value)
                {
                    Log.Debug("刷新不起!");
                }
                else
                {
                    // 显示新物品
                    ShowRandomItems();
                    // 扣钱
                    mPlayerSystem.Harvest.Value -= mPlayerSystem.RefreshCost.Value;
                    // 增加下次刷新的钱数并显示
                    mPlayerSystem.RefreshCost.Value++;
                    mRefreshBtn.text = "-" + mPlayerSystem.RefreshCost.Value.ToString();
                }
            });

            // 出发按钮
            var nextWaveBtn = mRootElement.Q<Button>("next-wave-btn");
            nextWaveBtn.RegisterCallback<ClickEvent>(e =>
            {
                if (mPlayerSystem.CurrWave < 20)
                    this.SendCommand<NextWaveCommand>();
            });

            this.RegisterEvent<WaveOverEvent>(e =>
            {
                mWaveLabel.text = $"商店(第{mPlayerSystem.CurrWave}波)";
            });

            mPlayerSystem.Harvest.RegisterWithInitValue(value =>
            {
                mHarvestLabel.text = ((int)value).ToString();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void ShowRandomItems()
        {
            mItemsContainer = mRootElement.Q("item-row");
            mItemsContainer.Clear();
            mItemsContainer.style.flexDirection = FlexDirection.Row;
            if (mPlayerSystem.CurrLockIndices.Count == 0)
            { // 无锁定物品
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
            { // 有锁定物品
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
