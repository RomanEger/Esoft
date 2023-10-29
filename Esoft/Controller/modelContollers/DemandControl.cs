using Esoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    }
}
