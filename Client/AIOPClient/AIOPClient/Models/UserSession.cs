using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIOPClient.Models
{
    public class UserSession
    {
        // Variable locale pour stocker une référence vers l'instance
	    private static UserSession instance = null;
	    private static readonly object mylock = new object();
        public bool logged { get; set; }
        public String userName { get; set; }
        public int id_user { get; set; }

	    private UserSession()
	    {
            logged = false;
	    }

	    // La méthode GetInstance doit être Shared
	    public static UserSession GetInstance()
	    {

		    lock ((mylock)) {
			    // Si pas d'instance existante on en crée une...
			    if (instance == null)
			    {
				    instance = new UserSession();
			    }

			    // On retourne l'instance de MonSingleton
			    return instance;
		    }
	    }
    }
}