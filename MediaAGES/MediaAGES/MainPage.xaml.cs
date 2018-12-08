using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MediaAGES
{
	public partial class MainPage : TabbedPage
    {
		public MainPage()
		{
			InitializeComponent();
            adMobView.AdUnitId = "ca-app-pub-3659475632008000/9740724198";
        }

        private async void CalcularMediaTeorica_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.Teoria());          
        }

        private async void CalcularMediaPratica_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.Pratica());
        }

        private void Pesquisa_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.instagram.com/luraxsoft/"));
        }

        private void FeedBack_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.instagram.com/jadsonxsantos/"));
        }      

        private void Compartilhar_Clicked(object sender, EventArgs e)
        {
            string MsgShare = "Baixe agora o app MédiaAGES e faça seus cálculos de média do UniAGES." + 
                Environment.NewLine + "https://play.google.com/store/apps/details?id=com.LURASOFT.MediaAGES&hl=pt_BR";
            Device.OpenUri(new Uri("https://api.whatsapp.com/send?text=" + MsgShare));            
        }
    }
}
