using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;


namespace Cryptod
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();


            string myFolder = Path.GetTempPath();
            if (File.Exists(myFolder + "//cryptodconfig.txt") && new FileInfo(myFolder + "//cryptodconfig.txt").Length != 0 )
            {

                string tokenString = File.ReadLines(myFolder + "//cryptodconfig.txt").Skip(1).Take(1).First();

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://keeper.p2pcrypto.dev.usabilityprovider.com/api/balance/paid_tariffs");
                request.Method = "GET";
                request.Headers["X-JWT"] = tokenString;
                request.UserAgent = "keeper.cryptod";
                request.ContentType = "*/*";


                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    StringBuilder output = new StringBuilder();

                    string result = reader.ReadToEnd();

                    List<resrec> responseObjectWH = new List<resrec>();
                    JavaScriptSerializer oJSWH = new JavaScriptSerializer();
                    responseObjectWH = oJSWH.Deserialize<List<resrec>>(result);
                    decimal sumWorkHours = 0;
                    sumWorkHours = responseObjectWH.Sum(em => em.work_hours);
                    label8.Text = sumWorkHours.ToString();

                    List<resrec> responseObjectGB = new List<resrec>();
                    JavaScriptSerializer oJSGB = new JavaScriptSerializer();
                    responseObjectGB = oJSGB.Deserialize<List<resrec>>(result);
                    decimal sumGB = 0;
                    sumGB = responseObjectGB.Sum(em => em.disk_space);
                    label19.Text = sumGB.ToString();

                    List<resrec> responseObjectPrice = new List<resrec>();
                    JavaScriptSerializer oJSPrice = new JavaScriptSerializer();
                    responseObjectPrice = oJSPrice.Deserialize<List<resrec>>(result);
                    decimal sumPrice = 0;
                    sumPrice = responseObjectPrice.Sum(em => em.price);
                    label6.Text = sumPrice.ToString();

                    Login.Size = new Size(0,0);

                }
                catch (Exception ex)
                {
                    var wex = GetNestedException<WebException>(ex);
                    if (wex == null) { throw; }
                    var response = wex.Response as HttpWebResponse;
                    if (response == null || response.StatusCode != HttpStatusCode.Forbidden)
                    {
                        throw;
                    }

                    string myFolder1 = Path.GetTempPath();
                    string tokenString1 = File.ReadLines(myFolder1 + "//cryptodconfig.txt").Skip(1).Take(1).First();

                    HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create("https://keeper.p2pcrypto.dev.usabilityprovider.com/api/auth");
                    request1.Method = "DELETE";
                    request1.Headers["X-JWT"] = tokenString1;
                    request1.ContentType = "application/x-www-form-urlencoded";
                    string postData1 = "";
                    byte[] bytes = Encoding.UTF8.GetBytes(postData1);
                    request1.ContentLength = bytes.Length;

                    Stream requestStream1 = request1.GetRequestStream();
                    requestStream1.Write(bytes, 0, bytes.Length);

                    WebResponse response1 = request1.GetResponse();
                    Stream stream1 = response1.GetResponseStream();
                    StreamReader reader1 = new StreamReader(stream1);

                    var result1 = reader1.ReadToEnd();
                    stream1.Dispose();
                    reader1.Dispose();

                    File.Delete(myFolder + "//cryptodconfig.txt");
                    MessageBox.Show("IP Changed. Logged out.");
                    Application.Restart();
                }


                var timerLive = new System.Threading.Timer((e) =>
                {
                    string myFolder1 = Path.GetTempPath();
                    string tokenString1 = File.ReadLines(myFolder1 + "//cryptodconfig.txt").Skip(1).Take(1).First();
                    HttpWebRequest requ8hest = (HttpWebRequest)WebRequest.Create("https://keeper.p2pcrypto.dev.usabilityprovider.com/api/keeper/live");
                    requ8hest.Method = "GET";
                    requ8hest.Headers["X-JWT"] = tokenString1;
                    requ8hest.UserAgent = "keeper.cryptod";
                    requ8hest.ContentType = "*/*";

                    HttpWebResponse respon435se = (HttpWebResponse)requ8hest.GetResponse();
                    StreamReader read123er = new StreamReader(respon435se.GetResponseStream());
                    StringBuilder output = new StringBuilder();

                    string res123ult = read123er.ReadToEnd();

                }, null, 0, (int)TimeSpan.FromMinutes(1).TotalMilliseconds);

                var timerDownload = new System.Threading.Timer((e) =>
                {

                    string myFolder1 = Path.GetTempPath();
                    string tokenString1 = File.ReadLines(myFolder1 + "//cryptodconfig.txt").Skip(1).Take(1).First();
                    HttpWebRequest requ8hest = (HttpWebRequest)WebRequest.Create("https://keeper.p2pcrypto.dev.usabilityprovider.com/api/keeper/files");
                    requ8hest.Method = "GET";
                    requ8hest.Headers["X-JWT"] = tokenString1;
                    requ8hest.UserAgent = "keeper.cryptod";
                    requ8hest.ContentType = "application/x-www-form-urlencoded";

                    HttpWebResponse respon435se = (HttpWebResponse)requ8hest.GetResponse();
                    StreamReader read123er = new StreamReader(respon435se.GetResponseStream());
                    StringBuilder output = new StringBuilder();

                    string res123ult = read123er.ReadToEnd();

                }, null, 0, (int)TimeSpan.FromMinutes(5).TotalMilliseconds);

                var timerUpload = new System.Threading.Timer((e) =>
                {
                    /*string myFolder12 = Path.GetTempPath();
                    string MyFile12 = myFolder12 + "//cryptodconfig.txt";
                    string tokenString12 = File.ReadLines(MyFile12).Skip(1).Take(1).First();

                    HttpWebRequest request12 = (HttpWebRequest)WebRequest.Create("https://keeper.p2pcrypto.dev.usabilityprovider.com/api/keeper/files");
                    request12.Method = "PUT";
                    request12.ContentType = "application/*";
                    string postData = "test";
                    byte[] bytes = Encoding.UTF8.GetBytes(postData);
                    request12.ContentLength = bytes.Length;

                    Stream requestStream = request12.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);

                    WebResponse response = request12.GetResponse();
                    Stream stream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream);

                    var result = reader.ReadToEnd();
                    stream.Dispose();
                    reader.Dispose();
                    */
                }, null, 0, (int)TimeSpan.FromMinutes(7).TotalMilliseconds);

            }
            else
            {
                groupBox2.Size = new Size(0, 0);

            }
           

        }


        public class resrec
        {
            public int id;
            public decimal price;
            public long created;
            public decimal disk_space;
            public string currency;
            public decimal work_hours;
            public resrec()
            {
                id = 0;
                price = 0;
                created = 0;
                disk_space = 0;
                currency = "";
                work_hours = 0;
            }
        }

        static T GetNestedException<T>(Exception ex) where T : Exception
        {
            if (ex == null) { return null; }

            var tEx = ex as T;
            if (tEx != null) { return tEx; }

            return GetNestedException<T>(ex.InnerException);
        }

        private void OnTextChanged(object sender, EventArgs args)
        {
            UpdateUserInterface();
        }
        private void UpdateUserInterface()
        {
            this.button1.Enabled = !string.IsNullOrWhiteSpace(this.textBox1.Text) && !string.IsNullOrWhiteSpace(this.textBox2.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = !string.IsNullOrWhiteSpace(this.textBox1.Text) && !string.IsNullOrWhiteSpace(this.textBox2.Text);

            string username = textBox1.Text;
            string password = textBox2.Text;

            if (String.IsNullOrWhiteSpace(textBox1.Text) && String.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Complete Email and Password");
                Environment.Exit(0);
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://keeper.p2pcrypto.dev.usabilityprovider.com/api/auth");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            string postData = "email="+username+"&password="+password;
            byte[] bytes = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = bytes.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();

            label22.Text = result;

            dynamic stuff = JsonConvert.DeserializeObject(result);

            if (stuff.status == "error")
            {
                MessageBox.Show("Wrong Email or Password");
                Environment.Exit(0);
            }

            string myFolder = Path.GetTempPath();
            label24.Text = myFolder;

            System.IO.StreamWriter file = new System.IO.StreamWriter(myFolder + "//cryptodconfig.txt");
            file.WriteLine(stuff.user.email);
            file.WriteLine(stuff.user.token);
            file.Close();

            Application.Restart();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string myFolder = Path.GetTempPath();
            string tokenString = File.ReadLines(myFolder + "//cryptodconfig.txt").Skip(1).Take(1).First();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://keeper.p2pcrypto.dev.usabilityprovider.com/api/auth");
            request.Method = "DELETE";
            request.Headers["X-JWT"] = tokenString;
            request.ContentType = "application/x-www-form-urlencoded";
            string postData = "";
            byte[] bytes = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = bytes.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();

            File.Delete(myFolder + "//cryptodconfig.txt");
            MessageBox.Show("Logged out");
            Application.Exit();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://cryptod.com/?act=forget");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://cryptod.com/?act=register");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string myFolder = Path.GetTempPath();
            string emailString = File.ReadLines(myFolder + "//cryptodconfig.txt").Skip(0).Take(1).First();
            label18.Text = emailString;
        }


    private void button3_Click(object sender, EventArgs e)
        {

        }


    }


}
