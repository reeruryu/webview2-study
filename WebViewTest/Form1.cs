using Microsoft.Web.WebView2.Core;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace WebViewTest
{

    public partial class Form1 : Form
    {

        Bridge bridge;

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
            webView21.CoreWebView2.Navigate("http://localhost:8083/webview");
            //webView21.CoreWebView2.Navigate(htmlPath);

            // WebView2 이벤트 핸들러 설정
            webView21.CoreWebView2.WebMessageReceived += WebView_WebMessageReceived;
            webView21.NavigationCompleted += WebView21_NavigationCompleted;
        }

        private void WebView21_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            // C# 객체를 JavaScript에 노출
            bridge = new Bridge();
            webView21.CoreWebView2.AddHostObjectToScript("bridge", bridge);

        }

        private void WebView_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            // WebView에서 C#으로 전송된 메시지를 처리합니다.
            string message = e.TryGetWebMessageAsString();
            label2.Text = message;
            bridge.Message = message;

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            // C#에서 WebView로 메시지를 전송합니다.
            webView21.CoreWebView2.PostWebMessageAsString(textBox1.Text);

            // 확인을 위해 메시지 박스를 표시
            MessageBox.Show("Message sent to WebView!");
        }

        private async void button2_Click(object sender, System.EventArgs e)
        {
            await webView21.CoreWebView2.ExecuteScriptAsync("sendMessage()");
        }

    }

    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class Bridge
    {

        // property
        public string Message { get; set; } = "메시지가 아직 없습니다.";

        // method
        public string ReturnMessage()
        {
            return Message;
        }


    }

}
