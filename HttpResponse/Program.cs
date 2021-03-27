using HttpResponse.Set;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security;
using System.Text;

namespace HttpResponse
{
    class Program
    {
        static void Main()
        {

            try
            {
                Console.Title = "HttpWebRequest v0.1";
                Console.WriteLine("[x] By: Wesley Vale.");

                Console.WriteLine("[x] Enter a URL: ");
                string Url = Console.ReadLine();

                Console.WriteLine("[x] Enter Method: [0 POST, 1 GET]");
                int method = int.Parse(Console.ReadLine());

                Console.WriteLine("[x] type the pause of the points ' ? ': ");
                int index = int.Parse(Console.ReadLine());

                if (Url != "" && Url.Contains("https://") && method == 0 && index > 0 || method == 1)
                {
                    string[] BreakPoint = Url.Split('?');

                    Console.WriteLine("Request Referer: " + BreakPoint[0]);//https://
                    Console.WriteLine("Request Method: " + (Method)method);

                    HttpWebRequest request = WebRequest.CreateHttp(BreakPoint[0]);
                    if(method == 0)
                    {
                        string breakPoint = BreakPoint[index];
                        byte[] dados = Encoding.ASCII.GetBytes(breakPoint);
                        Console.WriteLine("Breakpoint: " + breakPoint);
                        Console.WriteLine("Buffer: " + dados.Length);
                        
                        request.Method = ((Method)method).ToString();
                        request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
                        request.ContentLength = dados.Length;
                        request.ContentType = "application/x-www-form-urlencoded";//"multipart/form-data; boundary=----WebKitFormBoundaryZjAtXvxTO3xUgkEl";
                        request.Referer = BreakPoint[0];
                        request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36";

                        using var stream = request.GetRequestStream();
                        stream.Write(dados, 0, dados.Length);
                        stream.Close();
                        
                        using WebResponse response = request.GetResponse();
                        Stream streamDados = response.GetResponseStream();
                        StreamReader reader = new StreamReader(streamDados);
                        object objResponse = reader.ReadToEnd();
                        if (objResponse != null)
                        {
                            Console.WriteLine(objResponse.ToString());
                            streamDados.Close();
                            response.Close();
                        }
                    }
                    else if(method == 1)
                    {
                        request.Method = ((Method)method).ToString();
                        request.ContentType = "application/x-www-form-urlencoded";
                        request.Referer = BreakPoint[0];
                        request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36";
                        using WebResponse response = request.GetResponse();
                        Stream streamDados = response.GetResponseStream();
                        StreamReader reader = new StreamReader(streamDados);
                        object objResponse = reader.ReadToEnd();
                        if (objResponse != null)
                        {
                            Console.WriteLine(objResponse.ToString());
                            streamDados.Close();
                            response.Close();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("[x] Error requisition [!]");
                }

            }
            catch (FormatException ex)
            {
                Console.WriteLine("[x] FormatError:  [ " + ex.Message + " ] [!]");
            }
            catch (WebException ex)
            {
                Console.WriteLine("[x] WebException:  [ " + ex.Message + " ] [!]");
            }

            Process.GetCurrentProcess().WaitForExit();
        }
    }
}
