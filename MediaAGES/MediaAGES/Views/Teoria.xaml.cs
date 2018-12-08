using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MediaAGES.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Teoria : ContentPage
	{
        double mediaFinal = 0.00, NotaAluno50 = 0.00, notaAluno100 = 0.00, fichamento = 0.00, portifolio = 0.00, pu = 0.00;
        decimal valor;

        public Teoria()
        {
            InitializeComponent();
            MediaAGESTeorica.AdUnitId = "ca-app-pub-3659475632008000/7960166481";
        }

        private void Observacoes_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Observações", "1. Caso não tenha feito o 'FICHAMENTO' deixe-o em branco. (P.U / 2)"
                + Environment.NewLine + Environment.NewLine + "2. Caso não tenha feito a 'Prova de 50%' deixe-o em branco. (100% / 2)"
                + Environment.NewLine + Environment.NewLine + "3. Caso não tenha feito a 'Produção Única' deixe-o em branco. (-2 na Média)"
                + Environment.NewLine + Environment.NewLine + "4. A virgula será adicionada na medida que voce for digitando."
                + Environment.NewLine + Environment.NewLine + "5. O fichamento, e Portifolio, deverá ser preenchido, de acordo com o método, Ex. 10 . 8,00 . 7,4. O aplicativo fará o calculo automaticamente."
                , "OK");
        }

        private void LimparCampos()
        {
            //BoxNota50.Text = "";
            //NotaAluno50 = 0;
            //BoxNota100.Text = "";
            //BoxNotaFichamento.Text = "";
            //BoxNotaPortifolio.Text = "";
            //BoxNotaPU.Text = "";
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(BoxNota50.Text) && (string.IsNullOrEmpty(BoxNota100.Text)) && (string.IsNullOrEmpty(BoxNotaFichamento.Text)) && (string.IsNullOrEmpty(BoxNotaPortifolio.Text)) && (string.IsNullOrEmpty(BoxNotaPU.Text)))
            {
                await DisplayAlert("Aviso", "Por favor preencha os campos obrigatorios.", "OK");
                mediaFinal = 0;
            }
            else if (string.IsNullOrEmpty(BoxNota50.Text) & (string.IsNullOrEmpty(BoxNotaFichamento.Text)) || (string.IsNullOrEmpty(BoxNota50.Text) & (string.IsNullOrEmpty(BoxNotaPU.Text))))
            {
                await DisplayAlert("Aviso", "Por favor preencha os campos obrigatorios.", "OK");
                mediaFinal = 0;
            }
            else if ((string.IsNullOrEmpty(BoxNotaFichamento.Text) & (string.IsNullOrEmpty(BoxNota100.Text))))
            {
                await DisplayAlert("Aviso", "Por favor preencha os campos obrigatorios.", "OK");
                mediaFinal = 0;
            }
            else if (string.IsNullOrEmpty(BoxNota50.Text) & (string.IsNullOrEmpty(BoxNotaPU.Text)) || (string.IsNullOrEmpty(BoxNotaFichamento.Text) & (string.IsNullOrEmpty(BoxNotaPU.Text))))
            {
                await DisplayAlert("Aviso", "Por favor preencha os campos obrigatorios.", "OK");
                mediaFinal = 0;
            }
            else
            {
                if (string.IsNullOrEmpty(BoxNotaPU.Text) & (!string.IsNullOrEmpty(BoxNota50.Text)) & (!string.IsNullOrEmpty(BoxNota100.Text) & (!string.IsNullOrEmpty(BoxNotaFichamento.Text)) & (!string.IsNullOrEmpty(BoxNotaPortifolio.Text))))
                {
                    try
                    {
                        Double resultado = Math.Round(CalcularMFSEMPU(-2), 2);

                        if (resultado != 0)
                        {
                            await DisplayAlert("Aviso!", "Você não informou sua nota da Produção Única, portanto você não a realizou, sua média será diminuida por -2.", "OK");
                            var resp = await DisplayAlert("Resultado! - NÃO REALIZOU A P.U.", "Sua média é: " + resultado.ToString(), "OK", "Seguir");
                            if (resp)
                            {
                                LimparCampos();
                            }
                            else
                            {
                                LimparCampos();
                                Device.OpenUri(new Uri("https://www.instagram.com/jadsonxsantos/"));
                            }
                        }
                    }
                    catch
                    {
                        mediaFinal = 0;
                    }
                }

                if (string.IsNullOrEmpty(BoxNota50.Text) & (!string.IsNullOrEmpty(BoxNota100.Text)) & (!string.IsNullOrEmpty(BoxNotaFichamento.Text) & (!string.IsNullOrEmpty(BoxNotaPortifolio.Text) & (!string.IsNullOrEmpty(BoxNotaPU.Text)))))
                {
                    try
                    {
                        Double resultado = Math.Round(CalcularMFSEMNOTA50(), 2);

                        if (resultado != 0)
                        {
                            await DisplayAlert("Aviso!", "Você não informou sua nota de 50%, portanto você não a realizou, sua nota de 100% será dividida por 2.", "OK");
                            var resp = await DisplayAlert("Resultado! - NÃO REALIZOU A NOTA DE 50%", "Sua mé dia é: " + resultado.ToString(), "OK", "Seguir");
                            if (resp)
                            {
                                LimparCampos();
                            }
                            else
                            {
                                LimparCampos();
                                Device.OpenUri(new Uri("https://www.instagram.com/jadsonxsantos/"));
                            }
                        }
                    }
                    catch
                    {
                        mediaFinal = 0;
                    }
                }

                if (!string.IsNullOrEmpty(BoxNota50.Text) & (!string.IsNullOrEmpty(BoxNota100.Text) & (!string.IsNullOrEmpty(BoxNotaFichamento.Text) & (!string.IsNullOrEmpty(BoxNotaPortifolio.Text) & (!string.IsNullOrEmpty(BoxNotaPU.Text))))))
                {
                    try
                    {
                        Double resultado = Math.Round(CalcularMF(), 2);

                        if (resultado != 0)
                        {
                            var resp = await DisplayAlert("Resultado!", "Sua média é: " + resultado.ToString(), "OK", "Seguir");
                            if (resp)
                            {
                                LimparCampos();
                            }
                            else
                            {
                                LimparCampos();
                                Device.OpenUri(new Uri("https://www.instagram.com/jadsonxsantos/"));
                            }
                        }
                    }
                    catch (Exception)
                    {
                        mediaFinal = 0;
                    }
                }

                if (string.IsNullOrEmpty(BoxNotaFichamento.Text) & (!string.IsNullOrEmpty(BoxNota50.Text) & (!string.IsNullOrEmpty(BoxNota100.Text) & (!string.IsNullOrEmpty(BoxNotaPortifolio.Text) & (!string.IsNullOrEmpty(BoxNotaPU.Text))))))
                {
                    try
                    {
                        Double resultado = Math.Round(CalcularMFSEMFICHAMENTO(), 2);

                        if (resultado != 0)
                        {
                            await DisplayAlert("Aviso!", "Você não informou o fichamento, portanto você não o realizou, sua P.U será dividida por 2.", "OK");
                            var resp = await DisplayAlert("Resultado - NÃO REALIZOU O FICHAMENTO!", "Sua média é: " + resultado.ToString(), "OK", "Seguir");
                            if (resp)
                            {
                                LimparCampos();
                            }
                            else
                            {
                                LimparCampos();
                                Device.OpenUri(new Uri("https://www.instagram.com/jadsonxsantos/"));
                            }
                        }
                    }
                    catch (Exception)
                    {
                        mediaFinal = 0;

                    }
                }

            }
        }

        private double CalcularMFSEMNOTA50()
        {
            notaAluno100 = Convert.ToDouble(BoxNota100.Text);
            portifolio = Convert.ToDouble(BoxNotaPortifolio.Text);
            fichamento = Convert.ToDouble(BoxNotaFichamento.Text);
            pu = Convert.ToDouble(BoxNotaPU.Text);

            if (notaAluno100 > 10)
            {
                BoxNota100.Focus();
                DisplayAlert("Ops!", "A nota de 100% Só vale de 1 a 10.", "OK");
                mediaFinal = 0;
            }
            if (fichamento > 10)
            {
                BoxNotaFichamento.Focus();
                DisplayAlert("Ops!", "O Fichamento só Vale de 0 a 10.", "OK");
                mediaFinal = 0;
            }
            if (portifolio > 10)
            {
                BoxNotaPortifolio.Focus();
                DisplayAlert("Ops!", "O portifolio Só vale de 1 a 10.", "OK");
                mediaFinal = 0;
            }
            if (pu > 2)
            {
                BoxNotaPU.Focus();
                DisplayAlert("Ops!", "A P.U. só vale até (2) e você digitou, " + pu, "OK");
                mediaFinal = 0;
            }

            mediaFinal = (notaAluno100 * 0.8) / 2 + (fichamento * 0.1) + (portifolio * 0.1) + pu;


            return mediaFinal;
        }

        private double CalcularMFSEMPU(Double valorPU)
        {
            NotaAluno50 = Convert.ToDouble(BoxNota50.Text);
            notaAluno100 = Convert.ToDouble(BoxNota100.Text);
            portifolio = Convert.ToDouble(BoxNotaPortifolio.Text);
            fichamento = Convert.ToDouble(BoxNotaFichamento.Text);

            if (NotaAluno50 > 10)
            {
                BoxNota50.Focus();
                DisplayAlert("Atenção", "A Nota de 50% Só vale de 1 a 10.", "OK");
                mediaFinal = 0;
            }
            if (notaAluno100 > 10)
            {
                BoxNota100.Focus();
                DisplayAlert("Ops!", "A nota de 100% Só vale de 1 a 10.", "OK");
                mediaFinal = 0;
            }
            if (fichamento > 10)
            {
                BoxNotaFichamento.Focus();
                DisplayAlert("Ops!", "O Fichamento só Vale de 0 a 10.", "OK");
                mediaFinal = 0;
            }
            if (portifolio > 10)
            {
                BoxNotaPortifolio.Focus();
                DisplayAlert("Ops!", "O portifolio Só vale de 1 a 10.", "OK");
                mediaFinal = 0;
            }

            // usando o Se (if) para quando a nota de 50% for maior que a de 100% converter automaticamente para a regressão.
            if (NotaAluno50 < notaAluno100)
            {
                mediaFinal = (notaAluno100 * 0.8) + (fichamento * 0.1) + (portifolio * 0.1) + valorPU;

            }
            else
            {
                //Regressão
                mediaFinal = ((NotaAluno50 + notaAluno100) / 2 * 0.8 + (fichamento * 0.1) + (portifolio * 0.1)) + valorPU;
                // se a media for maior ou igual a 7 então escrever Aprovado.              
            }

            if (mediaFinal > 10)
            {
                //MessageBox.Show("Sua Média passou de '10', Parabéns!");
                return mediaFinal = 10;
            }

            return mediaFinal;
        }

        private double CalcularMFSEMFICHAMENTO()
        {
            NotaAluno50 = Convert.ToDouble(BoxNota50.Text);
            notaAluno100 = Convert.ToDouble(BoxNota100.Text);
            portifolio = Convert.ToDouble(BoxNotaPortifolio.Text);
            pu = Convert.ToDouble(BoxNotaPU.Text);

            if (NotaAluno50 > 10)
            {
                BoxNota50.Focus();
                DisplayAlert("Atenção", "A Nota de 50% Só vale de 1 a 10.", "OK");
                mediaFinal = 0;
            }
            if (notaAluno100 > 10)
            {
                BoxNota100.Focus();
                DisplayAlert("Ops!", "A nota de 100% Só vale de 1 a 10.", "OK");
                mediaFinal = 0;
            }
            if (portifolio > 10)
            {
                BoxNotaPortifolio.Focus();
                DisplayAlert("Ops!", "O portifolio Só vale de 1 a 10.", "OK");
                mediaFinal = 0;
            }
            if (pu > 2)
            {
                BoxNotaPU.Focus();
                DisplayAlert("Ops!", "A P.U. só vale até (2) e você digitou, " + pu, "OK");
                mediaFinal = 0;
            }
            // usando o Se (if) para quando a nota de 50% for maior que a de 100% converter automaticamente para a regressão.
            if (NotaAluno50 < notaAluno100)
            {
                mediaFinal = (notaAluno100 * 0.8) + (portifolio * 0.1) + pu / 2;

                // se a media final for maior ou igual a 7 escrever aprovado senão reprovado.
                if (mediaFinal >= 7)
                {
                    //situa.Text = "Aprovado(a)";
                    //situa.Foreground = new SolidColorBrush(Colors.Green);
                }
            }
            else
            {
                //Regressão
                mediaFinal = ((NotaAluno50 + notaAluno100) / 2 * 0.8 + (portifolio * 0.1)) + pu / 2;
                // se a media for maior ou igual a 7 então escrever Aprovado.
                if (mediaFinal >= 7)
                {
                    //situa.Text = "Aprovado :)";
                    //situa.Foreground = new SolidColorBrush(Colors.Green);
                }
            }

            if (mediaFinal > 10)
            {
                //MessageBox.Show("Sua Média passou de '10', Parabéns!");
                //return mediaFinal = 10;
            }

            return mediaFinal;
        }

        private double CalcularMFSEMPortiFolio()
        {
            NotaAluno50 = Convert.ToDouble(BoxNota50.Text);
            notaAluno100 = Convert.ToDouble(BoxNota100.Text);
            pu = Convert.ToDouble(BoxNotaPU.Text);

            if (NotaAluno50 > 10)
            {
                BoxNota50.Focus();
                DisplayAlert("Ops!", "A Nota de 50% Só vale de 0 a 10.", "OK");
                return mediaFinal = 0;
            }
            if (notaAluno100 > 10)
            {
                BoxNota100.Focus();
                DisplayAlert("Ops!", "A nota de 100% Só vale de 0 a 10.", "OK");
                return mediaFinal = 0;
            }
            if (fichamento > 10)
            {
                BoxNotaFichamento.Focus();
                DisplayAlert("Ops!", "O Fichamento só Vale de 0 a 10.", "OK");
                return mediaFinal = 0;
            }
            if (portifolio > 10)
            {
                BoxNotaPortifolio.Focus();
                DisplayAlert("Ops!", "O portifolio Só vale de 0 a 10.", "OK");
                return mediaFinal = 0;
            }
            if (pu > 2)
            {
                BoxNotaPU.Focus();
                DisplayAlert("Ops!", "A P.U. só vale até (2) e você digitou, " + pu + ". Caso você não tenha feito ou zerado deixe-o em branco.", "OK");
                return mediaFinal = 0;
            }
            // usando o Se (if) para quando a nota de 50% for maior que a de 100% converter automaticamente para a regressão.
            if (NotaAluno50 < notaAluno100)
            {
                mediaFinal = (notaAluno100 * 0.8) + (portifolio * 0.1) + pu / 2;

                // se a media final for maior ou igual a 7 escrever aprovado senão reprovado.
                if (mediaFinal >= 7)
                {
                    //situa.Text = "Aprovado(a)";
                    //situa.Foreground = new SolidColorBrush(Colors.Green);
                }
                //se a media for menor que 7 então calcula-se quando precisa na PF final.
                else
                {
                    //Calculo para saber quanto precisa na final.
                    //pf = ((mediaFinal * 7) - 50) / -3;
                    //situa.Text = "Sua média foi " + mediaFinal + ", você precisa de (" + Math.Round(pf, 2) + ") na avaliação final.";
                    //situa.Foreground = new SolidColorBrush(Colors.Green);
                }
            }
            else
            {
                //Regressão
                mediaFinal = ((NotaAluno50 + notaAluno100) / 2 * 0.8 + (portifolio * 0.1)) + pu / 2;
                // se a media for maior ou igual a 7 então escrever Aprovado.
                if (mediaFinal >= 7)
                {
                    //situa.Text = "Aprovado :)";
                    //situa.Foreground = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    //pf = ((mediaFinal * 7) - 50) / -3;
                    //situa.Text = "Sua média foi " + mediaFinal + ", você precisa de (" + Math.Round(pf, 2) + ") na avaliação final.";
                    //situa.Foreground = new SolidColorBrush(Colors.Green);
                }
            }

            if (mediaFinal > 10)
            {
                //MessageBox.Show("Sua Média passou de '10', Parabéns!");
                //return mediaFinal = 10;
            }

            return mediaFinal;
        }

        private double CalcularMF()
        {
            NotaAluno50 = Convert.ToDouble(BoxNota50.Text);
            notaAluno100 = Convert.ToDouble(BoxNota100.Text);
            portifolio = Convert.ToDouble(BoxNotaPortifolio.Text);
            fichamento = Convert.ToDouble(BoxNotaFichamento.Text);
            pu = Convert.ToDouble(BoxNotaPU.Text);

            // usando o Se (if) para quando a nota de 50% for maior que a de 100% converter automaticamente para a regressão.
            if (NotaAluno50 < notaAluno100)
            {
                mediaFinal = (notaAluno100 * 0.8) + (fichamento * 0.1) + (portifolio * 0.1) + pu;


            }
            else
            {
                //Regressão
                mediaFinal = ((NotaAluno50 + notaAluno100) / 2 * 0.8 + (fichamento * 0.1) + (portifolio * 0.1)) + pu;

            }

            if (mediaFinal > 10)
            {
                //MessageBox.Show("Sua Média passou de '10', Parabéns!");
                return mediaFinal = 10;
            }

            return mediaFinal;
        }


        private string txt = "";

        private void Mascara(Entry entry)
        {
            //var entry = (Entry)sender;

            if (!string.IsNullOrEmpty(entry.Text))
            {
                try
                {
                    double texto = Convert.ToDouble(entry.Text);

                    if (texto == 0)
                    {

                        string text = Regex.Replace(txt, @"[^0-9]", "");

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
                    else
                    {
                        try
                        {
                            txt = entry.Text.Replace(",", "").Replace(".", "");
                            if (texto > 10)
                            {
                                txt = txt.Remove(3);
                            }
                            txt = txt.PadLeft(3, '0');
                            if (txt.Length > 3 & txt.Substring(0, 1) == "0")
                                txt = txt.Substring(1, txt.Length - 1);
                            valor = Convert.ToDecimal(txt) / 100;
                            entry.Text = string.Format("{0:N}", valor);

                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                        }
                    }

                }
                catch
                {
                    entry.Text = "";
                }
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
                catch (Exception ex)
                {

                }
            }
            else
            {
                string.IsNullOrEmpty(entry.Text);
            }
        }
    }
}