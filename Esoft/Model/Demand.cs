//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Esoft.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Demand
    {
        public int Id { get; set; }
        public int IdClient { get; set; }
        public int IdRealtor { get; set; }
        public int IdTypeOfEstate { get; set; }
        public Nullable<int> MinPrice { get; set; }
        public Nullable<int> MaxPrice { get; set; }
        public Nullable<int> MinNumbOfRooms { get; set; }
        public Nullable<int> MaxNumbOfRooms { get; set; }
        public Nullable<int> MinFloorNumber { get; set; }
        public Nullable<int> MaxFloorNumber { get; set; }
        public Nullable<int> MinNumbOfStroyes { get; set; }
        public Nullable<int> MaxNumbOfStroyes { get; set; }
        public string CityAddress { get; set; }
        public string StreetAddress { get; set; }
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public Nullable<double> MinTotalArea { get; set; }
        public Nullable<double> MaxTotalArea { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Realtor Realtor { get; set; }
        public virtual TypesOfEstate TypesOfEstate { get; set; }
    }
}