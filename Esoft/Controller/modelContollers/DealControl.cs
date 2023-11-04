using Esoft.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WpfApp1.Controller;

namespace Esoft.Controller.modelContollers
{
    internal class DealControl : ModelControl
    {
        public async Task<IEnumerable> GetDemandsAsync(int idClient)
        {
            try
            {
                var q = from x in esoftDB.Demands
                        join r in esoftDB.Realtors
                        on x.IdRealtor equals r.Id into b
                        join cl in esoftDB.Clients
                        on x.IdClient equals cl.Id into c
                        join t in esoftDB.TypesOfEstates
                        on x.IdTypeOfEstate equals t.Id into d
                        from g in esoftDB.TypesOfEstates
                        join y in esoftDB.Estates
                        on x.Id equals y.Id into a
                        from estate in a.DefaultIfEmpty()
                        from realtor in b.DefaultIfEmpty()
                        from client in c.DefaultIfEmpty()
                        from type in d.DefaultIfEmpty()
                        where client.Id == idClient
                        select new
                        {
                            demandId = x.Id,
                            realtorLastName = realtor.LastName,
                            realtorFirstName = realtor.FirstName,
                            realtorPatronymic = realtor.Patronymic,
                            estateCityAddress = x.CityAddress,
                            estateStreetAddress = x.StreetAddress,
                            estateHouseNumber = x.HouseNumber,
                            estateApartmentNumber = x.ApartmentNumber,
                            typeTypeName = type.TypeName,
                            estateMinTotalArea = x.MinTotalArea,
                            estateMaxTotalArea = x.MaxTotalArea,
                            x.MinPrice,
                            x.MaxPrice,
                            minRooms = x.MinNumbOfRooms,
                            maxRooms = x.MaxNumbOfRooms,
                            minFloor = x.MinFloorNumber,
                            maxFloor = x.MaxFloorNumber,
                            minStroyes = x.MinNumbOfStroyes,
                            maxStroyes = x.MaxNumbOfStroyes
                        };

                var list = await q.Distinct().ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<IEnumerable> GetOffersAsync(int idClient)
        {
            try
            {
                int z = await esoftDB.Demands.Where(x => x.IdClient == idClient).CountAsync();
                if(z > 0)
                {
                    var q = from x in esoftDB.Offers
                            join y in esoftDB.Estates
                            on x.IdEstate equals y.Id into a
                            join r in esoftDB.Realtors
                            on x.IdRealtor equals r.Id into b
                            join cl in esoftDB.Clients
                            on x.IdClient equals cl.Id into c
                            join t in esoftDB.TypesOfEstates
                            on x.Estate.IdTypeOfEstate equals t.Id into d
                            join g in esoftDB.Demands
                            on x.Id equals idClient into j
                            from type in d.DefaultIfEmpty()
                            from estate in a.DefaultIfEmpty()
                            from realtor in b.DefaultIfEmpty()
                            from client in c.DefaultIfEmpty()
                            from f in j.DefaultIfEmpty()
                            where f.IdTypeOfEstate == estate.IdTypeOfEstate &&

                                  (f.MinPrice <= x.Price || f.MinPrice == null) &&
                                  (f.MaxPrice >= x.Price || f.MaxPrice == null) &&

                                  (f.MinTotalArea <= estate.TotalArea || f.MinTotalArea == null) &&
                                  (f.MaxTotalArea >= estate.TotalArea || f.MaxTotalArea == null) &&

                                  (f.MinNumbOfRooms <= estate.NumberOfRooms || f.MinNumbOfRooms == null) &&
                                  (f.MaxNumbOfRooms >= estate.NumberOfRooms || f.MaxNumbOfRooms == null) &&

                                  (f.MinNumbOfStroyes <= estate.NumberOfStroyes || f.MinNumbOfStroyes == null) &&
                                  (f.MaxNumbOfStroyes >= estate.NumberOfStroyes || f.MaxNumbOfStroyes == null) &&

                                  (f.CityAddress == estate.CityAddress || f.CityAddress == null) &&
                                  (f.StreetAddress == estate.StreetAddress || f.StreetAddress == null) &&
                                  (f.HouseNumber == estate.HouseNumber || f.HouseNumber == null) &&
                                  (f.ApartmentNumber == estate.ApartmentNumber || f.ApartmentNumber == null)
                            select new
                            {
                                offerId = x.Id,
                                realtorLastName = realtor.LastName,
                                realtorFirstName = realtor.FirstName,
                                realtorPatronymic = realtor.Patronymic,
                                estateCityAddress = estate.CityAddress,
                                estateStreetAddress = estate.StreetAddress,
                                estateHouseNumber = estate.HouseNumber,
                                estateApartmentNumber = estate.ApartmentNumber,
                                typeTypeName = type.TypeName,
                                estateTotalArea = estate.TotalArea,
                                x.Price
                            };

                    var list = await q.Distinct().ToListAsync();
                    return list;
                }
                else
                {
                    var q = from x in esoftDB.Offers
                            join y in esoftDB.Estates
                            on x.IdEstate equals y.Id into a
                            join r in esoftDB.Realtors
                            on x.IdRealtor equals r.Id into b
                            join cl in esoftDB.Clients
                            on x.IdClient equals cl.Id into c
                            join t in esoftDB.TypesOfEstates
                            on x.Estate.IdTypeOfEstate equals t.Id into d
                            from type in d.DefaultIfEmpty()
                            from estate in a.DefaultIfEmpty()
                            from realtor in b.DefaultIfEmpty()
                            from client in c.DefaultIfEmpty()
                            select new
                            {
                                offerId = x.Id,
                                realtorLastName = realtor.LastName,
                                realtorFirstName = realtor.FirstName,
                                realtorPatronymic = realtor.Patronymic,
                                estateCityAddress = estate.CityAddress,
                                estateStreetAddress = estate.StreetAddress,
                                estateHouseNumber = estate.HouseNumber,
                                estateApartmentNumber = estate.ApartmentNumber,
                                typeTypeName = type.TypeName,
                                estateTotalArea = estate.TotalArea,
                                x.Price
                            };

                    var list = await q.Distinct().ToListAsync();
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<List<string>> GetClientsAsync()
        {
            var list = await esoftDB.Clients.Where(x =>
                x.Offers.Count > 0 || x.Demands.Count > 0).
                Select(x =>
                x.Id + " | " + x.LastName + " " + x.FirstName + " " + x.Patronymic + "\nНомер телефона: " + x.MobileNumber + "\nEmail: " + x.Email).
                ToListAsync();
            return list;
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

            }
        }
    }
}
