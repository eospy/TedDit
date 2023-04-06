using System.IO;
using System;
using System.Net;
using System.Text;
using RedditCLient.MVVM.ViewModel;

namespace RedditCLient.API
{
    public class LocalServer
    {
        private HttpListener server;
        public TokenRetrieval codestate;
        public string state = "";
        public string code = "";
        public bool success = false;
        public LocalServer()
        {
            server = new HttpListener();
            codestate = new();
            // установка адресов прослушки
            server.Prefixes.Add("http://127.0.0.1:8080/auth/");
        }
        public void Stop() => server.Stop();
        public string Start(string requeststate)
        {
            server.Start();// начинаем прослушивать входящие подключения
            var context =  server.GetContext();
            var req = context.Request;
            if (req.QueryString["error"] == null)
            {
                state = req.QueryString["state"];
                code = req.QueryString["code"];
            }
            string responseText =
              @"<!DOCTYPE html>
                <html>
                    <head>
                        <meta charset='utf8'>
                        <title>Teddit</title>
                    </head>
                    <body>
                        <h2>App successfully authorized</h2>
                        <h2>You can close this window</h2>
                    </body>
                </html>";
            //проверка на обоснованность выполнения запроса
            if (state != requeststate)
            {
                responseText =
                @"<!DOCTYPE html>
                <html>
                <head>
                <meta charset='utf8'>
                <title>Teddit</title>
                </head>
                <body>
                <h2>Wrong state value</h2>
                </body>
                </html>";
            }
            var response = context.Response;
            // отправляемый в ответ код htmlвозвращает
          
            byte[] buffer = Encoding.UTF8.GetBytes(responseText);
            // получаем поток ответа и пишем в него ответ
            response.ContentLength64 = buffer.Length;
            using Stream output = response.OutputStream;
            // отправляем данные
            output.WriteAsync(buffer);
            output.FlushAsync();
            server.Stop();
            return code;
        }

    }
    public class TokenRetrieval
    {
        public string code;
        public string state;
        public TokenRetrieval()
        {
                
        }
        public TokenRetrieval(string code,string state)
        {
            this.code = code;
            this.state = state;
        }
    }
}
