using DijiWalk.Common.Response;
using DijiWalk.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Business.Contracts
{
    public interface ITagBusiness
    {


        /// <summary>
        /// Method to separate tag and routetags 
        /// </summary>
        /// <param name="tag">Object tag</param>
        Tag SeparateTag(Tag tag);
    }
}
