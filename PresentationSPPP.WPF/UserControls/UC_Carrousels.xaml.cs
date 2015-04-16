using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
using PresentationSPPP.LIB;
using VersionS3P = PresentationSPPP.LIB.Version;

namespace PresentationSPPP.WPF
{
    /// <summary>
    /// Logique d'interaction pour UC_Carrousels.xaml
    /// </summary>
    public partial class UC_Carrousels : UserControl
    {
        private UC_Carrousels_View View;
        public UC_Carrousels()
        {
            DataContext = View = new UC_Carrousels_View();
            InitializeComponent();
        }

        private void ComboBox_CategorieLogigrams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            View.ComboBox_CategorieLogigrams_SelectionChanged();
        }
    }
    public class UC_Carrousels_View : View
    {
        public ObservableCollection<Logigram> LogigramsNextsPossibles { get; set; }
        public Categorie CategorieSelected { get; set; }
        public Logigram LogigramSelected { get; set; }

        public UC_Carrousels_View()
        {
        }

        public void ComboBox_CategorieLogigrams_SelectionChanged()
        {
            if (CategorieSelected != null && LogigramSelected != null)
            {
                LogigramsNextsPossibles = new ObservableCollection<Logigram>();
                if (LogigramSelected.Next != null)
                {
                    if (LogigramSelected.Next.FID_Categorie == CategorieSelected.ID)
                    {
                        LogigramsNextsPossibles.Add(LogigramSelected.Next);
                    }
                }
                foreach (Logigram logigram in CategorieSelected.Logigrammes.Where(L => L.Previous == null && L.ID != LogigramSelected.ID).ToList())
                {
                    LogigramsNextsPossibles.Add(logigram);
                }
            }
            else
	        {
                LogigramsNextsPossibles = null;
	        }
        }
    }
}
