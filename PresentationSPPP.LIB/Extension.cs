//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PresentationSPPP.LIB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Extension
    {
        public Extension()
        {
            this.Versions = new HashSet<Version>();
        }
    
        public long ID { get; set; }
        public string Nom { get; set; }
    
        public virtual ICollection<Version> Versions { get; set; }
    }
}
