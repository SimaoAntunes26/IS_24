using System;
using System.IO;
using System.Net;
using System.Text;
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
        Application app = new Application(5, "Lightning", DateTime.Now);
        Container container = null;
        //TODO Mudar o url do cliente
        MqttClient mClient = new MqttClient("test.mosquitto.org");
        string[] mStrTopicsInfo = { "lightBulbState" };
        bool lightState = false;


        public LightningForm()
        {
            InitializeComponent();
            SendApplication();
        }

        private void SendApplication()
        {
            try
            {
                string xmlData = SerializeToXml(app);

                var client = new RestClient(baseURI);
                var request = new RestRequest();

                request.Method = Method.Post;
                request.AddHeader("Content-Type", "application/xml;charset=utf-8");
                request.AddParameter("application/xml", xmlData, ParameterType.RequestBody);

                var response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
                {
                    System.Windows.Forms.MessageBox.Show("Application successfully sent!");
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show($"Error: {response.StatusCode} - {response.Content}");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Exception: {ex.Message}");
            }
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

        private void createLightBulbButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (app.Id.HasValue)
                {
                    container = new Container(5, "Light Bulb", DateTime.Now, app.Id.Value);
                }
                else
                {
                    Console.WriteLine("app.Id is null. Cannot create Container.");
                }

                string xmlData = SerializeToXml(container);

                var client = new RestClient($"{baseURI}/{app.Name}");
                var request = new RestRequest();

                request.Method = Method.Post;
                request.AddHeader("Content-Type", "container/xml;charset=utf-8");
                request.AddParameter("container/xml", xmlData, ParameterType.RequestBody);

                var response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
                {
                    System.Windows.Forms.MessageBox.Show("Container successfully sent!");
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show($"Error: {response.StatusCode} - {response.Content}");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Exception: {ex.Message}");
            }

            try
            {
                Notification notification = null;
                if (container.Id.HasValue)
                {
                    notification = new Notification(5, "not1", DateTime.Now, container.Id.Value, "creation", "mqtt://192.168.1.2:1883",true);
                }
                else
                {
                    Console.WriteLine("container.Id is null. Cannot create notification.");
                }

                string xmlData = SerializeToXml(notification);

                var client = new RestClient($"{baseURI}/{app.Name}/{container.Name}");
                var request = new RestRequest();

                request.Method = Method.Post;
                request.AddHeader("Content-Type", "notification/xml;charset=utf-8");
                request.AddParameter("notification/xml", xmlData, ParameterType.RequestBody);

                var response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
                {
                    System.Windows.Forms.MessageBox.Show("Notification successfully sent!");
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show($"Error: {response.StatusCode} - {response.Content}");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Exception: {ex.Message}");
            }


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
