using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UTK
{
    public class SongView : BaseUTKView
    {
        private ListView _songListView;
        private Button _backButton;

        public Button BackButton { get => _backButton;}

        private void Awake()
        {
            _rootElement = _uiDocument.rootVisualElement;
            _songListView = _rootElement.Query<ListView>("songList");
            _backButton = _rootElement.Query<Button>("BackButton");
        }

        public void CreateListView(List<SongData> data,Action<VisualElement> action)
        {
            _songListView.itemsSource = data;

            _songListView.makeItem = () =>
            {
                var element = new SongObjectItem();
                //element.style.height = 80;
                element.RegisterCallback<ClickEvent>((evt) => UIToolkitManager.Instance.OnClickSong(element));
                return element;
            };
            _songListView.bindItem = (element, i) =>
            {
                var item = (SongObjectItem)element;
                element.userData = data[i];
                item.SetButtonName(data[i]);
                //item.songButton.clicked += () => action(element);
            };
            _songListView.fixedItemHeight = 100;
            var scrollView = _songListView.Q<ScrollView>();
            // スクロールバーの幅を設定
            var verticalScroller = scrollView.Query<Scroller>().First();
            if (verticalScroller != null)
            {
                verticalScroller.style.width = 50; // 幅を設定
                var lowButton = verticalScroller.Q("unity-low-button");
                if (lowButton != null)
                {
                    lowButton.style.width = 50; // 幅を設定
                }
                var highButton = verticalScroller.Q("unity-high-button");
                if (highButton != null)
                {
                    highButton.style.width = 50; // 幅を設定
                }
            }

            var verticalSlider = verticalScroller.Q("unity-slider");

            if (verticalSlider != null)
            {
                verticalSlider.style.width = 50; // スライダーの幅を設定
                var dragger = verticalSlider.Q("unity-dragger");
                if (dragger != null)
                {
                    dragger.style.width = 50; // スライダーの幅を設定
                }
            }

            //_songListView.style.height = 300;
            VisualElement content = _songListView.Query<VisualElement>("unity-content-container");
            content.style.flexGrow = 1;
        }

    }
}