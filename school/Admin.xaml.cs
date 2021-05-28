using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace school
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        List<Service> ServiswList1 = BD.CE.Service.ToList();
        List<Service> ServiswList = new List<Service>();

        public Admin()
        {
            InitializeComponent();
            ServiswList = ServiswList1;
            DGServises.ItemsSource = ServiswList;
            CBPeople.ItemsSource = BD.CE.Client.ToList();
            CBPeople.SelectedValuePath = "ID";
            CBPeople.DisplayMemberPath = "People";
        }
        int i = -1;
        private void MediaElement_Initialized(object sender, EventArgs e)
        {
            if (i < ServiswList.Count)
            {
                i++;
                MediaElement ME = (MediaElement)sender;
                Service S = ServiswList[i];
                Uri U = new Uri(S.MainImagePath, UriKind.RelativeOrAbsolute);
                ME.Source = U;
                //   i++;
            }
        }
        private void TextBlock_Initialized(object sender, EventArgs e)
        {
            if (i < ServiswList.Count)
            {
                TextBlock TB = (TextBlock)sender;
                Service S = ServiswList[i];
                TB.Text = S.Title;
                //  i++;
            }

        }
        private void StackPanel_Initialized(object sender, EventArgs e)
        {
            if (i < ServiswList.Count)
            {
                StackPanel SP = (StackPanel)sender;
                Service S = ServiswList[i];
                if (S.Discount != 0)
                {
                    SP.Background = new SolidColorBrush(Color.FromRgb(231, 250, 191));
                }
            }

        }
    //Инициализация кнопок
    private void Button_Initialized_Red(object sender, EventArgs e)
        {
            Button BtnRed = (Button)sender;
            if (BtnRed != null)
            {
                BtnRed.Uid = Convert.ToString(i);
            }

        }
        private void Button_Initialized_Del(object sender, EventArgs e)
        {
            Button BtnDel = (Button)sender;
            if (BtnDel != null)
            {
                BtnDel.Uid = Convert.ToString(i);
            }
        }
        private void Button_Initialized_Add(object sender, EventArgs e)
        {
            Button BtnAdd = (Button)sender;
            if (BtnAdd != null)
            {
                BtnAdd.Uid = Convert.ToString(i);
            }
        }
        Service S1;
        private void BReg_Click(object sender, RoutedEventArgs e)
        {
            Button BEdit = (Button)sender;
            int ind = Int32.Parse(BEdit.Uid);
            S1 = ServiswList[ind];
            //MessageBox.Show(S1.Title);
            MSP.Visibility = Visibility.Collapsed;
            SPRed.Visibility = Visibility.Visible;
            TBIRId.Text = Convert.ToString(S1.ID);
            TBRTitle.Text = S1.Title;
            TBRCost.Text = Convert.ToString(S1.Cost);
            TBRTime.Text = Convert.ToString(S1.DurationInSeconds);
            TBRSale.Text = Convert.ToString(S1.Discount);
            TBRImage.Text = S1.MainImagePath;
        }

        private void BRReg_Click(object sender, RoutedEventArgs e)
        {
            S1.Title = TBRTitle.Text;

            BD.CE.SaveChanges();
            MessageBox.Show("Запись изменена");
        }

        private void BRBack_Click(object sender, RoutedEventArgs e)
        {
            F.Mframe.Navigate(new Admin());
            SPRed.Visibility = Visibility.Collapsed;
            MSP.Visibility = Visibility.Visible;
        }

        private void BAdd_Click(object sender, RoutedEventArgs e)
        {
            SPRed.Visibility = Visibility.Collapsed;
            MSP.Visibility = Visibility.Collapsed;
            SPAdd.Visibility = Visibility.Visible;
        }

        private void BAAdd_Click(object sender, RoutedEventArgs e)
        {
            Service S = new Service();
            S.Title = TBATitle.Text;
            S.Cost = Convert.ToInt32(TBACost.Text);
            S.DurationInSeconds = Convert.ToInt32(TBATime.Text);
            S.Discount = Convert.ToInt32(TBASale.Text);
            S.Description = TBADescription.Text;
            S.MainImagePath = TBAImage.Text;
            BD.CE.Service.Add(S);
            BD.CE.SaveChanges();
        }

        private void RImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.ShowDialog();
            string path = OFD.FileName;
            TBRImage.Text = path;
        }
        private void BDel_Click(object sender, RoutedEventArgs e)
        {
            Button BtnRed = (Button)sender;
            int ind = Convert.ToInt32(BtnRed.Uid);
            Service S = ServiswList[ind];
            BD.CE.Service.Remove(S);
            MessageBox.Show("Удалена");
            BD.CE.SaveChanges();
            F.Mframe.Navigate(new Admin());
        }

        //private void BRed_Click(object sender, RoutedEventArgs e)
        //{
        //    Button BtnRed = (Button)sender;
        //    int ind = Convert.ToInt32(BtnRed.Uid);
        //    Service S = ServiswList[ind];
        //    BD.CE.Service.Remove(S);
        //    MessageBox.Show("Удалена");
        //    BD.CE.SaveChanges();
        //    F.Mframe.Navigate(new Admin());
        //}

        private void Aimage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.ShowDialog();
            string path = OFD.FileName;
            TBAImage.Text = path;
        }

        private void BABack_Click(object sender, RoutedEventArgs e)
        {
            F.Mframe.Navigate(new Admin());
            //SPRed.Visibility = Visibility.Collapsed;
            //MSP.Visibility = Visibility.Visible;
        }
        private void TextBlock_Initialized_Cost(object sender, EventArgs e)
        {
            if (i < ServiswList.Count)
            {
                TextBlock Price = (TextBlock)sender;
                Service S = ServiswList[i];
                Price.Text = Convert.ToInt32(S.Cost) + "";
            }
            //TextBlock TB = (TextBlock)sender;
            //Service SE = ServiswList[i];
            //if(SE.Discount == 0)
            //{
            //    TB.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    int old_cost = Convert.ToInt32(SE.Cost);
            //    TB.TextDecorations = TextDecorations.Strikethrough;
            //    TB.Text = Convert.ToString(old_cost);
            //}

        }
        private void TextBlock_Initialized_Duration(object sender, EventArgs e)
        {
            if (i < ServiswList.Count)
            {
                TextBlock Duration = (TextBlock)sender;
                Service S = ServiswList[i];
                Duration.Text = Convert.ToString(S.DurationInSeconds / 60 + " " + "Минут");
                
            }
        }
        private void TextBlock_Initialized_Discount(object sender, EventArgs e)
        {
            if (i < ServiswList.Count)
            {
                TextBlock Disc = (TextBlock)sender;
                Service S = ServiswList[i];
                Disc.Text = Convert.ToString(S.Discount * 100 + "%");     
            }
        }
        DateTime DT;
        private void TBTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex RTime = new Regex("[0-1][0-9]:[0-5][0-9]");
            Regex RTime1 = new Regex("2[0-3]:[0-5][0-9]");

            if ((RTime.IsMatch(TBTime.Text) || RTime1.IsMatch(TBTime.Text)) && TBTime.Text.Length == 5) 
            {
                DT = Convert.ToDateTime(DP.SelectedDate);
                TimeSpan TS = TimeSpan.Parse(TBTime.Text);
                DT = DT.Add(TS);
                if(DT > DateTime.Now)
                {
                    MessageBox.Show(DT + "");
                }
                else
                {
                    MessageBox.Show("Дата неверна");
                }
               
            }

            else
            {
                if(TBTime.Text.Length >= 5)
                {
                    MessageBox.Show("Неверное время");
                }
            }
        }
        int index;

        private void CBPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            index = (int)CBPeople.SelectedValue;
            MessageBox.Show(index + "");
        }

        private void BNBack_Click(object sender, RoutedEventArgs e)
        {
            F.Mframe.Navigate(new Admin());
        }

        private void BNZ_Click(object sender, RoutedEventArgs e)
        {
            Button BEdit = (Button)sender;
            int ind = Int32.Parse(BEdit.Uid);
            S1 = ServiswList[ind];
            TBNTitle.Text = Convert.ToString(S1.Title);
            TBNTime.Text = Convert.ToString(S1.DurationInSeconds / 60 + " " + "Минут");
            SPRed.Visibility = Visibility.Collapsed;
            MSP.Visibility = Visibility.Collapsed;
            SPAdd.Visibility = Visibility.Collapsed;
            SPNZ.Visibility = Visibility.Visible;
        }
        //int index;
        private void BNWrite_Click(object sender, RoutedEventArgs e)
        {
            Service S = ServiswList[i];
            int index = (int)CBPeople.SelectedValue;
            ClientService obj = new ClientService()
            {
                ClientID = index,
                ServiceID = S.ID,
                StartTime = DT
            };
            BD.CE.ClientService.Add(obj);
            BD.CE.SaveChanges();
            MessageBox.Show("Запись добавлена");
        }

        private void SortUp_Click(object sender, RoutedEventArgs e)
        {
            i = -1;
            ServiswList.Sort((x, y) => x.Cost.CompareTo(y.Cost));
            DGServises.Items.Refresh();
        }

        private void SortDown_Click(object sender, RoutedEventArgs e)
        {
            i = -1;
            ServiswList.Sort((x, y) => x.Cost.CompareTo(y.Cost));
            ServiswList.Reverse();
            DGServises.Items.Refresh();
        }
        List<Service> ServiswListFilter = new List<Service>();
        private void CBFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            i = -1;
            switch(CBFilter.SelectedIndex)
            {
                case 0:
                    ServiswListFilter = ServiswList.Where(x => x.Discount < 0.05).ToList();
                    ServiswList = ServiswListFilter;
                    DGServises.ItemsSource = ServiswList;
                    break;
                case 1:
                    ServiswListFilter = ServiswList.Where(x => x.Discount > 0.05 && x.Discount < 0.15).ToList();
                    ServiswList = ServiswListFilter;
                    DGServises.ItemsSource = ServiswList;
                    break;
                case 2:
                    ServiswListFilter = ServiswList.Where(x => x.Discount > 0.15 && x.Discount < 0.3).ToList();
                    ServiswList = ServiswListFilter;
                    DGServises.ItemsSource = ServiswList;
                    break;
                case 3:
                    ServiswListFilter = ServiswList.Where(x => x.Discount > 0.3 && x.Discount < 0.7).ToList();
                    ServiswList = ServiswListFilter;
                    DGServises.ItemsSource = ServiswList;
                    break;
                case 4:
                    ServiswListFilter = ServiswList.Where(x => x.Discount > 0.7 && x.Discount < 1).ToList();
                    ServiswList = ServiswListFilter;
                    DGServises.ItemsSource = ServiswList;
                    break;
                case 5:
                    ServiswListFilter = ServiswList.Where(x => x.Discount > 0 && x.Discount < 1).ToList();
                    ServiswList = ServiswList1;
                    DGServises.ItemsSource = ServiswList;
                    break;

            }

        }

        private void TBPoisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            i = -1;
            List<Service> ServiswListPoisk = new List<Service>();
            if (TBPoisk.Text != "")
            {
                ServiswListPoisk = ServiswList.Where(x => x.Title.Contains(TBPoisk.Text)).ToList();
                ServiswList = ServiswListPoisk;
                DGServises.ItemsSource = ServiswList;
            }
            else
            {
                if (ServiswListFilter.Count == 0) 
                {
                    ServiswList = ServiswList1;
                    DGServises.ItemsSource = ServiswList;
                }
                else
                {
                    ServiswList = ServiswListFilter;
                    DGServises.ItemsSource = ServiswList;
                }
            }
        }
    }
}
