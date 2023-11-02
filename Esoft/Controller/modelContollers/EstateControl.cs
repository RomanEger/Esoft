using Esoft.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using WpfApp1.Controller;

namespace Esoft.Controller
{
    internal class EstateControl : ModelControl
    {

        public async Task AddOrUpdateEstate(IEnumerable data)
        {
            try
            {
                List<Estate> estates = data.Cast<Estate>().ToList();
                string[][] arr = new string[estates.Count][];
                for (int i = 0; i < estates.Count; i++)
                {
                    arr[i] = new string[] { $"{i}",
                                            parser.GetStringOrNullData(estates[i].CityAddress),
                                            parser.GetStringOrNullData(estates[i].StreetAddress),
                                            parser.GetStringOrNullData(estates[i].HouseNumber),
                                            parser.GetStringOrNullData(estates[i].ApartmentNumber),
                                            parser.GetStringOrNullData(estates[i].Latitude.ToString()),
                                            parser.GetStringOrNullData(estates[i].Longtitude.ToString()),
                    };

                    bool isUnique = await IsUniqueEstate(arr[i], 1);
                    if (isUnique == false)
                    {
                        MessageBox.Show("Проверьте уникальность данных и повторите попытку", "Сохранения не приняты");
                        return;
                    }


                    if (arr[i][0] == null || arr[i][1] == null)
                    {
                        MessageBox.Show($"Объект недвижимости \"{arr[i][1]} {arr[i][2]} {arr[i][3]} {arr[i][4]}\" не удалось добавить\nПроверьте данные и повторите попытку", "Ошибка");
                        estates.RemoveAt(i);
                    }
                }


                if (true)
                {
                    esoftDB.Estates.AddOrUpdate(estates.ToArray());
                    await SaveChangesDB();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить изменения", "Ошибка");
            }
        }

        public async Task AddEstate(string[][] dataArr, int idTypeOfEstate)
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

                                bool isUnique = await IsUniqueEstate(dataArr[i], 1);
                                if (i > 0 && !isUnique)
                                    continue;
                                Estate estate = new Estate()
                                {
                                    CityAddress = parser.GetStringOrNullData(dataArr[i][1]),
                                    StreetAddress = parser.GetStringOrNullData(dataArr[i][2]),
                                    HouseNumber = parser.GetStringOrNullData(dataArr[i][3]),
                                    ApartmentNumber = parser.GetStringOrNullData(dataArr[i][4]),
                                    Latitude = parser.ConvertToDoubleOrNull(dataArr[i][5], -90, 90),
                                    Longtitude = parser.ConvertToDoubleOrNull(dataArr[i][6], -180, 180),
                                    NumberOfStroyes = parser.ConvertToIntOrNull(dataArr[i][7], 0),
                                    TotalArea = parser.ConvertToDoubleOrNull(dataArr[i][8], 0),
                                    IdTypeOfEstate = 1
                                };
                                esoftDB.Estates.Add(estate);
                            }
                        }
                        break;
                    case 1: //appartment
                        {
                            for (int i = 0; i < dataArr.Length; i++)
                            {
                                if (dataArr[i][0].ToCharArray()[0] < 48 ||
                                    dataArr[i][0].ToCharArray()[0] > 57)
                                    continue;
                                bool isUnique = await IsUniqueEstate(dataArr[i], 1);
                                if (i > 0 && !isUnique)
                                    continue;
                                Estate estate = new Estate()
                                {
                                    CityAddress = parser.GetStringOrNullData(dataArr[i][1]),
                                    StreetAddress = parser.GetStringOrNullData(dataArr[i][2]),
                                    HouseNumber = parser.GetStringOrNullData(dataArr[i][3]),
                                    ApartmentNumber = parser.GetStringOrNullData(dataArr[i][4]),
                                    Latitude = parser.ConvertToDoubleOrNull(dataArr[i][5], -90, 90),
                                    Longtitude = parser.ConvertToDoubleOrNull(dataArr[i][6], -180, 180),
                                    TotalArea = parser.ConvertToDoubleOrNull(dataArr[i][7], 0),
                                    FloorNumber = parser.ConvertToIntOrNull(dataArr[i][8], 0),
                                    NumberOfRooms = parser.ConvertToIntOrNull(dataArr[i][9], 0),
                                    IdTypeOfEstate = 2
                                };
                                esoftDB.Estates.Add(estate);
                            }
                        }
                        break;
                    case 2: //land
                        {
                            for (int i = 0; i < dataArr.Length; i++)
                            {
                                if (dataArr[i][0].ToCharArray()[0] < 48 || dataArr[i][0].ToCharArray()[0] > 57)
                                    continue;
                                bool isUnique = await IsUniqueEstate(dataArr[i], 1);
                                if (i > 0 && !isUnique)
                                    continue;
                                Estate estate = new Estate()
                                {
                                    CityAddress = parser.GetStringOrNullData(dataArr[i][1]),
                                    StreetAddress = parser.GetStringOrNullData(dataArr[i][2]),
                                    HouseNumber = parser.GetStringOrNullData(dataArr[i][3]),
                                    ApartmentNumber = parser.GetStringOrNullData(dataArr[i][4]),
                                    Latitude = parser.ConvertToDoubleOrNull(dataArr[i][5], -90, 90),
                                    Longtitude = parser.ConvertToDoubleOrNull(dataArr[i][6], -180, 180),
                                    TotalArea = parser.ConvertToDoubleOrNull(dataArr[i][7], 0),
                                    IdTypeOfEstate = 3
                                };
                                esoftDB.Estates.Add(estate);
                            }
                        }
                        break;
                    default:
                        break;
                }
                await SaveChangesDB();


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка");
            }
        }

        public async Task RemoveEstate(IEnumerable data)
        {
            try
            {
                Estate[] estates = data.Cast<Estate>().ToArray();
                List<string> errRemove = new List<string>();
                foreach (var estate in estates)
                {
                    var est = await esoftDB.Estates.Where(x => x.Id == estate.Id).FirstAsync();
                    if (est.Offers.Count > 0)
                    {
                        errRemove.Add($"{est.Id} | г. {est.CityAddress} ул. {est.StreetAddress} {est.HouseNumber} {est.ApartmentNumber}");
                        continue;
                    }
                    esoftDB.Estates.Remove(est);
                }
                if (errRemove.Count == 1)
                {
                    MessageBox.Show($"Клиента {errRemove[0]} нельзя удалить");
                }
                else if (errRemove.Count > 1)
                {
                    string str = string.Empty;
                    for (int i = 0; i < errRemove.Count; i++)
                        str += $"{errRemove[i]}\n";
                    MessageBox.Show($"Клиентов нельзя удалить:\n{str}");
                }
                await SaveChangesDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось удалить клиента", "Ошибка");
            }
        }

        public async Task<IEnumerable> GetEstates()
        {
            //var q = from x in esoftDB.Estates
            //        join y in esoftDB.TypesOfEstates
            //        on x.IdTypeOfEstate equals y.Id into a
            //        from z in a.DefaultIfEmpty()
            //        select new
            //        {
            //            x.CityAddress,
            //            x.StreetAddress,
            //            x.HouseNumber,
            //            x.ApartmentNumber,
            //            z.TypeName,
            //            x.Latitude,
            //            x.Longtitude,
            //            x.TotalArea,
            //            OffersCount = x.Offers.Count
            //        };

            //var list = await q.ToListAsync();
            var list = await esoftDB.Estates.ToListAsync();
            return list;
        }

        public async Task<List<string>> FillingCmbType()
        {
            var q = (from x in esoftDB.Estates
                     join y in esoftDB.TypesOfEstates
                     on x.IdTypeOfEstate equals y.Id into a
                     from z in a.DefaultIfEmpty()
                     select z.TypeName).Distinct();

            var list = await q.ToListAsync();

            return list;
        }

        public async Task<List<string>> FillingCmbStreetAddress()
        {
            var q = (from x in esoftDB.Estates
                     select x.StreetAddress).Distinct();

            var list = await q.ToListAsync();

            return list;
        }

        public async Task<IEnumerable> SearchAsync(string filterSrcText = null, string filterType = null, string filterAddress = null)
        {
            try
            {
                if (string.IsNullOrEmpty(filterSrcText) && string.IsNullOrEmpty(filterType) && string.IsNullOrEmpty(filterAddress))
                    throw new Exception("Пустой фильтр");
                if (filterType == "По умолчанию")
                    filterType = null;

                filterAddress = filterAddress == "По умолчанию" ? null : filterAddress;


                List<Estate> estates = await esoftDB.Estates.ToListAsync();
                if (string.IsNullOrEmpty(filterSrcText) && string.IsNullOrEmpty(filterType) && string.IsNullOrEmpty(filterAddress))
                    return estates;
                if (filterType != null && string.IsNullOrEmpty(filterSrcText) && filterAddress == null)
                {
                    var list = new List<Estate>();
                    foreach (var estate in estates)
                    {
                        if(estate.TypesOfEstate.TypeName == filterType)
                            list.Add(estate);
                    }
                    return list;
                }
                else if (filterAddress != null && string.IsNullOrEmpty(filterSrcText) && filterType == null)
                {
                    var list = new List<Estate>();
                    foreach (var estate in estates)
                    {
                        if (estate.StreetAddress == filterAddress)
                            list.Add(estate);
                    }
                    return list;
                }
                else if (filterType != null && filterAddress != null && string.IsNullOrEmpty(filterSrcText))
                {
                    var list = new List<Estate>();
                    foreach (var estate in estates)
                    {
                        if (estate.TypesOfEstate.TypeName == filterType && estate.StreetAddress == filterAddress)
                            list.Add(estate);
                    }
                    return list;
                }
                else if (!string.IsNullOrEmpty(filterSrcText) && filterAddress == null && filterType == null)
                {
                    
                    var list = new List<Estate>();
                    foreach (var estate in estates)
                    {
                        if (LevenshteinDistanceM(estate.CityAddress = estate.CityAddress == null ? "" : estate.CityAddress, filterSrcText) <= 3 ||
                            LevenshteinDistanceM(estate.StreetAddress = estate.StreetAddress == null ? "" : estate.StreetAddress, filterSrcText) <= 3 ||
                            LevenshteinDistanceM(estate.ApartmentNumber = estate.ApartmentNumber == null ? "" : estate.ApartmentNumber, filterSrcText) <=3 ||
                            LevenshteinDistanceM(estate.HouseNumber = estate.HouseNumber == null ? "" : estate.HouseNumber, filterSrcText) <=3)
                                list.Add(estate);
                    }

                    return list;
                }
                else if (!string.IsNullOrEmpty(filterSrcText) && filterAddress != null && filterType == null)
                {
                    var filter = new List<Estate>();
                    foreach (var estate in estates)
                    {
                        if (estate.StreetAddress == filterAddress)
                            filter.Add(estate);
                    }

                    var list = new List<Estate>();
                    foreach(var estate in filter)
                    {
                        if (LevenshteinDistanceM(estate.CityAddress = estate.CityAddress == null ? "" : estate.CityAddress, filterSrcText) <= 3 ||
                            LevenshteinDistanceM(estate.StreetAddress = estate.StreetAddress == null ? "" : estate.StreetAddress, filterSrcText) <= 3 ||
                            LevenshteinDistanceM(estate.ApartmentNumber = estate.ApartmentNumber == null ? "" : estate.ApartmentNumber, filterSrcText) <= 3 ||
                            LevenshteinDistanceM(estate.HouseNumber = estate.HouseNumber == null ? "" : estate.HouseNumber, filterSrcText) <= 3)
                                list.Add(estate);
                    }

                    return list;
                }
                else if (!string.IsNullOrEmpty(filterSrcText) && filterType != null && filterAddress == null)
                {
                    var filter = new List<Estate>();
                    foreach (var estate in estates)
                    {
                        if (estate.TypesOfEstate.TypeName == filterType)
                            filter.Add(estate);
                    }

                    var list = new List<Estate>();
                    foreach (var estate in filter)
                    {
                        if (LevenshteinDistanceM(estate.CityAddress = estate.CityAddress == null ? "" : estate.CityAddress, filterSrcText) <= 3 ||
                            LevenshteinDistanceM(estate.StreetAddress = estate.StreetAddress == null ? "" : estate.StreetAddress, filterSrcText) <= 3 ||
                            LevenshteinDistanceM(estate.ApartmentNumber = estate.ApartmentNumber == null ? "" : estate.ApartmentNumber, filterSrcText) <= 3 ||
                            LevenshteinDistanceM(estate.HouseNumber = estate.HouseNumber == null ? "" : estate.HouseNumber, filterSrcText) <= 3)
                            list.Add(estate);
                    }

                    return list;
                }
                else
                {
                    var filter = new List<Estate>();
                    foreach (var estate in estates)
                    {
                        if (estate.StreetAddress == filterAddress && estate.TypesOfEstate.TypeName == filterType)
                            filter.Add(estate);
                    }

                    var list = new List<Estate>();
                    foreach (var estate in filter)
                    {
                        if (LevenshteinDistanceM(estate.CityAddress = estate.CityAddress == null ? "" : estate.CityAddress, filterSrcText) <= 3 ||
                            LevenshteinDistanceM(estate.StreetAddress = estate.StreetAddress == null ? "" : estate.StreetAddress, filterSrcText) <= 3 ||
                            LevenshteinDistanceM(estate.ApartmentNumber = estate.ApartmentNumber == null ? "" : estate.ApartmentNumber, filterSrcText) <= 3 ||
                            LevenshteinDistanceM(estate.HouseNumber = estate.HouseNumber == null ? "" : estate.HouseNumber, filterSrcText) <= 3)
                            list.Add(estate);
                    }

                    return list;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
