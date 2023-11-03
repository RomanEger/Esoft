using Esoft.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Controller;

namespace Esoft.Controller
{
    internal class OfferControl : ModelControl
    {

        public async Task AddOffer(string[][] dataArr)
        {
            try
            {
                for (int i = 0; i < dataArr.Length; i++)
                {
                    if (dataArr[i][0].ToCharArray()[0] < 48 ||
                        dataArr[i][0].ToCharArray()[0] > 57)
                        continue;
                    bool isUnique = await IsUniqueOffer();
                    if (i > 0 && !isUnique)
                        continue;
                    Offer offer = new Offer()
                    {
                        Price = int.Parse(dataArr[i][1]),
                        IdRealtor = int.Parse(dataArr[i][2]),
                        IdClient = int.Parse(dataArr[i][3]),
                        IdEstate = int.Parse(dataArr[i][4])
                    };
                    esoftDB.Offers.Add(offer);
                }
                await SaveChangesDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }

        }

        public async Task AddOffer(int clientId, int realtorId, int estateId, int price, int operationId)
        {
            Offer offer = new Offer()
            {
                IdClient = clientId,
                IdRealtor = realtorId,
                IdEstate = estateId,
                Price = price
            };

            await IsUniqueOffer();
            if(operationId == 0)
                esoftDB.Offers.AddOrUpdate(offer);
            else if(operationId == 1)
            {
                var item = await esoftDB.Offers.Where(x => x.IdClient == offer.IdClient && x.IdEstate == offer.IdEstate).FirstOrDefaultAsync();
                if(item != null)
                {
                    offer.Id = item.Id;
                    esoftDB.Offers.AddOrUpdate(offer);
                }
            }    
            await SaveChangesDB();
        }

        public async Task<IEnumerable> GetOffersAsync()
        {
            try
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
                            clientLastName = client.LastName,
                            clientFirstName = client.FirstName,
                            clientPatronymic = client.Patronymic,
                            clientMobileNumber = client.MobileNumber,
                            clientEmail = client.Email,
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
                
                var list = await q.ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task RemoveOffer(IEnumerable data)
        {
            try
            {

            }
            catch
            {

            }
        }

        public async Task<IEnumerable> FillingCmbClient()
        {
            var q = await esoftDB.Clients.OrderBy(x => x.Id).Select(x => x.Id + " | " + x.LastName + " " + x.FirstName + " " + x.Patronymic + " " + x.MobileNumber + " " + x.Email).ToListAsync();
            return q;
        }

        public async Task<IEnumerable> FillingCmbRealtor()
        {
            var q = await esoftDB.Realtors.OrderBy(x => x.Id).Select(x => x.Id + " | " + x.LastName + " " + x.FirstName + " " + x.Patronymic).ToListAsync();
            return q;
        }

        public async Task<IEnumerable> FillingCmbEstate()
        {
            var q = await esoftDB.Estates.OrderBy(x => x.Id).Select(x => x.Id + " | " + x.CityAddress + " " + x.StreetAddress + " " + x.HouseNumber + " " +
                x.ApartmentNumber + " Этажей: " + x.NumberOfStroyes + " Комнат: " + x.NumberOfRooms + " Этаж: " + x.FloorNumber).ToListAsync();
            return q;
        }

    }
}
