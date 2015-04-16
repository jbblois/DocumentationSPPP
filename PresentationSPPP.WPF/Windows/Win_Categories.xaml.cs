using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.DirectoryServices.AccountManagement;
using PresentationSPPP.LIB;
namespace PresentationSPPP.WPF
{
    /// <summary>
    /// Logique d'interaction pour Win_Categories.xaml
    /// </summary>
    public partial class Win_Categories : Window
    {
        private Win_Categorie_View View;
        public Win_Categories()
        {
            DataContext = View = new Win_Categorie_View();
            InitializeComponent();
        }

        private void Button_AddCategorie_Click(object sender, RoutedEventArgs e)
        {
            Win_Categorie_View.Button_AddCategorie_Click();
        }

        private void Button_DeleteCategorie_Click(object sender, RoutedEventArgs e)
        {
            Win_Categorie_View.Button_DeleteCategorie_Click(ListBox_Categories.SelectedItem as Categorie);
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            Win_Categorie_View.Button_Save_Click();
            this.Close();
            
        }

        private void ListBox_Categories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            View.ListBox_Categories_SelectionChanged(ListBox_Categories.SelectedItem as Categorie);
        }
    }

    public class Win_Categorie_View : View
    {
        private Boolean _ReadOnly;

        public Boolean ReadOnly
        {
            get { return _ReadOnly; }
            set 
            { 
                _ReadOnly = value;
                NotifyPropertyChanged();
            }
        }
        
        public static void Button_AddCategorie_Click()
        {
            Categorie NouvelleCategorie = new Categorie();
            NouvelleCategorie.Nom = "Nouvelle catégorie";
            Categories.Add(NouvelleCategorie);
            Context.Categories.Add(NouvelleCategorie);
        }
        public void ListBox_Categories_SelectionChanged(Categorie CategorieSelected)
        {
            if (CategorieSelected != null)
            {
                if (CategorieSelected.ID == 0)
                {
                    ReadOnly = false;
                }
                else
                {
                   ReadOnly = true; 
                }
            }
            else
            {
                ReadOnly = true; 
            }
        }
        public static void Button_DeleteCategorie_Click(Categorie CategorieSelected)
        {
            if (CategorieSelected != null)
            {
                if (CategorieSelected.NbsDocuments == 0)
                {
                    try
                    {
                        System.IO.Directory.Delete(Base.FileBase_Path + '\\' + CategorieSelected.Nom);
                    }
                    catch (Exception)
                    {
                    }
                    Categories.Remove(CategorieSelected);
                    Context.Categories.Remove(CategorieSelected);
                    Base.Save();
                }
                else
                {
                    MessageBox.Show("Erreur: des documents appartiennent à cette catégorie");
                }
            }
            else
            {
                MessageBox.Show("Erreur: vous devez sélectionner un catégorie");
            }
        }
        public static void Button_Save_Click()
        {
            List<Categorie> ListCategories = Categories.ToList();
            foreach (Categorie categorie in ListCategories)
            {
                if (categorie.Nom == "" || categorie.Nom == "Nouvelle catégorie")
                {
                    Categories.Remove(categorie);
                    Context.Categories.Remove(categorie);
                }
                else if (categorie.ID == 0 && Categories.Count(C => C.Nom == categorie.Nom) > 1)
                {
                    Categories.Remove(categorie);
                    Context.Categories.Remove(categorie);
                }
            }
            Base.Save();

        }
    }
}
