using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SliderListaApp.Model;
using Xamarin.Forms;

namespace SliderListaApp
{
    public partial class MainPage : ContentPage
    {
        ArrayList lista = new ArrayList()
        {
            "AC","AL","AM","AP",
            "BA","CE","DF","ES",
            "GO","MA","MG","MS",
            "MT","PA","PB","PE",
            "PI","PR","RJ","RN",
            "RO","RR","RS","SC",
                "SE","SP","TO"
        };

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            //string uf = lbl_uf.Text.ToUpper();

            var client = new HttpClient();
            var json = await client.GetStringAsync($"http://ibge.herokuapp.com/municipio/?val={label_uf}");

            JObject municipios = JObject.Parse(json);

            Dictionary<string, string> dadosMunicipios = municipios.ToObject<Dictionary<string, string>>();

           //ArrayList lista = new ArrayList();

            List<Municipios> municipiolist = new List<Municipios>();

            foreach(KeyValuePair<string, string> municipio in dadosMunicipios)
            {
                Municipios munic = new Municipios();

                munic.nome = municipio.Key;
                munic.codigo = municipio.Value;

                municipiolist.Add(munic);
            }

            //listaMunicipios.ItemsSource = municipiolist;

        }

        void Handle_ValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            int posicao = Convert.ToInt32(slider_val.Value);

            string val_label = lista[posicao].ToString();

            label_uf.Text = val_label;
        }

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
