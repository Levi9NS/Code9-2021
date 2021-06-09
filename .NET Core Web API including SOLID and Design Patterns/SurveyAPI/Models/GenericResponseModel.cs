using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyAPI.Models
{
    public class GenericResponseModel<T> where T : class
    {
        public bool IsSuccessful { get; set; }

        public string ErrorMessage { get; set; }

        public T Data { get; set; }

        public List<T> DataList { get; set; }
    }
}
