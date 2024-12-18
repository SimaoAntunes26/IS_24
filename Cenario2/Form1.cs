using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Cenario2
{
    public partial class Form1 : Form
    {
        private string curApp = null;
        private string curContainer = null;
        private string url = "http://localhost:50669/api/somiod/";

        public Form1()
        {
            InitializeComponent();
        }

        #region General Use Functions

        private async Task<string> getXmlString(string endpoint, string locate)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // GET ALL from API
                    if (locate != null)
                        client.DefaultRequestHeaders.Add("somiod-locate", locate);
                    

                    HttpResponseMessage response = await client.GetAsync(url + endpoint);

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

        private async void getChildren(string endpoint, string locate, ListBox listBoxChild)
        {
            string responseData = await getXmlString(endpoint, locate);

            // Adding containers fetched to listbox
            listBoxChild.Items.Clear();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(responseData);

            XmlNodeList nodes = xmlDoc.GetElementsByTagName("string");

            foreach (XmlNode node in nodes)
                listBoxChild.Items.Add(node.InnerText);

            if (listBoxChild.Items.Count == 0)
                listBoxChild.Items.Add("No items found.");
        }

        private async void create(string endpoint, XDocument xml)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpContent content = new StringContent(xml.ToString(), Encoding.UTF8, "application/xml");

                    // POST application
                    HttpResponseMessage response = await client.PostAsync(url + endpoint, content);
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void delete(string endpoint)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // DELETE application
                    HttpResponseMessage response = await client.DeleteAsync(url + endpoint);
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void update(string endpoint, XDocument xml)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpContent content = new StringContent(xml.ToString(), Encoding.UTF8, "application/xml");

                    // PUT application
                    HttpResponseMessage response = await client.PutAsync(url + endpoint, content);
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Application functions
        private async void getAllAppsButton_Click(object sender, EventArgs e)
        {
            string responseData = await getXmlString("", "application");

            // Adding applications fetched to listbox
            appListBox.Items.Clear();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(responseData);

            XmlNodeList nodes = xmlDoc.GetElementsByTagName("string");

            foreach (XmlNode node in nodes)
                appListBox.Items.Add(node.InnerText);

            if (appListBox.Items.Count == 0)
                appListBox.Items.Add("No items found.");
        }

        private async void getAppSelectedButton_Click(object sender, EventArgs e)
        {
            if(appListBox.SelectedItem == null)
            {
                MessageBox.Show("No application selected from the list!");
                return;
            }

            string responseData = await getXmlString(appListBox.SelectedItem.ToString(), null);

            // Adding fetched application's details to labels
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(responseData);
                    
            XmlNode id = xmlDoc.GetElementsByTagName("Id")[0];
            XmlNode name = xmlDoc.GetElementsByTagName("Name")[0];
            XmlNode date = xmlDoc.GetElementsByTagName("CreationDatetime")[0];

            appIdLabel.Text = "Id: " + id.InnerText;
            appNameLabel.Text = "Name: " + name.InnerText;
            appCreationDateLabel.Text = "Date: " + date.InnerText;
        }

        private void getAppChildren(string locate, ListBox listBoxChild)
        {
            if (appListBox.SelectedItem == null)
            {
                MessageBox.Show("No application selected from the list!");
                return;
            }

            curApp = appListBox.SelectedItem.ToString();

            getChildren(curApp, locate, listBoxChild);
        }

        private void getAppContainersButton_Click(object sender, EventArgs e)
        {
            getAppChildren("container", containerListBox);
        }

        private void getAppRecordsButton_Click(object sender, EventArgs e)
        {
            getAppChildren("record", recordListBox);
        }

        private void getAppNotifsButton_Click(object sender, EventArgs e)
        {
            getAppChildren("notification", notifListBox);
        }

        private void createAppButton_Click(object sender, EventArgs e)
        {
            var xml =
            new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(
                    XName.Get("Application", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"),
                    new XAttribute(XNamespace.Xmlns + "i", "http://www.w3.org/2001/XMLSchema-instance"),
                    new XElement(XName.Get("Name", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"), createAppNameTextbox.Text)
                )
            );

            create("", xml);
        }

        private void updateAppButton_Click(object sender, EventArgs e)
        {
            if (appListBox.SelectedItem == null)
            {
                MessageBox.Show("No application selected from the list!");
                return;
            }

            string app = appListBox.SelectedItem.ToString();

            var xml =
            new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(
                    XName.Get("Application", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"),
                    new XAttribute(XNamespace.Xmlns + "i", "http://www.w3.org/2001/XMLSchema-instance"),
                    updateAppNameTexbox.Text.Length == 0 ? null : 
                    new XElement(XName.Get("Name", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"), updateAppNameTexbox.Text)
                )
            );

            update(app, xml);

            MessageBox.Show($"Succesfully updated application \'{app}\'");
        }

        private void deleteAppButton_Click(object sender, EventArgs e)
        {
            if (appListBox.SelectedItem == null)
            {
                MessageBox.Show("No application selected from the list!");
                return;
            }

            string app = appListBox.SelectedItem.ToString();

            delete(app);

            MessageBox.Show($"Succesfully deleted application \'{app}\'");
        }

        #endregion

        #region Container functions
        private async void getContainerSelectedButton_Click(object sender, EventArgs e)
        {
            if (containerListBox.SelectedItem == null)
            {
                MessageBox.Show("No container selected from the list!");
                return;
            }

            if (curApp == null)
            {
                MessageBox.Show("No parent application: must use \'Get containers\' first");
                return;
            }

            string responseData = await getXmlString(curApp + "/" + containerListBox.SelectedItem.ToString(), null);

            // Adding fetched containers's details to labels
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(responseData);

            XmlNode id = xmlDoc.GetElementsByTagName("Id")[0];
            XmlNode name = xmlDoc.GetElementsByTagName("Name")[0];
            XmlNode date = xmlDoc.GetElementsByTagName("CreationDatetime")[0];

            containerIdLabel.Text = "Id: " + id.InnerText;
            containerNameLabel.Text = "Name: " + name.InnerText;
            containerCreationDateLabel.Text = "Date: " + date.InnerText;
        }

        private void getContainerChildren(string locate, ListBox listBoxChild)
        {
            if (containerListBox.SelectedItem == null)
            {
                MessageBox.Show("No container selected from the list!");
                return;
            }

            if (curApp == null)
            {
                MessageBox.Show("No parent application: must use \'Get containers\' first");
                return;
            }

            curContainer = containerListBox.SelectedItem.ToString();

            getChildren(curApp + "/" + curContainer, locate, listBoxChild);
        }


        private void getContainerRecordsButton_Click(object sender, EventArgs e)
        {
            getContainerChildren("record", recordListBox);
        }

        private void getContainerNotifsButton_Click(object sender, EventArgs e)
        {
            getContainerChildren("notification", notifListBox);
        }

        private void createContainerButton_Click(object sender, EventArgs e)
        {
            if (curApp == null)
            {
                MessageBox.Show("No parent application: must use \'Get containers\' first");
                return;
            }

            var xml =
            new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(
                    XName.Get("Container", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"),
                    new XAttribute(XNamespace.Xmlns + "i", "http://www.w3.org/2001/XMLSchema-instance"),
                    new XElement(XName.Get("Name", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"),
                                            createContainerNameTextbox.Text
                                          )
                )
            );

            create(curApp, xml);
        }

        private void updateContainerButton_Click(object sender, EventArgs e)
        {
            if (containerListBox.SelectedItem == null)
            {
                MessageBox.Show("No container selected from the list!");
                return;
            }

            if (curApp == null)
            {
                MessageBox.Show("No parent application: must use \'Get containers\' first");
                return;
            }

            string container = containerListBox.SelectedItem.ToString();

            var xml =
            new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(
                    XName.Get("Container", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"),
                    new XAttribute(XNamespace.Xmlns + "i", "http://www.w3.org/2001/XMLSchema-instance"),
                    updateContainerNameTextbox.Text.Length == 0 ? null : 
                    new XElement(XName.Get("Name", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"),updateContainerNameTextbox.Text)
                )
            );

            update(curApp + "/" + container, xml);

            MessageBox.Show($"Succesfully updated container \'{container}\'");
        }

        private void deleteContainerButton_Click(object sender, EventArgs e)
        {
            if (containerListBox.SelectedItem == null)
            {
                MessageBox.Show("No container selected from the list!");
                return;
            }
            if (curApp == null)
            {
                MessageBox.Show("No parent application: must use \'Get containers\' first");
                return;
            }

            string container = containerListBox.SelectedItem.ToString();

            delete(curApp + "/" + container);

            MessageBox.Show($"Succesfully deleted container \'{container}\'");
        }

        #endregion

        #region Record functions
        private async void getRecordSelectedButton_Click(object sender, EventArgs e)
        {
            if (recordListBox.SelectedItem == null)
            {
                MessageBox.Show("No record selected from the list!");
                return;
            }

            if (curApp == null || curContainer == null)
            {
                MessageBox.Show("No parent application and/or container: must use \'Get containers\' (Application) and \'Get records\' (Container) first");
                return;
            }

            string responseData = await getXmlString(curApp + "/" + curContainer + "/record/" + recordListBox.SelectedItem.ToString(), null);

            // Adding fetched records's details to labels
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(responseData);

            XmlNode id = xmlDoc.GetElementsByTagName("Id")[0];
            XmlNode name = xmlDoc.GetElementsByTagName("Name")[0];
            XmlNode date = xmlDoc.GetElementsByTagName("CreationDatetime")[0];
            XmlNode content = xmlDoc.GetElementsByTagName("Content")[0];

            recordIdLabel.Text = "Id: " + id.InnerText;
            recordNameLabel.Text = "Name: " + name.InnerText;
            recordCreationDateLabel.Text = "Date: " + date.InnerText;
            recordContentTextbox.Text = content.InnerText;
        }

        private void createRecordButton_Click(object sender, EventArgs e)
        {
            if (curApp == null || curContainer == null)
            {
                MessageBox.Show("No parent application and/or container: must use \'Get containers\' (Application) and \'Get records\' (Container) first");
                return;
            }

            var xml =
            new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(
                    XName.Get("Record", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"),
                    new XAttribute(XNamespace.Xmlns + "i", "http://www.w3.org/2001/XMLSchema-instance"),
                    new XElement(XName.Get("Name", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"),
                                            createRecordNameTextbox.Text
                                          ),
                    new XElement(XName.Get("Content", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"),
                                            createRecordContentTextbox.Text
                                          )
                )
            );

            create(curApp + "/" + curContainer + "/record/", xml);
        }

        private void deleteRecordButton_Click(object sender, EventArgs e)
        {
            if (recordListBox.SelectedItem == null)
            {
                MessageBox.Show("No record selected from the list!");
                return;
            }

            if (curApp == null || curContainer == null)
            {
                MessageBox.Show("No parent application and/or container: must use \'Get containers\' (Application) and \'Get records\' (Container) first");
                return;
            }

            string record = recordListBox.SelectedItem.ToString();

            delete(curApp + "/" + curContainer + "/record/" + record);

            MessageBox.Show($"Succesfully deleted container \'{record}\'");
        }
        #endregion

        #region Notif functions
        private void createNotifButton_Click(object sender, EventArgs e)
        {
            if (curApp == null || curContainer == null)
            {
                MessageBox.Show("No parent application and/or container: must use \'Get containers\' (Application) and \'Get records\' (Container) first");
                return;
            }

            string notif_event = null;
            foreach (Control control in createNotifRadioGroup.Controls)
            {
                if (control is System.Windows.Forms.RadioButton radioButton && radioButton.Checked)
                {
                    notif_event = radioButton.Text; // Return the Text property of the selected RadioButton
                }
            }

            if (notif_event == null)
            {
                MessageBox.Show("No event type selected");
                return;
            }

            var xml =
            new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(
                    XName.Get("Notification", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"),
                    new XAttribute(XNamespace.Xmlns + "i", "http://www.w3.org/2001/XMLSchema-instance"),
                    new XElement(XName.Get("Name", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"),
                                            createNotifNameTextbox.Text
                                          ),
                    new XElement(XName.Get("Endpoint", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"),
                                            createNotifEndpointTextbox.Text
                                          ),
                    new XElement(XName.Get("Event", "http://schemas.datacontract.org/2004/07/SOMIOD.Models"),
                                            notif_event.ToLower()
                                          )
                )
            );

            create(curApp + "/" + curContainer + "/notif/", xml);
        }

        private async void getNotifSelectedButton_Click(object sender, EventArgs e)
        {
            if (notifListBox.SelectedItem == null)
            {
                MessageBox.Show("No notifications selected from the list!");
                return;
            }

            if (curApp == null || curContainer == null)
            {
                MessageBox.Show("No parent application and/or container: must use \'Get containers\' (Application) and \'Get records\' (Container) first");
                return;
            }

            string responseData = await getXmlString(curApp + "/" + curContainer + "/notif/" + notifListBox.SelectedItem.ToString(), null);

            // Adding fetched notif's details to labels
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(responseData);

            XmlNode id = xmlDoc.GetElementsByTagName("Id")[0];
            XmlNode name = xmlDoc.GetElementsByTagName("Name")[0];
            XmlNode date = xmlDoc.GetElementsByTagName("CreationDatetime")[0];
            XmlNode notif_event = xmlDoc.GetElementsByTagName("Event")[0];
            XmlNode endpoint = xmlDoc.GetElementsByTagName("Endpoint")[0];

            notifIdLabel.Text = "Id: " + id.InnerText;
            notifNameLabel.Text = "Name: " + name.InnerText;
            notifCreationDateLabel.Text = "Date: " + date.InnerText;
            notifEventLabel.Text = "Event: " + notif_event.InnerText;
            notifEndpointLabel.Text = "Endpoint: " + endpoint.InnerText;
        }

        private void deleteNotifButton_Click(object sender, EventArgs e)
        {
            if (notifListBox.SelectedItem == null)
            {
                MessageBox.Show("No notification selected from the list!");
                return;
            }

            if (curApp == null || curContainer == null)
            {
                MessageBox.Show("No parent application and/or container: must use \'Get containers\' (Application) and \'Get records\' (Container) first");
                return;
            }

            string notif = notifListBox.SelectedItem.ToString();

            delete(curApp + "/" + curContainer + "/notif/" + notif);

            MessageBox.Show($"Succesfully deleted container \'{notif}\'");
        }
        #endregion
    }
}
