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
    
    public partial class Version
    {
        public long Numero { get; set; }
        public long FID_Document { get; set; }
        public long FID_Extension { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual Document Document { get; set; }
        public virtual Extension Extension { get; set; }
    }
}
