using KM.Features.Resources;
using KM.Startup;
using KM.UI.Components;
using ScreenSystem.Components;
using ScreenSystem.Screens;

namespace KM.UI
{
    public class ResourcesScreen : BaseScreen
    {
        public ListComponent resourcesList;

        private ResourcesSystem _resourceSystem;

        protected override void OnShow()
        {
            _resourceSystem = AppStartup.Instance.GetSystem<ResourcesSystem>();

            _resourceSystem.ResourcesUpdated += ResourcesChanged;

            UpdateResourcesList();
        }

        protected override void OnHide()
        {
            _resourceSystem.ResourcesUpdated -= ResourcesChanged;
        }

        private void ResourcesChanged(ResourceStorage change)
        {
            UpdateResourcesList();

            for(int i = 0; i < resourcesList.items.Count; i++)
            {
                if (change.resources[i] == 0)
                    continue;

                var item = resourcesList.GetItem<ResourceItemComponent>(i);

                if (item == null)
                    continue;

                StartCoroutine(item.FadeOut(change.resources[i]));
            }
        }

        private void UpdateResourcesList()
        {
            resourcesList.SetItems<ResourceItemComponent>(_resourceSystem.Resources.resources.Length, (item, param) =>
            {
                var name = ((ResourceType)param.index).ToString();
                item.SetInfo(name, _resourceSystem.Resources.resources[param.index]);
            });
        }

        protected override void OnInit()
        {
        }
    }
}
