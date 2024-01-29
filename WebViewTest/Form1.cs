using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;

namespace WebViewTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeWebView();
        }

        private async void InitializeWebView()
        {
            // WebView2 초기화
            await webView21.EnsureCoreWebView2Async(null);

            // 로컬 HTML 파일 로드
            string htmlPath = @"C:\Users\User\Desktop\rhj\customurl\demo\src\main\resources\templates\webview.html";
            webView21.CoreWebView2.Navigate(htmlPath);

            // WebView2 이벤트 핸들러 설정
            webView21.CoreWebView2.WebMessageReceived += WebView_WebMessageReceived;
        }

        private void WebView_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            // WebView에서 C#으로 전송된 메시지를 처리합니다.
            string message = e.TryGetWebMessageAsString();
            MessageBox.Show($"Message from WebView: {message}");

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            // C#에서 WebView로 메시지를 전송합니다.
            webView21.CoreWebView2.PostWebMessageAsString(textBox1.Text);

            // 확인을 위해 메시지 박스를 표시
            MessageBox.Show("Message sent to WebView!");
        }

        /*private async void button1_Click(object sender, EventArgs e)
        {
            await webView21.CoreWebView2.ExecuteScriptAsync("WebSocketRequest_GetReturn()");
        }*/
    }
}
