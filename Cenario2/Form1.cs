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

        private async void getAppChildren(string locate, ListBox listBoxChild)
        {
            if (appListBox.SelectedItem == null)
            {
                MessageBox.Show("No application selected from the list!");
                return;
            }

            curApp = appListBox.SelectedItem.ToString();

            string responseData = await getXmlString(appListBox.SelectedItem.ToString(), locate);

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

        private async void getAppContainersButton_Click(object sender, EventArgs e)
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

        private async void createAppButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Create the JSON object with the variable name
                    var requestBody = new { name = createAppNameTextbox.Text };
                    string json = System.Text.Json.JsonSerializer.Serialize(requestBody);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    // POST application
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void updateAppButton_Click(object sender, EventArgs e)
        {
            if (appListBox.SelectedItem == null)
            {
                MessageBox.Show("No application selected from the list!");
                return;
            }

            string app = appListBox.SelectedItem.ToString();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Create the JSON object with the variable name
                    var requestBody = new { name = updateAppNameTexbox.Text.Length == 0 ? null : updateAppNameTexbox.Text };
                    string json = System.Text.Json.JsonSerializer.Serialize(requestBody);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    // PUT application
                    HttpResponseMessage response = await client.PutAsync(url + app, content);
                    response.EnsureSuccessStatusCode();

                    MessageBox.Show($"Succesfully updated application \'{app}\'");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void deleteAppButton_Click(object sender, EventArgs e)
        {
            if (appListBox.SelectedItem == null)
            {
                MessageBox.Show("No application selected from the list!");
                return;
            }

            string app = appListBox.SelectedItem.ToString();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // DELETE application
                    HttpResponseMessage response = await client.DeleteAsync(url + app);
                    response.EnsureSuccessStatusCode();

                    MessageBox.Show($"Succesfully deleted application \'{app}\'");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ACCIDENTAL :(
        private void appCreationDateLabel_Click(object sender, EventArgs e)
        {

        }

        private void appNameLabel_Click(object sender, EventArgs e)
        {

        }

        private void appIdLabel_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
    }
}
