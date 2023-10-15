using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Esoft.Model;

namespace WpfApp1.Controller
{
    class ModelControl
    {
        public static esoftDBEntities esoftDB;

        public static void SaveChangesDB()
        {
            try
            {
                esoftDB.SaveChanges();
                MessageBox.Show("Изменения успешно сохранены.", "Успех");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void AddEstate(string data, int idTypeOfEstate)
        {
            try
            {
                string[] dataSplit = data.Split(',');
                switch (idTypeOfEstate)
                {
                    case 0: //house
                        {

                            if (data[0] < 60 || data[0] > 71)
                            {
                                Estate estate = new Estate()
                                {
                                    CityAddress = dataSplit[0],
                                    StreetAddress = dataSplit[1],
                                    HouseNumber = dataSplit[2],
                                    ApartmentNumber = dataSplit[3],
                                    Latitude = GetDoubleData(dataSplit[4]),
                                    Longtitude = GetDoubleData(dataSplit[5]),
                                    NumberOfStroyes = GetIntData(dataSplit[6]),
                                    TotalArea = GetDoubleData(dataSplit[7])
                                };
                                esoftDB.Estates.Add(estate);
                            }
                            else
                            {
                                Estate estate = new Estate()
                                {
                                    CityAddress = dataSplit[1],
                                    StreetAddress = dataSplit[2],
                                    HouseNumber = dataSplit[3],
                                    ApartmentNumber = dataSplit[4],
                                    Latitude = GetDoubleData(dataSplit[5]),
                                    Longtitude = GetDoubleData(dataSplit[6]),
                                    NumberOfStroyes = GetIntData(dataSplit[7]),
                                    TotalArea = GetDoubleData(dataSplit[8])
                                };
                                esoftDB.Estates.Add(estate);
                            }
                                
                            SaveChangesDB();
                        }
                        break;
                    case 1: //appartment
                        {

                            if (data[0] < 60 || data[0] > 71)
                            {
                                Estate estate = new Estate()
                                {
                                    CityAddress = dataSplit[0],
                                    StreetAddress = dataSplit[1],
                                    HouseNumber = dataSplit[2],
                                    ApartmentNumber = dataSplit[3],
                                    Latitude = GetDoubleData(dataSplit[4]),
                                    Longtitude = GetDoubleData(dataSplit[5]),
                                    TotalArea = GetDoubleData(dataSplit[7]),
                                    NumberOfRooms = GetIntData(dataSplit[8]),
                                    FloorNumber = GetIntData(dataSplit[9]),
                                };
                                esoftDB.Estates.Add(estate);
                            }
                            else
                            {
                                Estate estate = new Estate()
                                {
                                    CityAddress = dataSplit[1],
                                    StreetAddress = dataSplit[2],
                                    HouseNumber = dataSplit[3],
                                    ApartmentNumber = dataSplit[4],
                                    Latitude = GetDoubleData(dataSplit[5]),
                                    Longtitude = GetDoubleData(dataSplit[6]),
                                    TotalArea = GetDoubleData(dataSplit[7]),
                                    NumberOfRooms = GetIntData(dataSplit[8]),
                                    FloorNumber = GetIntData(dataSplit[9]),
                                };
                                esoftDB.Estates.Add(estate);
                            }
                            SaveChangesDB();
                        }
                        break;
                    case 2: //land
                        {
                            if (data[0] < 60 || data[0] > 71)
                            {
                                Estate estate = new Estate()
                                {
                                    CityAddress = dataSplit[0],
                                    StreetAddress = dataSplit[1],
                                    HouseNumber = dataSplit[2],
                                    ApartmentNumber = dataSplit[3],
                                    Latitude = GetDoubleData(dataSplit[4]),
                                    Longtitude = GetDoubleData(dataSplit[5]),
                                    TotalArea = GetDoubleData(dataSplit[7]),
                                };
                                esoftDB.Estates.Add(estate);
                            }
                            else
                            {
                                Estate estate = new Estate()
                                {
                                    CityAddress = dataSplit[1],
                                    StreetAddress = dataSplit[2],
                                    HouseNumber = dataSplit[3],
                                    ApartmentNumber = dataSplit[4],
                                    Latitude = GetDoubleData(dataSplit[5]),
                                    Longtitude = GetDoubleData(dataSplit[6]),
                                    TotalArea = GetDoubleData(dataSplit[7]),
                                };
                                esoftDB.Estates.Add(estate);
                            }
                            SaveChangesDB();
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

        public void AddEstate(string[][] dataArr, int idTypeOfEstate)
        {
            try
            {
                switch(idTypeOfEstate)
                {
                    case 0: //house
                        {
                            for (int i = 0; i < dataArr.Length; i++)
                            {
                                if (dataArr[i][0].ToCharArray()[0] < 48 || dataArr[i][0].ToCharArray()[0] > 57)
                                    continue;
                                Estate estate = new Estate()
                                {
                                    CityAddress = dataArr[i][1],
                                    StreetAddress = dataArr[i][2],
                                    HouseNumber = dataArr[i][3],
                                    ApartmentNumber = dataArr[i][4],
                                    Latitude = GetDoubleData(dataArr[i][5]),
                                    Longtitude = GetDoubleData(dataArr[i][6]),
                                    NumberOfStroyes = GetIntData(dataArr[i][7]),
                                    TotalArea = GetDoubleData(dataArr[i][8])
                                };
                                esoftDB.Estates.Add(estate);
                            }
                            SaveChangesDB();
                        }
                        break;
                    case 1: //appartment
                        {
                            for (int i = 0; i < dataArr.Length; i++)
                            {
                                if (dataArr[i][0].ToCharArray()[0] < 60 || dataArr[i][0].ToCharArray()[0] > 71)
                                    continue;
                                Estate estate = new Estate()
                                {
                                    CityAddress = dataArr[i][1],
                                    StreetAddress = dataArr[i][2],
                                    HouseNumber = dataArr[i][3],
                                    ApartmentNumber = dataArr[i][4],
                                    Latitude = GetDoubleData(dataArr[i][5]),
                                    Longtitude = GetDoubleData(dataArr[i][6]),
                                    TotalArea = GetDoubleData(dataArr[i][7]),
                                    FloorNumber = GetIntData(dataArr[i][8]),
                                    NumberOfStroyes = GetIntData(dataArr[i][9]),
                                };
                                esoftDB.Estates.Add(estate);
                            }
                            SaveChangesDB();
                        }
                        break;
                    case 2: //land
                        {
                            for (int i = 0; i < dataArr.Length; i++)
                            {
                                if (dataArr[i][0].ToCharArray()[0] < 60 || dataArr[i][0].ToCharArray()[0] > 71)
                                    continue;
                                Estate estate = new Estate()
                                {
                                    CityAddress = dataArr[i][1],
                                    StreetAddress = dataArr[i][2],
                                    HouseNumber = dataArr[i][3],
                                    ApartmentNumber = dataArr[i][4],
                                    Latitude = GetDoubleData(dataArr[i][5]),
                                    Longtitude = GetDoubleData(dataArr[i][6]),
                                    TotalArea = GetDoubleData(dataArr[i][7])
                                };
                                esoftDB.Estates.Add(estate);
                            }
                            SaveChangesDB();
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

        public void AddDemand(string[][] dataArr)
        {
            for (int i = 0; i < dataArr.Length; i++)
            {
                if (dataArr[i][0].ToCharArray()[0] < 60 || dataArr[i][0].ToCharArray()[0] > 71)
                    continue;
                Demand demand = new Demand()
                {
                    CityAddress = dataArr[i][1],

                };
                esoftDB.Demands.Add(demand);
            }
            SaveChangesDB();
        }

        public void AddOffer(string[][] dataArr)
        {
            for (int i = 0; i < dataArr.Length; i++)
            {
                if (dataArr[i][0].ToCharArray()[0] < 60 || dataArr[i][0].ToCharArray()[0] > 71)
                    continue;
                Offer offer = new Offer()
                {

                };
                esoftDB.Offers.Add(offer);
            }
            SaveChangesDB();
        }

        public int? GetIntData(string data)
        {
            if(data == null ||
               string.IsNullOrEmpty(data))
            {
                MessageBox.Show("Пустая строка");
                return null;
            }
            else if(int.TryParse(data, out int x))
                return x;
            else
            {
                MessageBox.Show("Некорректные данные");
                return null;
            }
        }

        public double? GetDoubleData(string data)
        {
            data = data.Replace('.', ',');
            if (data == null ||
               string.IsNullOrEmpty(data))
            {
                MessageBox.Show("Пустая строка");
                return null;
            }
            else if (double.TryParse(data, out double x))
                return x;
            else
            {
                MessageBox.Show("Некорректные данные");
                return null;
            }
        }
    }
}
