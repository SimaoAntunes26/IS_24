using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using RestSharp;
using SOMIOD.Models;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Lightning
{
    public partial class LightningForm : System.Windows.Forms.Form
    {
        string baseURI = @"http://localhost:50669/api/somiod";
        XDocument app = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(
                    XName.Get("Application", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"),
                    new XAttribute(XNamespace.Xmlns + "i", "http://www.w3.org/2001/XMLSchema-instance"),
                    new XElement(XName.Get("Name", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"), "Lightning")
                    )
                );
        Container container = null;
        //TODO Mudar o url do cliente
        MqttClient mClient = new MqttClient("127.0.0.1");
        string[] mStrTopicsInfo = { "lightBulbState" };
        bool lightState = false;


        public LightningForm()
        {
            InitializeComponent();
            SendApplication();
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

        private void SendApplication()
        {
            create("", app);
        }


        private void createLightBulbButton_Click(object sender, EventArgs e)
        {
            string name = app.Descendants(XName.Get("Name", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"))
                     .FirstOrDefault()?.Value;
            var container =
            new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(
                    XName.Get("Container", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"),
                    new XAttribute(XNamespace.Xmlns + "i", "http://www.w3.org/2001/XMLSchema-instance"),
                    new XElement(XName.Get("Name", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"),
                                            "lightBulb"
                                          )
                )
            );

            create(name, container);

            string containerName = container.Descendants(XName.Get("Name", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"))
                     .FirstOrDefault()?.Value;

            var notif =
            new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(
                    XName.Get("Notification", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"),
                    new XAttribute(XNamespace.Xmlns + "i", "http://www.w3.org/2001/XMLSchema-instance"),
                    new XElement(XName.Get("Name", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"),
                                    "notif1"
                                  ),
            new XElement(XName.Get("Endpoint", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"),
                                    "127.0.0.1"
                                  ),
            new XElement(XName.Get("Event", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"),
                                    "creation"
                                  )
        )
    );

            create(name + "/" + containerName + "/notif/", notif);
        }

        private void LightningForm_Load(object sender, EventArgs e)
        {
            mClient.Connect(Guid.NewGuid().ToString());
            if (!mClient.IsConnected)
            {
                System.Windows.Forms.MessageBox.Show("Error connecting to message broker...");
                return;
            }

            //Subscribe chat channel
            mClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };//QoS
            mClient.Subscribe(mStrTopicsInfo, qosLevels);

        }

        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Received = " + Encoding.UTF8.GetString(e.Message) + " on topic " +
            e.Topic);
            lightState = Convert.ToBoolean(Encoding.UTF8.GetString(e.Message));
            System.Windows.Forms.MessageBox.Show("Updated light state");
        }
    }
}
