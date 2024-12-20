using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using SOMIOD.Models;
using System.IO;
using System.Xml.Serialization;
using System.Net;
using RestSharp;
using System.Xml.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Windows.Forms;
using System.Xml;

namespace Switch
{
    public partial class Switch : System.Windows.Forms.Form
    {
        //TODO Mudar o url do cliente
        MqttClient mClient = new MqttClient("127.0.0.1");
        string baseURI = @"http://localhost:50669/api/somiod";
        string mStrTopicsInfo = "lightBulbState";
        XDocument app = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(
                    XName.Get("Application", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"),
                    new XAttribute(XNamespace.Xmlns + "i", "http://www.w3.org/2001/XMLSchema-instance"),
                    new XElement(XName.Get("Name", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"), "Switch")
                    )
                );

        bool lightState = false;

        private async Task<string> getXmlString(string endpoint, string locate)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // GET ALL from API
                    if (locate != null)
                        client.DefaultRequestHeaders.Add("somiod-locate", locate);


                    HttpResponseMessage response = await client.GetAsync(baseURI + endpoint);

                    response.EnsureSuccessStatusCode();
                    string responseData = await response.Content.ReadAsStringAsync(); // XML string

                    return responseData;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private async void create(string endpoint, XDocument xml)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpContent content = new StringContent(xml.ToString(), Encoding.UTF8, "application/xml");

                    // POST application
                    HttpResponseMessage response = await client.PostAsync(baseURI + endpoint, content);
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Error: {ex.Message}", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public Switch()
        {
            InitializeComponent();
            SendApplication();
        }

        private void Switch_Load(object sender, EventArgs e)
        {
            mClient.Connect(Guid.NewGuid().ToString());
            if (!mClient.IsConnected)
            {
                System.Windows.Forms.MessageBox.Show("Error connecting to message broker...");
                return;
            }

        }

        private void SendApplication()
        {
            create("", app);
        }

        private string SerializeToXml<T>(T obj)
        {
            //TODO Provavelmente esta funcao tem de ser alterada para corresponder ao formato correto do somiod
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringWriter textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, obj);
                return textWriter.ToString();
            }
        }

        private void publishState(string state)
        {
            //TODO Maybe esta funcao precisa de mudar? Implementacao do mosquitto pelo somiod
            mClient.Publish(mStrTopicsInfo, Encoding.UTF8.GetBytes(state));
        }

        private void turnOnButton_Click(object sender, EventArgs e)
        {
            lightState = true;
            publishState(lightState.ToString());
            sendRecord();
        }

        private void turnOffButton_Click(object sender, EventArgs e)
        {
            lightState = false;
            publishState(lightState.ToString());
            sendRecord();
        }

        private async void sendRecord()
        {
            string application = await getXmlString("Lightning", null);

            // Adding fetched application's details to labels
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(application);

            XmlNode appName = xmlDoc.GetElementsByTagName("Name")[0];

            string container = await getXmlString("Lightning/lightBulb", null);

            // Adding fetched container's details to labels
            XmlDocument xmlDocument = new XmlDocument();
            xmlDoc.LoadXml(container);

            XmlNode containerName = xmlDocument.GetElementsByTagName("Name")[0];

            string s_appName= appName.InnerText;
            string s_containerName = containerName.InnerText;

            string content;

            if (lightState)
            {
                content = "on";
            }
            else
            {
                content = "off";
            }

            var xml =
                new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement(
                        XName.Get("Record", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"),
                        new XAttribute(XNamespace.Xmlns + "i", "http://www.w3.org/2001/XMLSchema-instance"),
                        new XElement(XName.Get("Name", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"),
                                "rec"
                              ),
                        new XElement(XName.Get("Content", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"),
                        content)
                        )
                    );

            create(s_appName + "/" + s_containerName + "/record/", xml);
        }
    }
}
