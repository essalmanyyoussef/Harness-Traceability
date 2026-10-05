using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.DirectoryServices.AccountManagement;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics;
using System.Diagnostics;

namespace Harness_Traceability
{
    public partial class Form2 : Form
    {
        public static string Software_Version;
        public Form2()
        {
            InitializeComponent();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public static DataTable Site_List;
        public static string User_Mail;
        public static string User_name;
        Form1 FFF = new Form1();
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Site_List == null)
                {
                    Site_List = new DataTable();
                    Site_List.Columns.Add("SiteName", typeof(string)); // Ensure you only add one column here
                }
                IMG_Searching.Visible = true;
                button1.Enabled = false;
                lblMessage.Visible = false;
                string url = "https://ukhqengs001.sews-e.com/ElectricalTest/HarnessTraceabilityAuthentication";
                string username = textBox1.Text;
                //Site_List = new DataTable();

                // Clear the TextBox and ComboBox before showing the response
                //textBox1.Clear();
                comboBox1.Items.Clear();

                using (var client = new HttpClient())
                {
                    using (var form = new MultipartFormDataContent())
                    {
                        // Add the username field to the form data
                        form.Add(new StringContent(username), "username");

                        // Set headers (optional, based on your server's requirements)
                        client.DefaultRequestHeaders.Add("Accept", "*/*");

                        try
                        {
                            // Send the POST request
                            HttpResponseMessage response = await client.PostAsync(url, form);

                            // Check if the request was successful
                            if (response.IsSuccessStatusCode)
                            {
                                // Read the response content and parse it as XML
                                string result = await response.Content.ReadAsStringAsync();
                                //textBox1.Text = $"Success! Response from the server: \r\n{result}";

                                // Load the XML response
                                XDocument xmlDoc = XDocument.Parse(result);

                                // Extract site information
                                var sites = xmlDoc.Root.Element("Sites");

                                if (sites != null && sites.HasElements)
                                {
                                    // Add all "True" sites to the ComboBox
                                    bool userExists = false;
                                    foreach (var site in sites.Elements())
                                    {
                                        if (site.Value == "True")
                                        {
                                            comboBox1.Items.Add(site.Name.LocalName);
                                            Site_List.Rows.Add(site.Name.LocalName);
                                            //Site_List.Columns.Add(new DataColumn(site.Name.LocalName));
                                            userExists = true;
                                            lblMessage.Visible = false;
                                        }

                                    }

                                    // If no "True" sites were found, display error
                                    if (!userExists)
                                    {
                                        IMG_Searching.Visible = false;
                                        lblMessage.Visible = true;
                                        lblMessage.Text = "Error: User does not exist or not have access.";
                                    }
                                    else
                                    {
                                        User_name = textBox1.Text;
                                        comboBox1.SelectedIndex = 0;  // Select the first item
                                        
                                        User_Mail = System.DirectoryServices.AccountManagement.UserPrincipal.Current?.EmailAddress;
                                        IMG_Searching.Visible = false;
                                        button1.Enabled = true;
                                        this.DialogResult = DialogResult.OK;

                                        this.Close();
                                    }
                                }
                                else
                                {
                                    lblMessage.Visible = true;
                                    lblMessage.Text = "Error: User does not exist or not have access.";
                                    button1.Enabled = true;
                                    IMG_Searching.Visible = false;
                                }

                            }
                            else
                            {
                                // Display the status code if the request failed
                                
                                lblMessage.Visible = true;
                                lblMessage.Text = $"Failed! Status Code: {response.StatusCode}";
                                IMG_Searching.Visible = false;
                                button1 .Enabled = true;
                                return;
                            }




                        }
                        catch (Exception ex)
                        {
                            // Display any errors that occur during the request

                            IMG_Searching.Visible = false;

                            //textBox1.Text = $"Error: {ex.Message}";
                            lblMessage.Text = $"Error: {ex.Message}";
                            button1.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                Software_Version = "V2.0.0";
                UserPrincipal user = UserPrincipal.Current;
                textBox1.Text = user.DisplayName;
            }
            catch (PrincipalServerDownException ex)
            {
                MessageBox.Show("Could not contact the server: " + ex.Message);
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // The mailto URI scheme allows you to specify recipient, subject, and body in a link
            string email = "SEWS-ECentralElectricalTestEngineers@sgcci.onmicrosoft.com";
            string subject = Uri.EscapeUriString("Harness Traceability Software"); // Replace with desired subject
            string body = Uri.EscapeUriString(""); // Replace with desired body text

            // The mailto link to open Outlook
            string mailto = $"mailto:{email}?subject={subject}&body={body}";

            // Open Outlook or the default email client
            try
            {
                Process.Start(new ProcessStartInfo(mailto) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
