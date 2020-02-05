using DijiWalk.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DijiWalk.Common.Response
{
    public class ApiResponse
    {

        /// <summary>
        /// Propriété message qui contient le message qui sera affiché partie client.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Propriété status qui contient le status (VOIR ApiStatus.cs pour la liste)
        /// </summary>
        public int Status { get; set; }
        
    }
}

