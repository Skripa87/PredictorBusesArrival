//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PredictorBusesArrival
{
    using System;
    using System.Collections.Generic;
    
    public partial class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Nullable<int> Number { get; set; }
        public string fromst { get; set; }
        public Nullable<int> fromstid { get; set; }
        public string tost { get; set; }
        public string tostid { get; set; }
    }
}
