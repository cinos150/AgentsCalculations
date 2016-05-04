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
using Newtonsoft.Json.Linq;

namespace AgentsSystemsTable
{
    public partial class Form1 : Form
    {

        enum discritctsEnum
        {
                 Bemowo
                , Białołęka
                , Bielany
                , Mokotów
                , Ochota
                , Praga_Południe
                , Praga_Północ
                , Rembertów
                , Śródmieście
                , Targówek
                , Ursus
                , Ursynów
                , Wawer
                , Wesoła
                , Wilanów
                , Włochy
                , Wola
                , Żoliborz
        };


        public Form1()
        {
            InitializeComponent();


            DataTable table = new DataTable();
            table.Columns.Add("Dosage", typeof(int));
            table.Columns.Add("Drug", typeof(string));
            table.Columns.Add("Patient", typeof(string));
            table.Columns.Add("Date", typeof(DateTime));

            // Here we add five DataRows.
            table.Rows.Add(25, "Indocin", "David", DateTime.Now);
            table.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
            table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
            table.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
            table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);



            Get();

        }
        

        public async void  Get()
        {

          

            string[] discritcts =
            {
                "Bemowo"
                , "Białołęka"
                , "Bielany"
                , "Mokotów"
                , "Ochota"
                , "Praga Południe"
                , "Praga Północ"
                , "Rembertów"
                , "Śródmieście"
                , "Targówek"
                , "Ursus"
                , "Ursynów"
                , "Wawer"
                , "Wesoła"
                , "Wilanów"
                , "Włochy"
                , "Wola"
                , "Żoliborz"
            };

            DataTable table = new DataTable();

            string wlochyPlaceID = "ChIJx8B2TmIzGUcRbPdFEAvxNCo";

            int [,] districtTable = new int[18,18];


            table.Columns.Add("Dist names", typeof(string));

            for (int i = 0; i < discritcts.Length; i++)
            {
               

                    table.Columns.Add(discritcts[i], typeof(int));
                
            }


         



            for (int i = 0; i < discritcts.Length; i++)
            {


                table.Rows.Add();
                table.Rows[i][0] = discritcts[i];

                for (int j = 0; j < discritcts.Length; j++)
                {
                   

                    if ((int) discritctsEnum.Włochy == j)
                        discritcts[j] = "place_id:"+wlochyPlaceID;
                     else if((int)discritctsEnum.Włochy == i)
                        discritcts[i] = "place_id:" + wlochyPlaceID;

                    
                   

                    if (j == i)
                    {
                        table.Rows.Add();
                        table.Rows[i][j+1] = 0;
                        continue;
                    }
                    
                    var json = "https://maps.googleapis.com/maps/api/directions/json?traffic_mode=pessimistic&" +
                                  "transit_mode=train|bus|subway" +
                                  "&origin=" + discritcts[i] +
                                  "&destination=" + discritcts[j]+
                                  "&key=AIzaSyCDOBCTduKjHKSDJSGh4A2ZkVkD8vxTg9g";

                    var result = await GetExternalResponse(json);
                    dynamic d = JObject.Parse(result);
                    table.Rows.Add();
                   
                    table.Rows[i][j+1] = (int)d.routes[0].legs[0].duration.value;



                    dataGridView1.DataSource = table;
                    //Console.WriteLine("From: " + discritcts[i] + " -> " + discritcts[j]);
                    //Console.WriteLine(d.routes[0].legs[0].duration.text);
                    //Console.WriteLine(d.routes[0].legs[0].duration.value);
                    //Console.WriteLine();
                }
            }
        

      
        }

        private async Task<string> GetExternalResponse(string address)
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(address);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
        //2035 // 34 min 
        // bielany wawer


    }
}
