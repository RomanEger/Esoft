using Esoft.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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

                    bool isUnique = await IsUniqueRealtor(arr[i], 1);
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

                                bool isUnique = await IsUniqueEstate(dataArr[i]);
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
                                bool isUnique = await IsUniqueEstate(dataArr[i]);
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
                                bool isUnique = await IsUniqueEstate(dataArr[i]);
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

        public async Task RemoveEstate(IEnumerable estates)
        {

        }


        public async Task<IEnumerable> GetEstates()
        {
            var q = from x in esoftDB.Estates
                    select new
                    {
                        x.CityAddress,
                        x.StreetAddress,
                        x.HouseNumber,
                        x.ApartmentNumber,
                        GetTypeOfEstate = x.IdTypeOfEstate == 1 ? "Дом" : x.IdTypeOfEstate == 2 ? "Квартира" : x.IdTypeOfEstate == 3 ? "Земля" : null,
                        x.Latitude,
                        x.Longtitude,
                        x.TotalArea,
                        OffersCount = x.Offers.Count
                    };

            var list = await q.ToListAsync();
            return list;
        }

    }
}
