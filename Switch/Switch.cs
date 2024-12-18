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

namespace Switch
{
    public partial class Switch : System.Windows.Forms.Form
    {
        //TODO Mudar o url do cliente
        MqttClient mClient = new MqttClient("test.mosquitto.org");
        string baseURI = @"http://localhost:50669/api/somiod";
        Application app = new Application(100, "Switch", DateTime.Now);
        string mStrTopicsInfo = "lightBulbState";

        bool lightState = false;

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

        private void sendRecord()
        {
            Record record = new Record();
            if (lightState)
            {
                record.Content = "on";
            }
            else
            {
                record.Content = "off";
            }
            try
            {
                string xmlData = SerializeToXml(record);

                var client = new RestClient(baseURI);
                var request = new RestRequest();

                request.Method = Method.Post;
                request.AddHeader("Content-Type", "record/xml;charset=utf-8");
                request.AddParameter("record/xml", xmlData, ParameterType.RequestBody);

                var response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
                {
                    System.Windows.Forms.MessageBox.Show("Record successfully sent!");
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
    }
}
