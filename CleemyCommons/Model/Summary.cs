using System.Collections.Generic;

namespace CleemyCommons.Model
{
    public class Summary<T>
    {
        public Summary()
        {
            Errors = new List<Error>();
        }

        public T Scope { get; set; }

        public List<Error> Errors { get; set; }

        public void Add(string scope, string reaseon)
        {
            Errors.Add(new Error
            {
                Scope = scope,
                Reason = reaseon
            });
        }
    }
}