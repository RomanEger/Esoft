using Esoft.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Controller;

namespace Esoft.Controller.modelContollers
{
    internal class ManageDealControl : ModelControl
    {
        public async Task<IEnumerable> GetDemandsAsync()
        {
            try
            {
                var list = await esoftDB.Demands.ToListAsync();
                
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable> GetOffersAsync(int id)
        {
            try
            {
                var demand = await esoftDB.Demands.Where(x => x.Id == id).FirstOrDefaultAsync();

                var list = await esoftDB.Offers.Where(x =>
                    x.IdClient != demand.IdClient &&
                    
                    x.Estate.IdTypeOfEstate == demand.IdTypeOfEstate &&

                    (demand.MinPrice == null || x.Price >= demand.MinPrice) &&
                    (demand.MaxPrice == null || x.Price <= demand.MaxPrice) &&

                    (demand.MinTotalArea == null || x.Estate.TotalArea >= demand.MinTotalArea) &&
                    (demand.MaxTotalArea == null || x.Estate.TotalArea <= demand.MaxTotalArea) &&

                    (demand.MinNumbOfRooms == null || x.Estate.NumberOfRooms >= demand.MinNumbOfRooms) &&
                    (demand.MaxNumbOfRooms == null || x.Estate.NumberOfRooms <= demand.MaxNumbOfRooms) &&

                    (demand.MinFloorNumber == null || x.Estate.FloorNumber >= demand.MinFloorNumber) &&
                    (demand.MinFloorNumber == null || x.Estate.FloorNumber <= demand.MaxFloorNumber) &&

                    (demand.MinNumbOfStroyes == null || x.Estate.NumberOfStroyes >= demand.MinNumbOfStroyes) &&
                    (demand.MinNumbOfStroyes == null || x.Estate.NumberOfStroyes <= demand.MaxNumbOfStroyes) &&

                    (demand.CityAddress == null || x.Estate.CityAddress == demand.CityAddress) &&
                    (demand.StreetAddress == null || x.Estate.StreetAddress == demand.StreetAddress) &&
                    (demand.HouseNumber == null || x.Estate.HouseNumber == demand.HouseNumber) &&
                    (demand.ApartmentNumber == null || x.Estate.ApartmentNumber == demand.ApartmentNumber)
                    ).
                    ToListAsync();
                return list;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddDealAsync(int idDemand, int idOffer)
        {
            try
            {
                Deal deal = new Deal()
                {
                    IdDemand = idDemand,
                    IdOffer = idOffer
                };

                int count = await esoftDB.Deals.Where(x => x.IdDemand == idDemand || x.IdOffer == idOffer).CountAsync();
                if (count == 0)
                {
                    esoftDB.Deals.Add(deal);
                    await SaveChangesDB();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
