using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Amazon.SecurityToken;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Amazon.DynamoDBv2.DataModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.S3.Model;

namespace WindowsFormsBulutArayüz
{
    public partial class Form2 : Form
    {
        public String user_id;
        public String user_photo;
        public Form2(String[] arr)
        {
            InitializeComponent();
            textBox8.Text = arr[0];
            textBox1.Text = arr[1];
            textBox2.Text = arr[2];
            textBox3.Text = arr[3];
            textBox4.Text = arr[4];
            textBox5.Text = arr[5];
            textBox6.Text = arr[6];
            textBox7.Text = arr[7];
            user_id = arr[0];
            user_photo = arr[7];
            if(arr[0] != null && arr[0] != "")
            {
                button1.Text = "Güncelle";
                button3.Visible = true;
                if(textBox7.Text != null && textBox7.Text != "")
                {
                    try
                    {
                        download_photo("bulutbilisimvize-2022", "fotograflar", textBox7.Text);
                    }
                        
                    catch {
                        int asdasd = 0;
                    }
                }
                
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(button1.Text == "Güncelle")
            {
                if(textBox8.Text != user_id)
                {
                    MessageBox.Show("ID değiştirilemez. Lütfen silip tekrar kaydedin.");
                }
                else
                {
                    if (user_photo != null && user_photo != "" && textBox7.Text != user_photo)
                    {
                        delete_photo(user_photo);
                    }
                    if (upload_photo(DosyaYolu, "bulutbilisimvize-2022", "fotograflar", textBox8.Text + "-" + DosyaAdi))
                    {
                        
                        UpdateItem(textBox8.Text, textBox1.Text, textBox2.Text, textBox3.Text,
                        textBox4.Text, textBox5.Text, textBox6.Text, textBox8.Text + "-" + DosyaAdi);
                        MessageBox.Show("Güncelleme başarılı.");
                        this.Close();
                    }
                    else
                    {
                        UpdateItem(textBox8.Text, textBox1.Text, textBox2.Text, textBox3.Text,
                        textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text);
                        MessageBox.Show("Güncelleme başarılı.");
                        this.Close();
                    }
                    
                }
                
            }
            else
            {
                
                if(upload_photo(DosyaYolu, "bulutbilisimvize-2022", "fotograflar", textBox8.Text + "-" + DosyaAdi))
                {
                    CreateItem(textBox8.Text, textBox1.Text, textBox2.Text, textBox3.Text,
                    textBox4.Text, textBox5.Text, textBox6.Text, textBox8.Text + "-" + textBox7.Text);
                    MessageBox.Show("Kayıt başarılı.");
                }
                else
                {
                    CreateItem(textBox8.Text, textBox1.Text, textBox2.Text, textBox3.Text,
                    textBox4.Text, textBox5.Text, textBox6.Text, "");
                    MessageBox.Show("Kayıt başarılı ancak fotoğraf yüklenmedi.");
                }

                

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                pictureBox1.Image = null;
                

            }
            //
            //CreateItem("33", "", "", "", "4", "5", "6", "f");
            //
            //DeleteItem("33");
            
            
            
        }

        private static string tableName = "bulutbilisimvizedb-2022";
        private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();

        

        private static void CreateItem(String id, String tur, String isim, String cinsiyet, String yas, String boy, String kilo, String foto_adi)
        {
            var request = new PutItemRequest
            {
                TableName = tableName,
                Item = new Dictionary<string, AttributeValue>()
            {
                { "user_id", new AttributeValue {
                    N = id
                }},
                { "tur", new AttributeValue {
                      S = tur
                  }},
                { "isim", new AttributeValue {
                      S = isim
                  }},
                { "cinsiyet", new AttributeValue {
                      S = cinsiyet
                  }},
                { "yas", new AttributeValue {
                      N = yas
                  }},
                { "boy", new AttributeValue {
                      N = boy
                  }},
                { "kilo", new AttributeValue {
                      N = kilo
                  }},
                { "foto_adi", new AttributeValue {
                      S = foto_adi
                  } }
            }
            };
            client.PutItem(request);
        }

        public static void GetItems(ListView lst)
        {
            var request = new ScanRequest
            {
                TableName = tableName,
            };

            var response = client.Scan(request);

            foreach (Dictionary<string, AttributeValue> item in response.Items)
            {
                // Process the result.
                ListViewItem itm = new ListViewItem(GetItemArray(item));
                lst.Items.Add(itm);
            }

        }

        private static String[] GetItemArray(
            Dictionary<string, AttributeValue> attributeList)
        {
            String[] arr = new string[8];
            
            foreach (KeyValuePair<string, AttributeValue> kvp in attributeList)
            {
                string attributeName = kvp.Key;
                AttributeValue value = kvp.Value;

                if(attributeName == "tur") {
                    arr[1] = (value.S == null ? "" : value.S) +
                    (value.N == null ? "" : value.N);
                }
                else if (attributeName == "isim")
                {
                    arr[2] = (value.S == null ? "" : value.S) +
                    (value.N == null ? "" : value.N);
                }
                else if (attributeName == "cinsiyet")
                {
                    arr[3] = (value.S == null ? "" : value.S) +
                    (value.N == null ? "" : value.N);
                }
                else if (attributeName == "yas")
                {
                    arr[4] = (value.S == null ? "" : value.S) +
                    (value.N == null ? "" : value.N);
                }
                else if (attributeName == "boy")
                {
                    arr[5] = (value.S == null ? "" : value.S) +
                    (value.N == null ? "" : value.N);
                }
                else if (attributeName == "kilo")
                {
                    arr[6] = (value.S == null ? "" : value.S) +
                    (value.N == null ? "" : value.N);
                }
                else if (attributeName == "foto_adi")
                {
                    arr[7] = (value.S == null ? "" : value.S) +
                    (value.N == null ? "" : value.N);
                }
                else if (attributeName == "user_id")
                {
                    arr[0] = (value.S == null ? "" : value.S) +
                    (value.N == null ? "" : value.N);
                }
                


            }

            return arr;
            
        }

        private static void UpdateItem(String id, String tur, String isim, String cinsiyet, String yas, String boy, String kilo, String foto_adi)
        {
            var request = new UpdateItemRequest
            {
                Key = new Dictionary<string, AttributeValue>()
            {
                { "user_id", new AttributeValue {
                      N = id
                  } }
            },



                ExpressionAttributeNames = new Dictionary<string, string>()
            {
                {"#A","tur"},
                {"#B","isim"},
                {"#C","cinsiyet"},
                {"#D","yas"},
                {"#E","boy"},
                {"#F","kilo"},
                {"#G","foto_adi"},
            },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
            {
                
                {":a1",new AttributeValue {
                     S = tur
                 }},
                {":b1",new AttributeValue {
                     S = isim
                 }},
                {":c1",new AttributeValue {
                     S = cinsiyet
                 }},
                {":d1",new AttributeValue {
                     N = yas
                 }},
                {":e1",new AttributeValue {
                     N = boy
                 }},
                {":f1",new AttributeValue {
                     N = kilo
                 }},
                {":g1",new AttributeValue {
                     S = foto_adi
                 }}
            },

                UpdateExpression = "SET #A = :a1 , #B = :b1 , #C = :c1 , #D = :d1 , #E = :e1 , #F = :f1 , #G = :g1",

                TableName = tableName,
                ReturnValues = "ALL_NEW"
            };
            var response = client.UpdateItem(request);

            
            var attributeList = response.Attributes;
        }

        

        private static void DeleteItem(String id)
        {
            var request = new DeleteItemRequest
            {
                TableName = tableName,
                Key = new Dictionary<string, AttributeValue>()
            {
                { "user_id", new AttributeValue {
                      N = id
                  } }
            },

                
                ReturnValues = "ALL_OLD"
            };

            var response = client.DeleteItem(request);

            
            var attributeList = response.Attributes;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            delete_photo(user_photo);
            pictureBox1.Image = null;
            textBox7.Text = "";
            DeleteItem(textBox8.Text);
            MessageBox.Show("Silme başarılı.");
            
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(button1.Text == "Güncelle")
            {
                
                this.Close();
            }
            else
            {
                Form1 frm1 = new Form1();
                this.Close();
                frm1.Show();
            }
            
        }


        public bool upload_photo(string localFilePath, string bucketName, string subDirectoryInBucket, string fileNameInS3)
        {
            
            IAmazonS3 s3client = new AmazonS3Client(RegionEndpoint.EUCentral1);

            
            TransferUtility utility = new TransferUtility(s3client);
            
            TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

            if (subDirectoryInBucket == "" || subDirectoryInBucket == null)
            {
                request.BucketName = bucketName;
            }
            else
            {
                request.BucketName = bucketName + @"/" + subDirectoryInBucket;
            }
            request.Key = fileNameInS3;
            request.FilePath = localFilePath;
            try
            {
                utility.Upload(request);
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public bool download_photo(string bucketName, string subDirectoryInBucket, string fileNameInS3)
        {

            IAmazonS3 s3client = new AmazonS3Client(RegionEndpoint.EUCentral1);


            TransferUtility utility = new TransferUtility(s3client);

            TransferUtilityOpenStreamRequest request = new TransferUtilityOpenStreamRequest();

            if (subDirectoryInBucket == "" || subDirectoryInBucket == null)
            {
                request.BucketName = bucketName;
            }
            else
            {
                request.BucketName = bucketName + @"/" + subDirectoryInBucket;
            }
            request.Key = fileNameInS3;

            System.IO.Stream downloaded_file = utility.OpenStream(request);
            pictureBox1.Image = new Bitmap(downloaded_file);
            return true;
        }

        public void delete_photo(String fileName)
        {
            IAmazonS3 s3client = new AmazonS3Client(RegionEndpoint.EUCentral1);
            DeleteObjectRequest request = new DeleteObjectRequest
            {
                BucketName = "bulutbilisimvize-2022" + @"/" + "fotograflar",
                Key = fileName
            };


            try
            {
                s3client.DeleteObject(request);
            }
            catch
            {
                int asdasd = 0;
            }
                
            
        }


        public String DosyaYolu = "";
        public String DosyaAdi = "";
        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            DosyaYolu = openFileDialog1.FileName;
            DosyaAdi = openFileDialog1.SafeFileName;
            if(DosyaYolu != "")
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = Image.FromFile(DosyaYolu);
            }
            
            textBox7.Text = DosyaAdi;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(user_photo != null && user_photo != "")
            {
                delete_photo(user_photo);
                pictureBox1.Image = null;
                textBox7.Text = "";
            }
            else
            {
                pictureBox1.Image = null;
                textBox7.Text = "";
            }
        }
    }
}
