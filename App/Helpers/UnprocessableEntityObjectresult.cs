using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Helpers
{
    //to send 422 - unprocessable entity error message
    public class UnprocessableEntityObjectresult:ObjectResult
    {
        // takes modelstate dictionary to give only the error message
        // if ModelState then it will given out all key value pair
        //serializableError sends only the erroe field
        public UnprocessableEntityObjectresult(ModelStateDictionary modelState)
            :base(new SerializableError(modelState))
        {
            if( modelState == null)
            {
                throw new ArgumentNullException(nameof(modelState));
            }

            StatusCode = 422;
        }
    }
}
