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

                    Console.WriteLine("Site Accepted: " + BreakPoint[0]);//https://
                    Console.WriteLine("Method Accepted: " + (Method)method);

                    HttpWebRequest request = WebRequest.CreateHttp(BreakPoint[0]);
                    if(method == 0)
                    {
                        string breakPoint = BreakPoint[index];
                        byte[] dados = Encoding.UTF8.GetBytes(breakPoint);
                        Console.WriteLine("Breakpoint: " + breakPoint);

                        request.ContentType = "application/x-www-form-urlencoded";
                        request.ContentLength = dados.Length;
                        request.Method = ((Method)method).ToString();
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
                    else
                    {
                        request.Method = ((Method)method).ToString();
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
                    Console.WriteLine("[x] Incorrect values ​​for requisition [!]");
                }

            }
            catch (FormatException ex)
            {
                Console.WriteLine("[x] FormatError:  [" + ex.Message + "]");
            }
            catch (WebException ex)
            {
                Console.WriteLine("[x] WebException:  [" + ex.Message + "]");
            }

            Process.GetCurrentProcess().WaitForExit();
        }
    }
}
