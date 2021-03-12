using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Sly_WAL_Editor
{
    public partial class Sly_1 : Form
    {
        WAC wac = new WAC();
        string selectedItem = "";

        public Sly_1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "WAL Archive | *.WAL";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    listView1.Items.Clear();
                    using (FileStream WAL = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                    {
                        var WACDir = new FileInfo(Path.Combine(Path.GetDirectoryName(ofd.FileName), "SLY.WAC"));

                        if (File.Exists(WACDir.ToString()))
                        {
                            using (FileStream WAC = new FileStream(WACDir.ToString(), FileMode.Open, FileAccess.Read))
                            {
                                using (BinaryReader WACReader = new BinaryReader(WAC))
                                {
                                    using (BinaryReader WALReader = new BinaryReader(WAL))
                                    {
                                        int numFiles = 0;
                                        
                                        wac.entryCount = WACReader.ReadUInt32();
                                        wac.entries = new List<WACEntry>();

                                        WACEntry wacEntry = new WACEntry();
                                        wacEntry.files = new List<WACFileEntry>();

                                        for (int a = 0; a < wac.entryCount; a++)
                                        {
                                            byte letter;
                                            int temp = 0;
                                            wacEntry.fileNames = "";
                                            

                                            while (temp != 23)
                                            {
                                                letter = WACReader.ReadByte();
                                                if (letter != 0)
                                                {
                                                    wacEntry.fileNames += Encoding.Default.GetString(new byte[] { letter });
                                                    temp++;
                                                }

                                                else
                                                {
                                                    temp++;
                                                }
                                            }

                                            WACFileEntry wacFileEntry = new WACFileEntry();

                                            wacFileEntry.type = WACReader.ReadChar();
                                            wacFileEntry.offset = WACReader.ReadUInt32() * 2048;
                                            wacFileEntry.size = WACReader.ReadUInt32();

                                            long position = WAL.Position;
                                            WAL.Seek(wacFileEntry.offset, SeekOrigin.Begin);
                                            wacFileEntry.fileData = WALReader.ReadBytes(Convert.ToInt32(wacFileEntry.size));

                                            WAL.Seek(position, SeekOrigin.Begin);

                                            wacEntry.files.Add(wacFileEntry);

                                            ListViewItem lvi = new ListViewItem(wacEntry.fileNames, 0);
                                            
                                            lvi.SubItems.Add(wacFileEntry.size.ToString());
                                            lvi.SubItems.Add(wacFileEntry.type.ToString());
                                            lvi.SubItems.Add(wacFileEntry.offset.ToString("X"));

                                            listView1.Items.Add(lvi);

                                            wac.entries.Add(wacEntry);
                                            numFiles++;
                                        }

                                        label1.Text = numFiles + " Files";
                                        listView1.ContextMenuStrip = contextMenuStrip1;
                                    }
                                }
                            }
                        }

                        else
                        {
                            MessageBox.Show("SLY.WAC could not be found");
                        }

                    }
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                selectedItem = listView1.SelectedItems[0].Text;
            }
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            selectedItem = e.Item.Text;
            
        }

        private void extractToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Folder Selection."
            };

            DialogResult dialogResult = openFileDialog.ShowDialog();

            {
                string path = Path.GetDirectoryName(openFileDialog.FileName);
                string fileName = selectedItem.Split('\\')[0];
                //MessageBox.Show(fileName);
                ExportFile(fileName, path);
            }
            
        }

        private WACFileEntry FindFileEntry(string fileName)
        {
            WACFileEntry wacFileEntry = new WACFileEntry();

            for (int i = 0; i < wac.entries.Count; i++)
            {
                WACEntry wacEntry = wac.entries[i];
                
                if (wacEntry.fileNames.ToString().Equals(fileName))
                {
                    wacFileEntry = wacEntry.files[i];
                }
            }

            return wacFileEntry;
        }

        private void ExportFile(string fileName, string path)
        {
            WACFileEntry wacFileEntry = FindFileEntry(fileName);
            string filePath = Path.Combine(path, fileName);

            using (FileStream WAL = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                WAL.Write(wacFileEntry.fileData, 0, wacFileEntry.fileData.Length);
            }

            MessageBox.Show("Exported successfully.");
        }
    }
}
