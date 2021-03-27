using HttpResponse.Set;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security;
using System.Text;

namespace HttpResponse
{
    class Program
    {
        /*
         * @author: https://github.com/WesleyVale99
         * Sábado, 27 de março de 2021
         */
        static void Main()
        {

            try
            {
                Console.Title = "HttpWebRequest v0.1";
                Console.WriteLine("@author: https://github.com/WesleyVale99");
                Console.WriteLine("\n");
                Console.WriteLine("x---------------------------------------------------------------------------------------x");
                Console.WriteLine("[!] one of the methods to use the program is to leave g-recaptcha-response = null \n" +
                    "[!] so that the program can automatically add");
                Console.WriteLine("[!] Url example: https://test.com?user=12345&password=12345&g-recaptcha-response=");
                Console.WriteLine("x---------------------------------------------------------------------------------------x");
                Console.WriteLine("[!] however in case you don't need captcha you can just skip the process.d");
                Console.WriteLine("[!] Url example: https://test.com?user=12345&password=12345");
                Console.WriteLine("x---------------------------------------------------------------------------------------x");
                Console.WriteLine("\n");

                Console.WriteLine("[x] Enter type [0]:[1] (0: recaptcha TRUE, 1: recaptcha FALSE)");
                string type = Console.ReadLine();
                if(type == "0")
                {
                    Console.WriteLine("[x] Enter a URL: ");
                    string Url = Console.ReadLine();

                    Console.WriteLine("[x] Enter a Token ReCaptcha: ");
                    string Token = Console.ReadLine();

                    Console.WriteLine("[x] Enter Method: [0 POST, 1 GET]");
                    int method = int.Parse(Console.ReadLine());

                    if (Url != "" && (Url.Contains("https://") || Url.Contains("http://")) && Token != "" && (method == 0 || method == 1))
                    {

                        string[] BreakPoint = Url.Split('?');
                        if (method == 0)
                        {
                            string addtoken = ReCaptcha(Token);
                            if (addtoken == "404")
                                return;

                            string Mount = "";
                            string[] serialize = BreakPoint[1].Split('&');
                            for (int i = 0; i < serialize.Length; i++)
                            {
                                string spn = serialize[i];
                                if (spn == "g-recaptcha-response=")
                                {
                                    spn = "g-recaptcha-response=" + addtoken;
                                }
                                Mount += i == 0 ? spn : "&" + spn;
                            }

                            HttpWebRequest request = WebRequest.CreateHttp(BreakPoint[0]);

                            string breakPoint = string.Concat(Mount);
                            byte[] data = Encoding.UTF8.GetBytes(breakPoint);



                            request.Method = ((Method)method).ToString();
                            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
                            request.ContentLength = data.Length;
                            request.ContentType = "application/x-www-form-urlencoded";//"multipart/form-data; boundary=----WebKitFormBoundaryZjAtXvxTO3xUgkEl";
                            request.Referer = BreakPoint[0];
                            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36";
                            request.ContinueTimeout = 100;

                            using Stream stream = request.GetRequestStream();
                            stream.Write(data, 0, data.Length);
                            stream.Close();



                            string Format = JsonConvert.SerializeObject(request, Formatting.Indented);
                            Console.WriteLine("\n");
                            Console.WriteLine("Breakpoint: " + breakPoint);
                            Console.WriteLine(Format);

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
                        else if (method == 1)
                        {
                            HttpWebRequest request = WebRequest.CreateHttp(BreakPoint[0]);
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
                else if(type == "1")
                {
                    Console.WriteLine("[x] Enter a URL: ");
                    string Url = Console.ReadLine();

                    Console.WriteLine("[x] Enter Method: [0 POST, 1 GET]");
                    int method = int.Parse(Console.ReadLine());

                    if (Url != "" && (Url.Contains("https://") || Url.Contains("http://")) && (method == 0 || method == 1))
                    {

                        string[] BreakPoint = Url.Split('?');
                        if (method == 0)
                        {

                            HttpWebRequest request = WebRequest.CreateHttp(BreakPoint[0]);

                            string breakPoint = BreakPoint[1];
                            byte[] data = Encoding.UTF8.GetBytes(breakPoint);



                            request.Method = ((Method)method).ToString();
                            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
                            request.ContentLength = data.Length;
                            request.ContentType = "application/x-www-form-urlencoded";//"multipart/form-data; boundary=----WebKitFormBoundaryZjAtXvxTO3xUgkEl";
                            request.Referer = BreakPoint[0];
                            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36";
                            request.ContinueTimeout = 100;

                            using Stream stream = request.GetRequestStream();
                            stream.Write(data, 0, data.Length);
                            stream.Close();



                            string Format = JsonConvert.SerializeObject(request, Formatting.Indented);
                            Console.WriteLine("\n");
                            Console.WriteLine("Breakpoint: " + breakPoint);
                            Console.WriteLine(Format);

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
                        else if (method == 1)
                        {
                            HttpWebRequest request = WebRequest.CreateHttp(BreakPoint[0]);
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
        public static string ReCaptcha(string token)
        {
            string Url = "https://www.google.com/recaptcha/api2/anchor?ar=2&k="+ token + "&co=aHR0cHM6Ly9wYnRvcHouc2l0ZTo0NDM.&hl=pt-BR&v=5mNs27FP3uLBP3KBPib88r1g&size=normal&cb=ltvh0f2op6yi";
            HttpWebRequest request = WebRequest.CreateHttp(Url);
            if(request != null)
            {
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36";
                using WebResponse response = request.GetResponse();
                Stream streamDados = response.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                while (!reader.EndOfStream)
                {
                    string objResponse = reader.ReadLine();
                    if (objResponse != null && objResponse.Contains("value="))
                    {;
                        string[] split = objResponse.Split('=');
                        return split[3].Split(@"""")[1];
                    }
                }
                streamDados.Close();
                response.Close();
            }
            return "404";
        }
    }
}
