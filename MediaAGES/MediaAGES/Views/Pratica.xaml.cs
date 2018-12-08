using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MediaAGES.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Pratica : ContentPage
	{
        double mediaFinal = 0.00, MediaFinalTeorica = 0, MediaFinalPratica = 0, NotaAluno50 = 0.00, notaAluno100 = 0.00,
             fichamento = 0.00, portifolio = 0.00, pu = 0.00,
             Notapratica = 0.00, pesoPratica = 0, pesoTeorica = 0, resultado, ResultadoTeorico, ResultadoPratico;

        decimal valor;

        public Pratica()
        {
            InitializeComponent();
            adMobView.AdUnitId = "ca-app-pub-3659475632008000/3995928793";
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(BoxNota50.Text) && (string.IsNullOrEmpty(BoxNota100.Text)) && (string.IsNullOrEmpty(BoxNotaFichamento.Text)) && (string.IsNullOrEmpty(BoxNotaPortifolio.Text)) && (string.IsNullOrEmpty(BoxNotaPU.Text)) && (string.IsNullOrEmpty(BoxNotaPratica.Text)))
                {
                    await DisplayAlert("Aviso", "Por favor preencha os campos obrigatorios.", "OK");
                    resultado = 0;
                }
                else if (string.IsNullOrEmpty(BoxNota50.Text) & (string.IsNullOrEmpty(BoxNotaFichamento.Text)) || (string.IsNullOrEmpty(BoxNota50.Text) & (string.IsNullOrEmpty(BoxNotaPU.Text))))
                {
                    await DisplayAlert("Aviso", "Por favor preencha os campos obrigatorios.", "OK");
                    resultado = 0;
                }
                else if ((string.IsNullOrEmpty(BoxNotaFichamento.Text) & (string.IsNullOrEmpty(BoxNota100.Text))))
                {
                    await DisplayAlert("Aviso", "Por favor preencha os campos obrigatorios.", "OK");
                    resultado = 0;
                }
                else if (string.IsNullOrEmpty(BoxNota50.Text) & (string.IsNullOrEmpty(BoxNotaPU.Text)) || (string.IsNullOrEmpty(BoxNotaFichamento.Text) & (string.IsNullOrEmpty(BoxNotaPU.Text))))
                {
                    await DisplayAlert("Aviso", "Por favor preencha os campos obrigatorios.", "OK");
                    resultado = 0;
                }
                else if (PickerTeorica.SelectedItem == null | PickerPratica.SelectedItem == null)
                {
                    await DisplayAlert("Ops!", "Preencha o peso da Avaliação Teórica.", "OK");
                    resultado = 0;
                }
                else if (!string.IsNullOrEmpty(BoxNotaFichamento.Text))
                {
                    try
                    {
                        resultado = Math.Round(CalcularMF(), 2);
                        ResultadoTeorico = Math.Round(MediaFinalTeorica, 2);
                        ResultadoPratico = Math.Round(MediaFinalPratica, 2);
                        if (resultado != 0)
                        {
                            var resp = await DisplayAlert("Resultado!", "Média Teórica: " + ResultadoTeorico
                            + Environment.NewLine + Environment.NewLine + "Média Prática: " + ResultadoPratico
                            + Environment.NewLine + Environment.NewLine + "Média Final: " + resultado.ToString(),
                            "OK", "Seguir");

                            if (resp)
                            {
                                LimparCampos();
                            }
                            else
                            {
                                LimparCampos();
                                Device.OpenUri(new Uri("https://www.instagram.com/luraxsoft/"));
                            }
                        }
                    }
                    catch (Exception)
                    {
                        await DisplayAlert("Ops!", "Preencha todos os campos.", "OK");
                    }

                }
                else
                {
                    try
                    {
                        resultado = Math.Round(CalcularMFSEMFICHAMENTO(), 2);
                        ResultadoTeorico = Math.Round(MediaFinalTeorica, 2);
                        ResultadoPratico = Math.Round(MediaFinalPratica, 2);
                        if (resultado != 0)
                        {
                            var resp = await DisplayAlert("Resultado! - NÃO REALIZOU O FICHAMENTO", "Média Teórica: " + ResultadoTeorico
                            + Environment.NewLine + Environment.NewLine + "Média Prática: " + ResultadoPratico
                            + Environment.NewLine + Environment.NewLine + "Média Final: " + resultado.ToString(),
                            "OK", "Seguir");
                            if (resp)
                            {
                                LimparCampos();
                            }
                            else
                            {
                                LimparCampos();
                                Device.OpenUri(new Uri("https://www.instagram.com/luraxsoft/"));
                            }
                        }
                    }
                    catch (Exception)
                    {
                        await DisplayAlert("Ops!", "Preencha todos os campos.", "OK");
                    }
                }
            }
            catch
            {
                await DisplayAlert("Ops!", "Preencha o peso da Avaliação Teórica.", "OK");
                resultado = 0;
            }
        }

        private void LimparCampos()
        {
            //BoxNota50.Text = "";
            //BoxNota100.Text = "";       
            //BoxNotaFichamento.Text = "";
            //BoxNotaPortifolio.Text = "";
            //BoxNotaPU.Text = "";

            //BoxNotaPratica.Text = "";
        }

        private double CalcularMF()
        {
            NotaAluno50 = Convert.ToDouble(BoxNota50.Text);
            notaAluno100 = Convert.ToDouble(BoxNota100.Text);
            portifolio = Convert.ToDouble(BoxNotaPortifolio.Text);
            fichamento = Convert.ToDouble(BoxNotaFichamento.Text);
            Notapratica = Convert.ToDouble(BoxNotaPratica.Text);
            pesoPratica = Convert.ToInt16(PickerPratica.SelectedItem);
            pesoTeorica = Convert.ToInt16(PickerTeorica.SelectedItem);
            pu = Convert.ToDouble(BoxNotaPU.Text);

            if (NotaAluno50 < notaAluno100)
            {
                MediaFinalTeorica = ((notaAluno100 * 0.8) + (fichamento * 0.1) + (portifolio * 0.1)) + pu;
                //Nota Teorica!
                MediaFinalTeorica = MediaFinalTeorica * (pesoTeorica / 10);

                MediaFinalPratica = Notapratica * (pesoPratica / 10);
                //Nota Pratica e media final recebendo a soma de tudo!
                mediaFinal = MediaFinalTeorica + MediaFinalPratica;

            }
            //Se a nota de 50% nao for menor que a nota de 100% então faça
            else
            {
                //Regressão
                MediaFinalTeorica = ((NotaAluno50 + notaAluno100) / 2 * 0.8) + (fichamento * 0.1) + (portifolio * 0.1) + pu;
                // Nota Teorica
                MediaFinalTeorica = MediaFinalTeorica * (pesoTeorica / 10);

                MediaFinalPratica = Notapratica * (pesoPratica / 10);
                //Nota Pratica e media final recebendo a soma de tudo!
                mediaFinal = MediaFinalTeorica + MediaFinalPratica;
            }

            if (mediaFinal > 10)
            {
                return mediaFinal = 10;
            }

            return mediaFinal;
        }

        private void Observacoes_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Observações", "1. Caso não tenha feito o 'FICHAMENTO' deixe-o em branco."
               + Environment.NewLine + Environment.NewLine + "2. A virgula será adicionada na medida que você for digitando."
               + Environment.NewLine + Environment.NewLine + "3. O peso do Fichameto e Portifolio é '1'."
               + Environment.NewLine + Environment.NewLine + "4. Este calculo Prático só se aplica nas seguintes condições."
               + Environment.NewLine + Environment.NewLine + " 4.1 Quando a Produção Única se aplica junto a Prática."
               + Environment.NewLine + Environment.NewLine + " 4.2 Fiz a prova de 50%."
               + Environment.NewLine + Environment.NewLine + " 4.3 Fiz a prova de 100%."
               + Environment.NewLine + Environment.NewLine + " 4.4 Fiz o Portifólio."
               + Environment.NewLine + Environment.NewLine + " 4.5 Fiz a Produção Única."
               , "OK");
        }

        private double CalcularMFSEMFICHAMENTO()
        {
            NotaAluno50 = Convert.ToDouble(BoxNota50.Text);
            notaAluno100 = Convert.ToDouble(BoxNota100.Text);
            portifolio = Convert.ToDouble(BoxNotaPortifolio.Text);
            Notapratica = Convert.ToDouble(BoxNotaPratica.Text);
            pesoPratica = Convert.ToInt16(PickerPratica.SelectedItem);
            pesoTeorica = Convert.ToInt16(PickerTeorica.SelectedItem);
            pu = Convert.ToDouble(BoxNotaPU.Text);


            if (NotaAluno50 < notaAluno100)
            {
                MediaFinalTeorica = ((notaAluno100 * 0.8) + (portifolio * 0.1)) + pu / 2;
                //Nota Teorica!
                MediaFinalTeorica = MediaFinalTeorica * (pesoTeorica / 10);
                //Recebendo o resultado em uma variavel
                MediaFinalPratica = Notapratica * (pesoPratica / 10);
                //Nota Pratica e media final recebendo a soma de tudo!
                mediaFinal = MediaFinalTeorica + MediaFinalPratica;
            }
            //Se a nota de 50% nao for menor que a nota de 100% então faça
            else
            {
                //Regressão
                MediaFinalTeorica = ((NotaAluno50 + notaAluno100) / 2 * 0.8) + (portifolio * 0.1) + pu / 2;
                // Nota Teorica
                MediaFinalTeorica = MediaFinalTeorica * (pesoTeorica / 10);
                //Recebendo o resultado em uma variavel
                MediaFinalPratica = Notapratica * (pesoPratica / 10);
                //Nota Pratica e media final recebendo a soma de tudo!
                mediaFinal = MediaFinalTeorica + MediaFinalPratica;
            }

            if (mediaFinal > 10)
            {
                //MessageBox.Show("Sua Média passou de '10', Parabéns!");
                return mediaFinal = 10;
            }

            return mediaFinal;
        }

        private string txt = "";

        private void BoxNotaPratica_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;

            if (!string.IsNullOrWhiteSpace(entry.Text))
            {
                try
                {
                    double texto = Convert.ToDouble(entry.Text);

                    if (texto == 0)
                    {
                        var ev = e as TextChangedEventArgs;
                        if (ev.NewTextValue != ev.OldTextValue)
                        {
                            string text = Regex.Replace(e.NewTextValue, @"[^0-9]", "");

                            if (text.Length > 3)
                                text = text.Remove(3);

                            double Texto = Convert.ToDouble(entry.Text);

                            if (text.Length > 1)
                            {
                                text = text.Insert(1, ",");
                            }

                            if (entry.Text != text)
                                entry.Text = text;
                        }
                    }
                    else
                    {
                        try
                        {
                            txt = entry.Text.Replace(",", "").Replace(".", "");
                            if (txt.Equals(""))
                            {
                                entry.Text = "";
                            }
                            if (texto > 10)
                            {
                                txt = txt.Remove(3);
                            }
                            //txt = txt.PadLeft(3, '0');
                            if (txt.Length > 3 & txt.Substring(0, 1) == "0")
                                txt = txt.Substring(1, txt.Length - 1);
                            valor = Convert.ToDecimal(txt) / 100;
                            entry.Text = string.Format("{0:N}", valor);

                        }
                        catch (Exception)
                        {
                            string.IsNullOrEmpty(entry.Text);
                        }
                    }

                }
                catch
                {
                    string.IsNullOrEmpty(entry.Text);
                }
            }
            else
            {
                string.IsNullOrEmpty(entry.Text);
            }
        }

        private void BoxNotaPU_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;

            if (!string.IsNullOrEmpty(entry.Text))
            {
                try
                {
                    double numer = Convert.ToDouble(entry.Text);

                    txt = entry.Text.Replace(".", "").Replace(",", "");
                    if (numer > 10)
                    {
                        txt = txt.Remove(3);
                    }
                    else if (valor > 2)
                    {
                        txt = txt.Remove(txt.Length - 1);
                    }

                    if (txt.Length > 3 & txt.Substring(0, 1) == "0")
                        txt = txt.Substring(1, txt.Length - 1);

                    valor = Convert.ToDecimal(txt) / 100;

                    if (valor != 0)
                    {
                        entry.Text = string.Format("{0:N}", valor);
                    }
                    else
                    {
                        //textBlock.Text = txt;
                    }

                }
                catch (Exception)
                {

                }
            }
            else
            {
                string.IsNullOrEmpty(entry.Text);
            }
        }

        private void BoxNotaPortifolio_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;

            if (!string.IsNullOrWhiteSpace(entry.Text))
            {
                try
                {
                    double texto = Convert.ToDouble(entry.Text);

                    if (texto == 0)
                    {
                        var ev = e as TextChangedEventArgs;
                        if (ev.NewTextValue != ev.OldTextValue)
                        {
                            string text = Regex.Replace(e.NewTextValue, @"[^0-9]", "");

                            if (text.Length > 3)
                                text = text.Remove(3);

                            double Texto = Convert.ToDouble(entry.Text);

                            if (text.Length > 1)
                            {
                                text = text.Insert(1, ",");
                            }

                            if (entry.Text != text)
                                entry.Text = text;
                        }
                    }
                    else
                    {
                        try
                        {
                            txt = entry.Text.Replace(",", "").Replace(".", "");
                            if (txt.Equals(""))
                            {
                                entry.Text = "";
                            }
                            if (texto > 10)
                            {
                                txt = txt.Remove(3);
                            }
                            //txt = txt.PadLeft(3, '0');
                            if (txt.Length > 3 & txt.Substring(0, 1) == "0")
                                txt = txt.Substring(1, txt.Length - 1);
                            valor = Convert.ToDecimal(txt) / 100;
                            entry.Text = string.Format("{0:N}", valor);

                        }
                        catch (Exception)
                        {
                            string.IsNullOrEmpty(entry.Text);
                        }
                    }

                }
                catch
                {
                    string.IsNullOrEmpty(entry.Text);
                }
            }
            else
            {
                string.IsNullOrEmpty(entry.Text);
            }
        }

        private void BoxNotaFichamento_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;

            if (!string.IsNullOrWhiteSpace(entry.Text))
            {
                try
                {
                    double texto = Convert.ToDouble(entry.Text);

                    if (texto == 0)
                    {
                        var ev = e as TextChangedEventArgs;
                        if (ev.NewTextValue != ev.OldTextValue)
                        {
                            string text = Regex.Replace(e.NewTextValue, @"[^0-9]", "");

                            if (text.Length > 3)
                                text = text.Remove(3);

                            double Texto = Convert.ToDouble(entry.Text);

                            if (text.Length > 1)
                            {
                                text = text.Insert(1, ",");
                            }

                            if (entry.Text != text)
                                entry.Text = text;
                        }
                    }
                    else
                    {
                        try
                        {
                            txt = entry.Text.Replace(",", "").Replace(".", "");
                            if (txt.Equals(""))
                            {
                                entry.Text = "";
                            }
                            if (texto > 10)
                            {
                                txt = txt.Remove(3);
                            }
                            //txt = txt.PadLeft(3, '0');
                            if (txt.Length > 3 & txt.Substring(0, 1) == "0")
                                txt = txt.Substring(1, txt.Length - 1);
                            valor = Convert.ToDecimal(txt) / 100;
                            entry.Text = string.Format("{0:N}", valor);

                        }
                        catch (Exception)
                        {
                            string.IsNullOrEmpty(entry.Text);
                        }
                    }

                }
                catch
                {
                    string.IsNullOrEmpty(entry.Text);
                }
            }
            else
            {
                string.IsNullOrEmpty(entry.Text);
            }
        }

        private void BoxNota100_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;

            if (!string.IsNullOrWhiteSpace(entry.Text))
            {
                try
                {
                    double texto = Convert.ToDouble(entry.Text);

                    if (texto == 0)
                    {
                        var ev = e as TextChangedEventArgs;
                        if (ev.NewTextValue != ev.OldTextValue)
                        {
                            string text = Regex.Replace(e.NewTextValue, @"[^0-9]", "");

                            if (text.Length > 3)
                                text = text.Remove(3);

                            double Texto = Convert.ToDouble(entry.Text);

                            if (text.Length > 1)
                            {
                                text = text.Insert(1, ",");
                            }

                            if (entry.Text != text)
                                entry.Text = text;
                        }
                    }
                    else
                    {
                        try
                        {
                            txt = entry.Text.Replace(",", "").Replace(".", "");
                            if (txt.Equals(""))
                            {
                                entry.Text = "";
                            }
                            if (texto > 10)
                            {
                                txt = txt.Remove(3);
                            }
                            //txt = txt.PadLeft(3, '0');
                            if (txt.Length > 3 & txt.Substring(0, 1) == "0")
                                txt = txt.Substring(1, txt.Length - 1);
                            valor = Convert.ToDecimal(txt) / 100;
                            entry.Text = string.Format("{0:N}", valor);

                        }
                        catch (Exception)
                        {
                            string.IsNullOrEmpty(entry.Text);
                        }
                    }

                }
                catch
                {
                    string.IsNullOrEmpty(entry.Text);
                }
            }
            else
            {
                string.IsNullOrEmpty(entry.Text);
            }
        }

        private void BoxNota50_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;

            if (!string.IsNullOrWhiteSpace(entry.Text))
            {
                try
                {
                    double texto = Convert.ToDouble(entry.Text);

                    if (texto == 0)
                    {
                        var ev = e as TextChangedEventArgs;
                        if (ev.NewTextValue != ev.OldTextValue)
                        {
                            string text = Regex.Replace(e.NewTextValue, @"[^0-9]", "");

                            if (text.Length > 3)
                                text = text.Remove(3);

                            double Texto = Convert.ToDouble(entry.Text);

                            if (text.Length > 1)
                            {
                                text = text.Insert(1, ",");
                            }

                            if (entry.Text != text)
                                entry.Text = text;
                        }
                    }
                    else
                    {
                        try
                        {
                            txt = entry.Text.Replace(",", "").Replace(".", "");
                            if (txt.Equals(""))
                            {
                                entry.Text = "";
                            }
                            if (texto > 10)
                            {
                                txt = txt.Remove(3);
                            }
                            //txt = txt.PadLeft(3, '0');
                            if (txt.Length > 3 & txt.Substring(0, 1) == "0")
                                txt = txt.Substring(1, txt.Length - 1);
                            valor = Convert.ToDecimal(txt) / 100;
                            entry.Text = string.Format("{0:N}", valor);

                        }
                        catch (Exception)
                        {
                            string.IsNullOrEmpty(entry.Text);
                        }
                    }

                }
                catch
                {
                    string.IsNullOrEmpty(entry.Text);
                }
            }
            else
            {
                string.IsNullOrEmpty(entry.Text);
            }
        }
    }
}