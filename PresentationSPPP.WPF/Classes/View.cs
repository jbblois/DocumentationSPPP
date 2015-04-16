using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PresentationSPPP.LIB;
using VersionS3P = PresentationSPPP.LIB.Version;
namespace PresentationSPPP.WPF
{
    public class View : INotifyPropertyChanged
    {
        public static PresentationSPPPEntities Context { get { return Base.Context; } }

        protected static ObservableCollection<Categorie> _Categories;
        public static ObservableCollection<Categorie> Categories
        {
            get
            {
                if (_Categories == null)
                {
                    _Categories = new ObservableCollection<Categorie>(Base.Context.Categories);
                }
                return _Categories;
            }
        }

        protected static ObservableCollection<Document> _Documents;
        public static ObservableCollection<Document> Documents
        {
            get
            {
                if (_Documents == null)
                {
                    _Documents = new ObservableCollection<Document>(Base.Context.Documents);
                }
                return _Documents;
            }
        }

        protected static ObservableCollection<Extension> _Extensions;
        public static ObservableCollection<Extension> Extensions
        {
            get
            {
                if (_Extensions == null)
                {
                    _Extensions = new ObservableCollection<Extension>(Base.Context.Extensions);
                }
                return _Extensions;
            }
        }

        protected static ObservableCollection<Lien> _Liens;
        public static ObservableCollection<Lien> Liens
        {
            get
            {
                if (_Liens == null)
                {
                    _Liens = new ObservableCollection<Lien>(Base.Context.Liens);
                }
                return _Liens;
            }
        }

        protected static ObservableCollection<Logigram> _Logigrams;
        public static ObservableCollection<Logigram> Logigrams
        {
            get
            {
                if (_Logigrams == null)
                {
                    _Logigrams = new ObservableCollection<Logigram>(Base.Context.Documents.OfType<Logigram>());
                }
                return _Logigrams;
            }
        }

        protected static ObservableCollection<VersionS3P> _Versions;
        public static ObservableCollection<VersionS3P> Versions
        {
            get
            {
                if (_Versions == null)
                {
                    _Versions = new ObservableCollection<VersionS3P>(Base.Context.Versions);
                }
                return _Versions;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static Boolean Cancel()
        {
            _Categories = null;
            _Documents = null;
            _Extensions = null;
            _Liens = null;
            _Logigrams = null;
            _Versions = null;
            Base.Cancel();
            return true;
        }

        public static void Synchroniser()
        {
            _Categories = null;
            _Documents = null;
            _Extensions = null;
            _Liens = null;
            _Logigrams = null;
            _Versions = null;
        }
    }
}
