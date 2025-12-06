using System;

namespace DiplomGames
{
    public class DIRegistation
    {
        public Func<DIContainer, object> Factory { get; set; }
        public bool IsSengleton { get; set; }
        public object Instance { get; set; }
    }
}
