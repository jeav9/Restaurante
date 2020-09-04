using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.Helpers
{
    public class ResultHelper<T>
    {
        public T Value { get; set; }
        public List<string> Errors { get; set; }

        public ResultHelper()
        {
            this.Errors = new List<string>();
        }

        public void AddError(string error)
        {
            this.Errors.Add(error);
        }

        public bool Success
        {
            get { return (this.Errors.Count == 0); }
        }
    }
}
