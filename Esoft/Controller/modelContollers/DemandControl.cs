using Esoft.Model;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Controller;

namespace Esoft.Controller
{
    internal class DemandControl : ModelControl
    {
        public async Task AddDemand(string[][] dataArr, int idTypeOfEstate)
        {
            try
            {
                switch (idTypeOfEstate)
                {
                    case 0: //house
                        {
                            for (int i = 0; i < dataArr.Length; i++)
                            {
                                if (dataArr[i][0].ToCharArray()[0] < 48 ||
                                    dataArr[i][0].ToCharArray()[0] > 57)
                                    continue;
                                bool isUnique = await IsUniqueDemand(dataArr[i]);
                                if (i > 0 && !isUnique)
                                    continue;
                                Demand demand = new Demand()
                                {
                                    CityAddress = parser.GetStringOrNullData(dataArr[i][1]),
                                    StreetAddress = parser.GetStringOrNullData(dataArr[i][2]),
                                    HouseNumber = parser.GetStringOrNullData(dataArr[i][3]),
                                    ApartmentNumber = parser.GetStringOrNullData(dataArr[i][4]),
                                    MinPrice = parser.ConvertToIntOrNull(dataArr[i][5]),
                                    MaxPrice = parser.ConvertToIntOrNull(dataArr[i][6]),
                                    IdRealtor = int.Parse(dataArr[i][7]),
                                    IdClient = int.Parse(dataArr[i][8]),
                                    MinNumbOfStroyes = parser.ConvertToIntOrNull(dataArr[i][9]),
                                    MaxNumbOfStroyes = parser.ConvertToIntOrNull(dataArr[i][10]),
                                    MinTotalArea = parser.ConvertToDoubleOrNull(dataArr[i][11]),
                                    MaxTotalArea = parser.ConvertToDoubleOrNull(dataArr[i][12]),
                                    MinNumbOfRooms = parser.ConvertToIntOrNull(dataArr[i][13]),
                                    MaxNumbOfRooms = parser.ConvertToIntOrNull(dataArr[i][14]),
                                    IdTypeOfEstate = 1
                                };
                                esoftDB.Demands.Add(demand);
                            }
                            await SaveChangesDB();
                        }
                        break;
                    case 1: //appartment
                        {
                            for (int i = 0; i < dataArr.Length; i++)
                            {
                                if (dataArr[i][0].ToCharArray()[0] < 48 || dataArr[i][0].ToCharArray()[0] > 57)
                                    continue;
                                bool isUnique = await IsUniqueDemand(dataArr[i]);
                                if (i > 0 && !isUnique)
                                    continue;
                                Demand demand = new Demand()
                                {
                                    CityAddress = parser.GetStringOrNullData(dataArr[i][1]),
                                    StreetAddress = parser.GetStringOrNullData(dataArr[i][2]),
                                    HouseNumber = parser.GetStringOrNullData(dataArr[i][3]),
                                    ApartmentNumber = parser.GetStringOrNullData(dataArr[i][4]),
                                    MinPrice = parser.ConvertToIntOrNull(dataArr[i][5]),
                                    MaxPrice = parser.ConvertToIntOrNull(dataArr[i][6]),
                                    IdRealtor = int.Parse(dataArr[i][7]),
                                    IdClient = int.Parse(dataArr[i][8]),
                                    MinTotalArea = parser.ConvertToDoubleOrNull(dataArr[i][9]),
                                    MaxTotalArea = parser.ConvertToDoubleOrNull(dataArr[i][10]),
                                    MinNumbOfRooms = parser.ConvertToIntOrNull(dataArr[i][11]),
                                    MaxNumbOfRooms = parser.ConvertToIntOrNull(dataArr[i][12]),
                                    MinFloorNumber = parser.ConvertToIntOrNull(dataArr[i][13]),
                                    MaxFloorNumber = parser.ConvertToIntOrNull(dataArr[i][14]),
                                    IdTypeOfEstate = 2
                                };
                                esoftDB.Demands.Add(demand);
                            }
                            await SaveChangesDB();
                        }
                        break;
                    case 2: //land
                        {
                            for (int i = 0; i < dataArr.Length; i++)
                            {
                                if (dataArr[i][0].ToCharArray()[0] < 48 || dataArr[i][0].ToCharArray()[0] > 57)
                                    continue;
                                bool isUnique = await IsUniqueDemand(dataArr[i]);
                                if (i > 0 && !isUnique)
                                    continue;
                                Demand demand = new Demand()
                                {
                                    CityAddress = parser.GetStringOrNullData(dataArr[i][1]),
                                    StreetAddress = parser.GetStringOrNullData(dataArr[i][2]),
                                    HouseNumber = parser.GetStringOrNullData(dataArr[i][3]),
                                    ApartmentNumber = parser.GetStringOrNullData(dataArr[i][4]),
                                    MinPrice = parser.ConvertToIntOrNull(dataArr[i][5]),
                                    MaxPrice = parser.ConvertToIntOrNull(dataArr[i][6]),
                                    IdRealtor = int.Parse(dataArr[i][7]),
                                    IdClient = int.Parse(dataArr[i][8]),
                                    MinTotalArea = parser.ConvertToDoubleOrNull(dataArr[i][9]),
                                    MaxTotalArea = parser.ConvertToDoubleOrNull(dataArr[i][10]),
                                    IdTypeOfEstate = 3
                                };
                                esoftDB.Demands.Add(demand);
                            }
                            await SaveChangesDB();
                        }
                        break;
                    default:
                        break;
                }


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка");
            }
        }

        public async Task AddDemand(int clientId, int realtorId, int estateId, string cityAddress, string streetAddress,
            int? minPrice, int? maxPrice, int? minArea, int? maxArea, int? minRooms, int? maxRooms, int? minFloor, int? maxFloor)
        {
            Demand demand = new Demand()
            {
                IdClient = clientId,
                IdRealtor = realtorId,
                IdTypeOfEstate = estateId,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                MinTotalArea = minArea,
                MaxTotalArea = maxArea,
                CityAddress = cityAddress,
                StreetAddress = streetAddress,
            };
            if(estateId == 1)
            {
                demand.MinNumbOfRooms = minRooms;
                demand.MaxNumbOfRooms = maxRooms;
                demand.MinNumbOfStroyes = minFloor;
                demand.MaxNumbOfStroyes = maxFloor;
            }
            else if(estateId == 2)
            {
                demand.MinNumbOfRooms = minRooms;
                demand.MaxNumbOfRooms = maxRooms;
                demand.MinFloorNumber = minFloor;
                demand.MaxFloorNumber = maxFloor;
            }

            var item = await esoftDB.Demands.Where(x => 
                x.IdClient == demand.IdClient &&
                x.IdTypeOfEstate == demand.IdTypeOfEstate &&
                x.IdRealtor == demand.IdRealtor).FirstOrDefaultAsync();
            if (item != null)
            {
                demand.Id = item.Id;
                esoftDB.Demands.AddOrUpdate(demand);
            }
            else
                esoftDB.Demands.Add(demand);

            await SaveChangesDB();
        }

        public async Task<IEnumerable> GetDemandsAsync()
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
                        select new
                        {
                            demandId = x.Id,
                            clientLastName = client.LastName,
                            clientFirstName = client.FirstName,
                            clientPatronymic = client.Patronymic,
                            clientMobileNumber = client.MobileNumber,
                            clientEmail = client.Email,
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
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<IEnumerable> FillingCmbClient()
        {
            //var q = from x in esoftDB.Demands
            //        join y in esoftDB.Clients
            //        on x.IdClient equals y.Id
            //        into a
            //        from z in a
            //        select new
            //        {
            //            l = $"{z.Id} | {z.LastName} {z.FirstName} {z.Patronymic} | Номер телефона: {z.MobileNumber} Email: {z.Email}"
            //        };

            var q = esoftDB.Clients.Select(
                cl => 
                    cl.Id + " | " + cl.LastName + " " + cl.FirstName + " " + cl.Patronymic + " | Номер телефона: " + cl.MobileNumber + "Email: " + cl.Email
                );

            var list = await q.ToListAsync();
            return list;
        }

        public async Task<IEnumerable> FillingCmbRealtor()
        {
            //var q = from x in esoftDB.Demands
            //        join y in esoftDB.Realtors
            //        on x.IdRealtor equals y.Id
            //        into a
            //        from z in a
            //        select new
            //        {
            //            l = $"{z.Id} | {z.LastName} {z.FirstName} {z.Patronymic}"
            //        };

            var q = esoftDB.Realtors.Select(
                r => r.Id + " | " + r.LastName + " " + r.FirstName + " " + r.Patronymic
                );

            var list = await q.ToListAsync();
            return list;
        }

        public async Task<IEnumerable> FillingCmbType()
        {
            //var q = from x in esoftDB.Demands
            //        join y in esoftDB.TypesOfEstates
            //        on x.IdTypeOfEstate equals y.Id
            //        into a
            //        from z in a
            //        select
            //        z.TypeName;

            var q = esoftDB.TypesOfEstates.OrderBy(x => x.Id).Select(x => x.TypeName).Distinct();

            var list = await q.ToListAsync();
            return list;
        }

        public async Task<IEnumerable> FillingCmbCity()
        {
            var list = await esoftDB.Estates.Select(x => x.CityAddress).Distinct().ToListAsync();
            return list;
        }

        public async Task<IEnumerable> FillingCmbStreet()
        {
            var list = await esoftDB.Estates.Select(x => x.StreetAddress).Distinct().ToListAsync();
            return list;
        }

        public async Task RemoveDemand(List<int> id)
        {
            try
            {
                var list = new List<Demand>();
                foreach(var i in id)
                {
                    var l = await esoftDB.Demands.Where(x => x.Id == i).FirstOrDefaultAsync();
                    list.Add(l);
                }

                foreach (var item in list)
                {
                    
                    esoftDB.Demands.Remove(item);
                }
                await SaveChangesDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
