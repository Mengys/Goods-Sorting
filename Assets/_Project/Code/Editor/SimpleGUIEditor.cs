using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Project.Code.Editor
{
    public class SimpleGUIEditor : EditorWindow
    {
        // References to UI elements
        private VisualElement _leftArea;
        private VisualElement _middleArea;
        private VisualElement _rightArea;
        private VisualElement _bottomPanel;
        private Button _button1;
        private Button _button2;
        private Button _button3;

        [MenuItem("Window/Simple GUI Editor")]
        public static void ShowWindow()
        {
            // Get existing open window or create a new one
            SimpleGUIEditor window = GetWindow<SimpleGUIEditor>("Simple GUI Editor");
            window.minSize = new Vector2(600, 400); // Set minimum window size
        }

        private void CreateGUI()
        {
            // Get the root element
            var root = rootVisualElement;
            
            // Create main container
            var mainContainer = new VisualElement();
            mainContainer.style.flexGrow = 1;
            mainContainer.style.flexDirection = FlexDirection.Column;
            root.Add(mainContainer);
            
            // Create areas container (for the 3 vertical areas)
            var areasContainer = new VisualElement();
            areasContainer.style.flexGrow = 1;
            areasContainer.style.flexDirection = FlexDirection.Row;
            mainContainer.Add(areasContainer);
            
            // Create the three vertical areas
            _leftArea = CreateArea("Left Area", Color.gray);
            _middleArea = CreateArea("Middle Area", Color.gray);
            _rightArea = CreateArea("Right Area", Color.gray);
            
            // Add the areas to the container
            areasContainer.Add(_leftArea);
            areasContainer.Add(_middleArea);
            areasContainer.Add(_rightArea);
            
            // Create bottom panel
            _bottomPanel = new VisualElement();
            _bottomPanel.style.height = 40;
            _bottomPanel.style.flexDirection = FlexDirection.Row;
            _bottomPanel.style.justifyContent = Justify.SpaceAround;
            _bottomPanel.style.alignItems = Align.Center;
            _bottomPanel.style.backgroundColor = new Color(0.2f, 0.2f, 0.2f);
            _bottomPanel.style.paddingLeft = 10;
            _bottomPanel.style.paddingRight = 10;
            mainContainer.Add(_bottomPanel);
            
            // Create the three buttons
            _button1 = CreateButton("Button 1", OnButton1Clicked);
            _button2 = CreateButton("Button 2", OnButton2Clicked);
            _button3 = CreateButton("Button 3", OnButton3Clicked);
            
            // Add the buttons to the bottom panel
            _bottomPanel.Add(_button1);
            _bottomPanel.Add(_button2);
            _bottomPanel.Add(_button3);
        }

        private VisualElement CreateArea(string title, Color backgroundColor)
        {
            var area = new VisualElement();
            area.style.flexGrow = 1;
            area.style.marginLeft = 5;
            area.style.marginRight = 5;
            area.style.marginTop = 5;
            area.style.marginBottom = 5;
            area.style.backgroundColor = backgroundColor;
            area.style.borderTopLeftRadius = 5;
            area.style.borderTopRightRadius = 5;
            area.style.borderBottomLeftRadius = 5;
            area.style.borderBottomRightRadius = 5;
            
            // Add a title label
            var titleLabel = new Label(title);
            titleLabel.style.unityTextAlign = TextAnchor.MiddleCenter;
            titleLabel.style.fontSize = 14;
            titleLabel.style.marginTop = 10;
            area.Add(titleLabel);
            
            // Add a content container
            var content = new VisualElement();
            content.style.flexGrow = 1;
            content.style.marginLeft = 10;
            content.style.marginRight = 10;
            content.style.marginTop = 10;
            content.style.marginBottom = 10;
            area.Add(content);
            
            return area;
        }

        private Button CreateButton(string text, System.Action clickHandler)
        {
            var button = new Button(clickHandler);
            button.text = text;
            button.style.width = 120;
            button.style.height = 30;
            return button;
        }

        private void OnButton1Clicked()
        {
            Debug.Log("Button 1 clicked");
            // Add your button 1 functionality here
        }

        private void OnButton2Clicked()
        {
            Debug.Log("Button 2 clicked");
            // Add your button 2 functionality here
        }

        private void OnButton3Clicked()
        {
            Debug.Log("Button 3 clicked");
            // Add your button 3 functionality here
        }
    }
}
