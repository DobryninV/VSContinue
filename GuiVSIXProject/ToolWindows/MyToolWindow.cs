using System.Windows;
using Microsoft.Web.WebView2.Wpf;
using System.Windows.Controls;
using System.Runtime.InteropServices;

namespace GuiVSIXProject
{
    [Guid("12345678-1234-1234-1234-123456789012")]
    public class MyToolWindow : ToolWindowPane
    {
        private WebView2 _webView;
        private Grid _grid;
        public MyToolWindow() : base(null)
        {
            this.Caption = "Моё WebView2 окно";

            InitializeComponent();
            InitializeWebViewAsync();
        }
        private void InitializeComponent()
        {
            _grid = new Grid();
            _webView = new WebView2
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Visibility = Visibility.Hidden
            };

            _grid.Children.Add(_webView);
            this.Content = _grid;
        }

        private async void InitializeWebViewAsync()
        {
            try
            {
                await _webView.EnsureCoreWebView2Async();
                _webView.Visibility = Visibility.Visible;

                string htmlContent = @"
<!DOCTYPE html>
<html>
<head>
    <style>
        body { 
            font-family: 'Segoe UI', Arial; 
            margin: 20px;
            background: #f5f5f5;
        }
        .container {
            background: white;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }
    </style>
</head>
<body>
    <div class='container'>
        <h1>Мой UI в Visual Studio!</h1>
        <p>Это HTML интерфейс в расширении VS 2022</p>
        <button onclick='showMessage()'>Нажми меня</button>
    </div>
    <script>
        function showMessage() {
            alert('Hello from WebView2!');
        }
    </script>
</body>
</html>";

                _webView.CoreWebView2.NavigateToString(htmlContent);
            }
            catch (Exception ex)
            {
                // Fallback - простой текст если WebView2 не работает
                var errorText = new TextBlock
                {
                    Text = $"WebView2 error: {ex.Message}\n\nUsing fallback UI",
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(10)
                };

                _grid.Children.Add(errorText);
            }
        }
    }
}