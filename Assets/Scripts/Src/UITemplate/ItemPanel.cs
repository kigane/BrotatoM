using UnityEngine;
using UnityEngine.UIElements;
using QFramework;

namespace BrotatoM
{
    public class ItemPanel : VisualElement, IController
    {
        // 模板外套的根元素
        private readonly TemplateContainer mTemplateContainer;

        // 让自定义控件可以显示到UI Builder中
        public new class UxmlFactory : UxmlFactory<ItemPanel> { }
        private EventCallback<MouseOverEvent> OnHover;
        private EventCallback<MouseLeaveEvent> OnLeave;

        // 提供无参构造函数
        public ItemPanel()
        {
            mTemplateContainer = Resources.Load<VisualTreeAsset>("UI/Template/item-panel").Instantiate();
            mTemplateContainer.style.flexGrow = 1;

            hierarchy.Add(mTemplateContainer);
        }

        // 有参构造函数
        public ItemPanel(ItemConfigItem item, bool locked = false) : this()
        {
            var playerSystem = this.GetSystem<IPlayerSystem>();
            mTemplateContainer.Q("item-icon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>(item.Path));
            mTemplateContainer.Q<Label>("item-name").text = item.Name;
            mTemplateContainer.Q<Label>("item-tag").text = "道具";
            mTemplateContainer.Q<Label>("item-description").text = item.Effects;

            mTemplateContainer.Q("buy-btn-container").RegisterCallback<ClickEvent>(e =>
            {
                if (playerSystem.Harvest.Value < item.BasePrice)
                {
                    Log.Debug("买不起!");
                }
                else
                {
                    Log.Debug($"添加 道具 {item.Id} {item.Name}");
                    // 添加道具
                    playerSystem.AddItem(item.Id);
                    playerSystem.Harvest.Value -= item.BasePrice;

                    // 清除Panel
                    mTemplateContainer.Q("item-panel").Clear();
                }
            });

            // 图片的PickMode要设为Ignore
            var buyBtn = mTemplateContainer.Q<Button>("buy-btn");
            AddHoverHandler(buyBtn);
            buyBtn.text = item.BasePrice.ToString();

            var lockBtn = mTemplateContainer.Q<Button>("lock-btn");

            lockBtn.clickable.clicked += () =>
            {
                if (!playerSystem.CurrLockIndices.Contains(item.Id))
                {
                    Log.Debug($"锁定 道具 {item.Id} {item.Name}");
                    playerSystem.CurrLockIndices.Add(item.Id);
                    lockBtn.style.backgroundColor = UIColor.LOCKED;

                    lockBtn.UnregisterCallback(OnHover);
                    lockBtn.UnregisterCallback(OnLeave);
                }
                else
                {
                    Log.Debug($"解锁 道具 {item.Id} {item.Name}");
                    playerSystem.CurrLockIndices.Remove(item.Id);
                    lockBtn.style.backgroundColor = UIColor.GRAY;

                    OnHover = e => { lockBtn.style.backgroundColor = UIColor.WHITE; };
                    lockBtn.RegisterCallback(OnHover);
                    OnLeave = e => { lockBtn.style.backgroundColor = UIColor.BLACK; };
                    lockBtn.RegisterCallback(OnLeave);
                }
            };

            if (locked)
            {
                lockBtn.style.backgroundColor = UIColor.LOCKED;
            }
            else
            {
                AddHoverHandler(lockBtn);
            }
        }

        private void AddHoverHandler(VisualElement btn)
        {
            var rawColor = btn.style.backgroundColor;
            OnHover = e => { btn.style.backgroundColor = UIColor.WHITE; };
            btn.RegisterCallback(OnHover);

            OnLeave = e => { btn.style.backgroundColor = rawColor; };
            btn.RegisterCallback(OnLeave);
        }

        public IArchitecture GetArchitecture()
        {
            return BrotatoGame.Interface;
        }
    }
}
