﻿using Flashcard_app.MainForm.Home_Form;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Flashcard_app
{
    public partial class Home : Form
    {
        private Form f;
        public Home()
        {
            InitializeComponent();

        }
        private SqlConnection GetConnection()
        {
            string connectionstring = @"Data Source=NHATANH;Initial Catalog=\App Flashcard\;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            return new SqlConnection(connectionstring);
        }


        private void FormAnsOpen_DoubleClick(object sender, EventArgs e)
        {
            Form_AnswerFolders folder = new Form_AnswerFolders();
            folder.StartPosition = FormStartPosition.Manual;

            if (Form_Main.Instance != null)          //Nếu form Main đang được mở thì tắt, và lấy vị trí của nó 
            {
                folder.Location = Form_Main.Instance.Location;
                Form_Main.Instance.Hide();
            }
            else
            {
                folder.Location = new Point(100, 100); // fallback nếu Instance null
                                                       // nếu không lấy được vị trí cũ của main thì lấy đại ví trí đâu đó :)
            }

            folder.Show();
        }
        private void bt_AddMain_Click(object sender, EventArgs e)
        {
            Form_AddFolder addfolder = new Form_AddFolder();
            addfolder.Size = new Size(440, 300);

            //Postion of button 
            Point buttonScreenPoint = bt_AddMain.PointToScreen(Point.Empty);
            int x = buttonScreenPoint.X;
            int y = buttonScreenPoint.Y + bt_AddMain.Width +5;

            addfolder.StartPosition = FormStartPosition.Manual;
            addfolder.Location = new Point(x, y);


            addfolder.FormBorderStyle = FormBorderStyle.FixedToolWindow; // hoặc None
            addfolder.ShowDialog();

        }
        //Add new folder 
        private void AddnewFolder()
        {
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    string querry = "insert into Folder (foldername,  created,islearned) values (@name, @date, @status) ";

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error creating new folder");
                }
            }
        }

        //LoadNewFolder
        private void LoadFolder()
        {
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string querry = "Select * From Folder";
                    SqlCommand cmd = new SqlCommand(querry, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    //ColorPanel
                    string colorStr = reader["PanelColor"]?.ToString();
                    if (!string.IsNullOrEmpty(colorStr))
                    {
                        pn_Folder.BackColor = ColorTranslator.FromHtml(colorStr);
                    }
                    else
                    {
                        pn_Folder.BackColor = Color.LightGray; // màu mặc định nếu không chọn
                    }

                    while (reader.Read())
                    {
                        string folderName = reader["foldername"].ToString();
                        int folderId = Convert.ToInt32(reader["folderid"]);

                        Panel panel = new Panel();
                        panel.Width = 197;
                        panel.Height = 207;
                        panel.BackColor = Color.FromArgb(232, 241, 245);

                        Label label = new Label();
                        label.Text = folderName;
                        label.Dock = DockStyle.Fill;

                        panel.Controls.Add(label);
                        flowLayout_Main.Controls.Add(panel);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading folder" + ex.Message);
                }
            }
        }



        private void Home_Load(object sender, EventArgs e)
        {

        }
        private void pn_Folder_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pn_Folder_Paint_1(object sender, PaintEventArgs e)
        {

        }

       
    }
}
